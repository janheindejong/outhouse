import dataclasses
from typing import Protocol

from .entities import Cottage, Membership, Role, User

__all__ = ["UserDbAdapter", "UserInteractor"]


class UserDbAdapter(Protocol):
    """Responsible for lower level database operations"""

    def create_user(self, name: str, email: str) -> User:
        ...

    def get_user_by_id(self, id: int) -> User | None:
        ...

    def get_user_by_email(self, email: str) -> User | None:
        ...


class MembershipDbAdapter(Protocol):
    """Responsible for lower level database operations"""

    def create_membership(
        self, user_id: int, cottage_id: int, role: Role
    ) -> Membership | None:
        ...

    def get_membership_by_id(self, id: int) -> Membership | None:
        ...

    def get_memberships_by_user_id(self, user_id: int) -> list[Membership]:
        ...

    def get_memberships_by_cottage_id(self, user_id: int) -> list[Membership]:
        ...


class CottageDbAdapter(Protocol):
    """Responsible for lower level database operations"""

    def create_cottage(self, name: str, email: str) -> Cottage:
        ...

    def get_cottage_by_id(self, id: int) -> Cottage | None:
        ...


class UserInteractor:
    """Responsible for executing business logic related to managing
    users"""

    def __init__(
        self, user_db: UserDbAdapter, membership_db: MembershipDbAdapter
    ) -> None:
        self._user_db = user_db
        self._membership_db = membership_db

    def create(self, name: str, email: str) -> dict:
        user = self._user_db.create_user(name, email)
        return dataclasses.asdict(user)

    def get_by_id(self, id: int) -> dict | None:
        user = self._user_db.get_user_by_id(id)
        return dataclasses.asdict(user) if user else None

    def get_by_email(self, email: str) -> dict | None:
        user = self._user_db.get_user_by_email(email)
        return dataclasses.asdict(user) if user else None

    def get_memberships(self, user_id: int) -> list[dict]:
        memberships = self._membership_db.get_memberships_by_user_id(user_id)
        return [dataclasses.asdict(membership) for membership in memberships]
