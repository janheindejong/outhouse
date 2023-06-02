from typing import Protocol

from .entities import User

__all__ = ["UserDbAdapter", "UserManager"]


class UserDbAdapter(Protocol):
    """Responsible for lower level database operations"""

    def create(self, name: str, email: str) -> int:
        ...

    def get_by_id(self, id: int) -> dict | None:
        ...

    def get_by_email(self, email: str) -> dict | None:
        ...


class UserManager:
    """Responsible for executing business logic related to managing
    users (i.e. creating users, querying users, deleting users)"""

    def __init__(self, user_db: UserDbAdapter) -> None:
        self._user_db = user_db

    def create(self, name: str, email: str) -> User:
        id = self._user_db.create(name, email)
        return User(name=name, email=email, id=id)

    def get_by_id(self, id: int) -> User | None:
        user = self._user_db.get_by_id(id)
        if user:
            return User(**user)
        else:
            return None

    def get_by_email(self, email: str) -> User | None:
        user = self._user_db.get_by_email(email)
        if user:
            return User(**user)
        else:
            return None
