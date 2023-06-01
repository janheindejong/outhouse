from typing import Protocol
from .entities import User


__all__ = ["UserDbAdapter", "UserManager"]


class UserDbAdapter(Protocol):
    """Responsible for lower level database operations"""

    def create(self, name: str) -> int:
        ...

    def get(self, id: int) -> dict | None:
        ...


class UserManager:
    """Responsible for executing business logic (i.e. check if user can
    create booking, and create it); uses DbHandler for this purpose"""

    def __init__(self, user_db: UserDbAdapter) -> None:
        self._user_db = user_db

    def create(self, name: str) -> User:
        id = self._user_db.create(name)
        return User(name=name, id=id)

    def get_by_id(self, id: int) -> User | None:
        user = self._user_db.get(id)
        if user:
            return User(**user)
        else:
            return None
