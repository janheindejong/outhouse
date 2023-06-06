import os

from .adapters import SQLUserDbAdapter
from .drivers import SQLiteConnection
from .interactors import UserInteractor


class UserInteractorFactory:
    def __init__(self, db_url: str | bytes | os.PathLike) -> None:
        self._db_url = db_url

    def __call__(self):
        conn = SQLiteConnection(self._db_url)
        try:
            adapter = SQLUserDbAdapter(conn)
            yield UserInteractor(adapter)
        finally:
            conn.close()
