from dataclasses import dataclass


__all__ = ["User"]


@dataclass
class User:
    name: str
    id: int | None = None
