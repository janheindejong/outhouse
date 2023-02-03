from datetime import datetime

from pydantic import BaseModel


class UserBase(BaseModel):
    name: str


class UserIn(UserBase):
    ...


class UserInDB(UserBase):
    id: int

    class Config:
        orm_mode = True


class OuthouseBase(BaseModel):
    name: str


class OuthouseIn(OuthouseBase):
    ...


class OuthouseInDB(OuthouseBase):
    id: int

    class Config:
        orm_mode = True


class BookingBase(BaseModel):
    startDate: datetime
    endDate: datetime
    userId: int
    outhouseId: int

    class Config:
        orm_mode = True


class BookingIn(BookingBase):
    ...


class BookingInDB(BookingBase):
    id: int
