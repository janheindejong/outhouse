from .db_adapters import SQLConnection
from typing import Any

import sqlite3


class SQLiteConnection(SQLConnection):
    _cur: sqlite3.Cursor

    def __init__(self, path: str) -> None:
        self._conn = sqlite3.connect(path)
        self._conn.row_factory = sqlite3.Row

    def start(self):
        self._cur = self._conn.cursor()

    def execute(self, sql: str):
        self._check_conn()
        return self._cur.execute(sql)

    def commit(self):
        self._check_conn()
        self._conn.commit()

    def fetchone(self) -> dict[str, Any]:
        self._check_conn()
        obj = self._cur.fetchone()
        if not isinstance(obj, dict):
            raise Exception("Fetched object is not a dict")
        return obj

    def close(self):
        return self._conn.close()

    @property
    def lastrowid(self) -> int | None:
        self._check_conn()
        return self._cur.lastrowid

    def _check_conn(self):
        if not hasattr("_cur"):
            raise Exception("Start connection first")
