from typing import Generic, Protocol, Type, TypeVar

from .entities import Membership, Role, User
from .interactors import MembershipDbAdapter, UserDbAdapter

__all__ = ["SQLUserDbAdapter", "SQLConnection"]


class SQLCursor(Protocol):
    """Stricter implementation of PEP249 Connection object"""

    def execute(self, sql: str):
        ...

    def fetchone(self) -> dict | None:
        ...

    def fetchall(self) -> list[dict]:
        ...

    @property
    def lastrowid(self) -> int | None:
        ...


class SQLConnection(Protocol):
    """Stricter implementation of PEP249 Cursor object"""

    def cursor(self) -> SQLCursor:
        ...

    def commit(self):
        ...


Entity = TypeVar("Entity")


class BaseSQLDbAdapter(Generic[Entity]):

    _constructor: Type[Entity]
    _table: str

    def __init__(self, conn: SQLConnection):
        self._conn = conn

    def _insert(self, values: str) -> Entity:
        cur = self._conn.cursor()
        operation = f"""
            INSERT INTO {self._table} VALUES {values}
        """
        cur.execute(operation)
        self._conn.commit()
        if not cur.lastrowid:
            raise Exception("Couldn't get last row ID")
        operation = f"""
            SELECT * FROM {self._table} WHERE rowid={cur.lastrowid}
        """
        cur.execute(operation)
        row = cur.fetchone()
        if not row:
            raise Exception("Couldn't get new row")
        return self._constructor(**row)

    def _get_one(self, where: str) -> Entity | None:
        cur = self._conn.cursor()
        operation = f"""
            SELECT * FROM {self._table} WHERE {where}
        """
        cur.execute(operation)
        if row := cur.fetchone():
            return self._constructor(**row)
        else:
            return None

    def _get_many(self, where: str) -> list[Entity]:
        cur = self._conn.cursor()
        operation = f"""
            SELECT * FROM {self._table} WHERE {where}
        """
        cur.execute(operation)
        return [self._constructor(**row) for row in cur.fetchall()]


class SQLUserDbAdapter(UserDbAdapter, BaseSQLDbAdapter[User]):
    """Contains SQL logic to interact with user DB"""

    _constructor = User
    _table = "user"

    def create_user(self, name: str, email: str) -> User:
        return self._insert(f"(NULL, '{name}', '{email}')")

    def get_user_by_id(self, id: int) -> User | None:
        return self._get_one(f"id={id}")

    def get_user_by_email(self, email: str) -> User | None:
        return self._get_one(f"email='{email}'")


class SQLMembershipDbAdapter(MembershipDbAdapter, BaseSQLDbAdapter[Membership]):
    """Contains SQL logic to interact with membership DB"""

    _constructor = Membership
    _table = "membership"

    def create_membership(
        self, user_id: int, cottage_id: int, role: Role
    ) -> Membership:
        return self._insert(f"(NULL, '{user_id}', '{cottage_id}', '{role}')")

    def get_membership_by_id(self, id: int) -> Membership | None:
        return self._get_one(f"id={id}")

    def get_memberships_by_user_id(self, user_id: int) -> list[Membership]:
        return self._get_many(f"user_id={user_id}")

    def get_memberships_by_cottage_id(self, cottage_id: int) -> list[Membership]:
        return self._get_many(f"cottage_id={cottage_id}")
