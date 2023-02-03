from typing import Optional

from fastapi import APIRouter, Depends
from sqlalchemy.orm import Session

from ...db.services import BookingService
from ..dependencies import get_db_session
from ..schemas import BookingIn, BookingInDB

router = APIRouter()


@router.get("/", response_model=list[BookingInDB])
def get(
    outhouseId: Optional[int] = None,
    userId: Optional[int] = None,
    session: Session = Depends(get_db_session),
):
    return BookingService(session).get_by_user_and_outhouse_id(
        userId=userId, outhouseId=outhouseId
    )


@router.post("/", response_model=BookingInDB)
def post(booking: BookingIn, session: Session = Depends(get_db_session)):
    return BookingService(session).create(
        startDate=booking.startDate,
        endDate=booking.endDate,
        userId=booking.userId,
        outhouseId=booking.outhouseId,
    )


@router.delete("/{id}", response_model=BookingInDB)
def delete(bookingId: int, session: Session = Depends(get_db_session)):
    return BookingService(session).delete(bookingId)
