from datetime import datetime

from .db import AbstractService
from .models import Booking, Outhouse, User


class UserService(AbstractService):
    def get_all(self) -> list[User]:
        return self._session.query(User).all()

    def create(self, name: str) -> User:
        user = User(name=name)
        self._session.add(user)
        self._session.commit()
        self._session.refresh(user)
        return user


class OuthouseService(AbstractService):
    def get_all(self) -> list[User]:
        return self._session.query(Outhouse).all()

    def create(self, name: str) -> Outhouse:
        outhouse = Outhouse(name=name)
        self._session.add(outhouse)
        self._session.commit()
        self._session.refresh(outhouse)
        return outhouse


class CalendarService(AbstractService):
    def get_bookings(self, outhouseId: int) -> list[Booking]:
        return (
            self._session.query(Booking).filter(Booking.outhouseId == outhouseId).all()
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
