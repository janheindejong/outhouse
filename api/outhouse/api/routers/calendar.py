from fastapi import APIRouter, Depends
from sqlalchemy.orm import Session

from ...db.services import CalendarService
from ..dependencies import get_db_session
from ..schemas import Booking, BookingIn

router = APIRouter()


@router.get("/booking", response_model=list[Booking])
def get_bookings(outhouseId: int, session: Session = Depends(get_db_session)):
    return CalendarService(session).get_bookings_by_outhouse_id(outhouseId)


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


@router.delete("/booking/{id}", response_model=Booking)
def delete_booking(
    bookingId: int, outhouseId: int, session: Session = Depends(get_db_session)
):
    return CalendarService(session).delete_booking(bookingId)
