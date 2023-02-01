from contextlib import contextmanager
from datetime import datetime
from typing import Generator

from sqlalchemy import Column, DateTime, ForeignKey, Integer, String, create_engine
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import Session, relationship, sessionmaker

Base = declarative_base()


class User(Base):
    __tablename__ = "users"

    id = Column(Integer, primary_key=True)
    name = Column(String)
    bookings = relationship("Booking", cascade="all, delete")


class Booking(Base):
    __tablename__ = "bookings"

    id = Column(Integer, primary_key=True)
    startDate = Column(DateTime)
    endDate = Column(DateTime)
    userId = Column(Integer, ForeignKey("users.id"))
    user = relationship("User", back_populates="bookings")


class DbHandler:
    def __init__(self, db_url: str) -> None:
        self._session_factory = sessionmaker(
            autocommit=False,
            autoflush=False,
            bind=create_engine(
                db_url, connect_args={"check_same_thread": False}, echo=False
            ),
        )

    @contextmanager
    def session(self) -> Generator[Session, None, None]:
        session: Session = self._session_factory()
        try:
            yield session
        except Exception:
            session.rollback()
            raise
        finally:
            session.close()


class AbstractService:
    def __init__(self, session: Session) -> None:
        self._session = session


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
