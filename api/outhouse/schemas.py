from datetime import datetime

from pydantic import BaseModel


class UserIn(BaseModel):
    name: str


class User(UserIn):
    userId: str


class BookingIn(BaseModel):
    startDate: datetime
    endDate: datetime
    userId: str


class Booking(BookingIn):
    bookingId: int
