from .managers import UserDbAdapter
from typing import Protocol


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

    def create(self, name: str) -> int:
        cur = self._conn.cursor()
        cur.execute(
            f"""
            INSERT INTO user VALUES (NULL, '{name}')
            """
        )
        self._conn.commit()
        if not cur.lastrowid:
            raise Exception("Couldn't get last row ID")
        return cur.lastrowid

    def get(self, id: int) -> dict | None:
        cur = self._conn.cursor()
        cur.execute(
            f"""
            SELECT * FROM user WHERE id={id}
            """
        )
        return cur.fetchone()
