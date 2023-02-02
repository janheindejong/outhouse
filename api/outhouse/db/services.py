from datetime import datetime

from .db import AbstractService
from .models import Booking, User


class UserService(AbstractService):
    def get_all(self) -> list[User]:
        return self._session.query(User).all()

    def create(self, name: str) -> User:
        user = User(name=name)
        self._session.add(user)
        self._session.commit()
        self._session.refresh(user)
        return user


class CalendarService(AbstractService):
    def get_bookings(self) -> list[Booking]:
        return self._session.query(Booking).all()

    def create_booking(
        self, startDate: datetime, endDate: datetime, userId: int
    ) -> Booking:
        booking = Booking(startDate=startDate, endDate=endDate, userId=userId)
        self._session.add(booking)
        self._session.commit()
        self._session.refresh(booking)
        return booking