from dataclasses import dataclass
from enum import Enum

__all__ = ["User", "Membership", "Role", "Cottage"]


class Role(Enum):
    ADMIN = "admin"
    MEMBER = "member"


@dataclass
class Membership:
    id: int
    cottage_id: int
    user_id: int
    role: Role


@dataclass
class User:
    id: int
    name: str
    email: str


@dataclass
class Cottage:
    id: int
    name: str
