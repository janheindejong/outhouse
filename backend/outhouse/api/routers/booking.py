from typing import Optional

from fastapi import APIRouter, Depends

from ...db.services import BookingService
from ..dependencies import get_service
from ..schemas import BookingIn, BookingInDB

router = APIRouter()


@router.get("/", response_model=list[BookingInDB])
def get(
    outhouseId: Optional[int] = None,
    userId: Optional[int] = None,
    service: BookingService = Depends(get_service(BookingService)),
):
    return service.get_by_user_and_outhouse_id(userId=userId, outhouseId=outhouseId)


@router.post("/", response_model=BookingInDB)
def post(
    booking: BookingIn, service: BookingService = Depends(get_service(BookingService))
):
    return service.create(
        startDate=booking.startDate,
        endDate=booking.endDate,
        userId=booking.userId,
        outhouseId=booking.outhouseId,
    )


@router.delete("/{id}", response_model=BookingInDB)
def delete(
    bookingId: int, service: BookingService = Depends(get_service(BookingService))
):
    return service.delete(bookingId)
