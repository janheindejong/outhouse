from dataclasses import dataclass

__all__ = ["User"]


@dataclass
class User:
    name: str
    email: str
    id: int | None = None


@dataclass
class Cottage:
    name: str
    id: int | None = None
