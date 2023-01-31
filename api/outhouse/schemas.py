from datetime import datetime

from pydantic import BaseModel


class UserIn(BaseModel):
    name: str


class User(UserIn):
    id: int

    class Config:
        orm_mode = True


class BookingIn(BaseModel):
    startDate: datetime
    endDate: datetime
    userId: int

    class Config:
        orm_mode = True


class Booking(BookingIn):
    id: int
