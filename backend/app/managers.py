from typing import Protocol

from .entities import User

__all__ = ["UserDbAdapter", "UserManager"]


class UserDbAdapter(Protocol):
    """Responsible for lower level database operations"""

    def create_user(self, name: str, email: str) -> User:
        ...

    def get_user_by_id(self, id: int) -> User | None:
        ...

    def get_user_by_email(self, email: str) -> User | None:
        ...


class UserManager:
    """Responsible for executing business logic related to managing
    users (i.e. creating users, querying users, deleting users)"""

    def __init__(self, user_db: UserDbAdapter) -> None:
        self._user_db = user_db

    def create(self, name: str, email: str) -> User:
        return self._user_db.create_user(name, email)

    def get_by_id(self, id: int) -> User | None:
        return self._user_db.get_user_by_id(id)

    def get_by_email(self, email: str) -> User | None:
        return self._user_db.get_user_by_email(email)
