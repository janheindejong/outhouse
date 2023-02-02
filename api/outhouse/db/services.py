from datetime import datetime
from typing import Optional

from sqlalchemy.orm import selectinload

from .db import AbstractService
from .models import Booking, Outhouse, User


class UserService(AbstractService):
    def get_all(self) -> list[User]:
        return self._session.query(User).all()

    def get(self, id: int) -> Optional[User]:
        return self._session.query(User).options(selectinload(User.bookings)).get(id)

    def create(self, name: str) -> User:
        user = User(name=name)
        self._session.add(user)
        self._session.commit()
        self._session.refresh(user)
        return user

    def delete(self, id: int) -> Optional[User]:
        user = self._session.query(User).get(id)
        self._session.delete(user)
        self._session.commit()
        return user


class OuthouseService(AbstractService):
    def get_all(self) -> list[Outhouse]:
        return self._session.query(Outhouse).all()

    def get(self, id: int) -> Optional[Outhouse]:
        return (
            self._session.query(Outhouse)
            .options(selectinload(Outhouse.bookings))
            .get(id)
        )

    def create(self, name: str) -> Outhouse:
        outhouse = Outhouse(name=name)
        self._session.add(outhouse)
        self._session.commit()
        self._session.refresh(outhouse)
        return outhouse

    def delete(self, id: int) -> Optional[Outhouse]:
        outhouse = self._session.query(Outhouse).get(id)
        self._session.delete(outhouse)
        self._session.commit()
        return outhouse


class CalendarService(AbstractService):
    def get_bookings_by_outhouse_id(self, outhouseId: int) -> list[Booking]:
        return (
            self._session.query(Booking)
            .options(selectinload(Booking.user))
            .options(selectinload(Booking.outhouse))
            .filter(Booking.outhouseId == outhouseId)
            .all()
        )

    def create_booking(
        self, startDate: datetime, endDate: datetime, userId: int, outhouseId: int
    ) -> Booking:
        booking = Booking(
            startDate=startDate, endDate=endDate, userId=userId, outhouseId=outhouseId
        )
        self._session.add(booking)
        self._session.commit()
        self._session.refresh(booking)
        return booking

    def delete_booking(self, bookingId: int) -> Optional[Booking]:
        booking = self._session.query(Booking).get(bookingId)
        self._session.delete(booking)
        self._session.commit()
        return booking
