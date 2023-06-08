import os

from .adapters import SQLMembershipDbAdapter, SQLUserDbAdapter
from .drivers import SQLiteConnection
from .interactors import UserInteractor


class UserInteractorFactory:
    def __init__(self, db_url: str | bytes | os.PathLike) -> None:
        self._db_url = db_url

    def __call__(self):
        conn = SQLiteConnection(self._db_url)
        try:
            user_db = SQLUserDbAdapter(conn)
            membership_db = SQLMembershipDbAdapter(conn)
            yield UserInteractor(user_db, membership_db)
        finally:
            conn.close()
