from .managers import UserDbAdapter
from typing import Protocol, Any


__all__ = ["SQLUserDbAdapter", "SQLConnection"]


class SQLConnection(Protocol):
    def start(self):
        ...

    def commit(self):
        ...

    def execute(self, sql: str):
        ...

    def fetchone(self) -> dict[str, Any]:
        ...

    @property
    def lastrowid(self) -> int | None:
        ...

    def close(self):
        ...


class SQLUserDbAdapter(UserDbAdapter):
    """Contains SQL logic to interact with user DB"""

    def __init__(self, conn: SQLConnection):
        self._conn = conn

    def create_user(self, name: str) -> int:
        self._conn.start()
        self._conn.execute(
            f"""
            INSERT INTO user VALUES (NULL, '{name}')
            """
        )
        self._conn.commit()
        if not self._conn.lastrowid:
            raise Exception("Couldn't get last row ID")
        return self._conn.lastrowid

    def get_user(self, id: int):
        self._conn.start()
        self._conn.execute(
            f"""
            SELECT * FROM user WHERE id={id}
            """
        )
        return self._conn.fetchone()

    def close(self) -> None:
        self._conn.close()
