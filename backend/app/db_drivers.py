import os
import sqlite3

from .db_adapters import SQLConnection, SQLCursor


class SQLiteCursor(SQLCursor):
    def __init__(self, cursor: sqlite3.Cursor) -> None:
        self._cur = cursor

    def execute(self, sql: str):
        self._cur = self._cur.execute(sql)

    def fetchone(self) -> dict | None:
        return self._cur.fetchone()

    @property
    def lastrowid(self) -> int | None:
        return self._cur.lastrowid


class SQLiteConnection(SQLConnection):
    def __init__(self, path: str | bytes | os.PathLike) -> None:
        self._conn = sqlite3.connect(path)
        self._conn.row_factory = self._dict_factory

    def cursor(self) -> SQLCursor:
        return SQLiteCursor(self._conn.cursor())

    def commit(self):
        self._conn.commit()

    def close(self):
        return self._conn.close()

    @staticmethod
    def _dict_factory(cursor, row):
        fields = [column[0] for column in cursor.description]
        return {key: value for key, value in zip(fields, row, strict=True)}
