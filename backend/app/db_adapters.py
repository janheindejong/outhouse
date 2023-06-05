from typing import Protocol

from .entities import User
from .managers import UserDbAdapter

__all__ = ["SQLUserDbAdapter", "SQLConnection"]


class SQLCursor(Protocol):
    """Stricter implementation of PEP249 Connection object"""

    def execute(self, sql: str):
        ...

    def fetchone(self) -> dict | None:
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


class SQLUserDbAdapter(UserDbAdapter):
    """Contains SQL logic to interact with user DB"""

    def __init__(self, conn: SQLConnection):
        self._conn = conn

    def create_user(self, name: str, email: str) -> User:
        cur = self._conn.cursor()
        cur.execute(
            """
            INSERT INTO user VALUES (NULL, '{}', '{}')
            """.format(
                name, email
            )
        )
        self._conn.commit()
        if not cur.lastrowid:
            raise Exception("Couldn't get last row ID")
        return User(id=cur.lastrowid, name=name, email=email)

    def get_user_by_id(self, id: int) -> User | None:
        cur = self._conn.cursor()
        cur.execute(
            """
            SELECT * FROM user WHERE id={}
            """.format(
                id
            )
        )
        if user := cur.fetchone():
            return User(**user)
        else:
            return None

    def get_user_by_email(self, email: str) -> User | None:
        cur = self._conn.cursor()
        cur.execute(
            """
            SELECT * FROM user WHERE email='{}'
            """.format(
                email
            )
        )
        if user := cur.fetchone():
            return User(**user)
        else:
            return None
