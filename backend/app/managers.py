from typing import Protocol
from .entities import User


__all__ = ["UserDbAdapter", "UserManager"]


class UserDbAdapter(Protocol):
    """Responsible for lower level database operations"""

    def create_user(self, name: str) -> int:
        ...

    def get_user(self, id: int) -> dict:
        ...

    def close(self) -> None:
        ...


class UserManager:
    """Responsible for executing business logic (i.e. check if user can
    create booking, and create it); uses DbHandler for this purpose"""

    def __init__(self, user_db: UserDbAdapter) -> None:
        self._user_db = user_db

    def create_user(self, name: str) -> User:
        id = self._user_db.create_user(name)
        return User(name=name, id=id)

    def get_user_by_id(self, id: int) -> User:
        return User(**self._user_db.get_user(id))

    def __del__(self):
        self._user_db.close()
