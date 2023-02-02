from datetime import datetime

from fastapi import APIRouter, Depends
from pydantic import BaseModel
from sqlalchemy.orm import Session

from ..db.services import CalendarService
from .dependencies import get_db_session
from .user import User


class BookingIn(BaseModel):
    startDate: datetime
    endDate: datetime
    userId: int

    class Config:
        orm_mode = True


class Booking(BookingIn):
    id: int
    outhouseId: int
    user: User


router = APIRouter()


@router.get("/booking", response_model=list[Booking])
def get_bookings(outhouseId: int, session: Session = Depends(get_db_session)):
    return CalendarService(session).get_bookings(outhouseId)


@router.post("/booking", response_model=Booking)
def post_booking(
    outhouseId: int, booking: BookingIn, session: Session = Depends(get_db_session)
):
    return CalendarService(session).create_booking(
        startDate=booking.startDate,
        endDate=booking.endDate,
        userId=booking.userId,
        outhouseId=outhouseId,
    )
