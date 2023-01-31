import logging
from contextlib import AbstractContextManager, contextmanager
from datetime import datetime
from typing import Callable, Generator, Iterator

from sqlalchemy import Column, DateTime, ForeignKey, Integer, String, create_engine
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import Session, relationship, scoped_session, sessionmaker

logger = logging.getLogger()

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


class DbHandler:
    def __init__(self, db_url: str) -> None:
        self._session_factory = scoped_session(
            sessionmaker(
                autocommit=False,
                autoflush=False,
                bind=create_engine(
                    db_url, connect_args={"check_same_thread": False}, echo=False
                ),
            )
        )

    @contextmanager
    def session(self) -> Generator[Session, None, None]:
        session: Session = self._session_factory()
        try:
            yield session
        except Exception:
            logger.exception("Session rollback because of exception")
            session.rollback()
            raise
        finally:
            session.close()


class AbstractRepository:
    def __init__(self, db: DbHandler) -> None:
        self._db = db


class UserRepository(AbstractRepository):
    def get_all(self) -> list[User]:
        with self._db.session() as db:
            return db.query(User).all()

    def create(self, name) -> User:
        user = User(name=name)
        with self._db.session() as db:
            db.add(user)
            db.commit()
            db.refresh(user)
            return user


class BookingRepository(AbstractRepository):
    def get_all(self) -> list[Booking]:
        with self._db.session() as db:
            return db.query(Booking).all()

    def create(self, startDate: datetime, endDate: datetime, userId: int) -> Booking:
        booking = Booking(startDate=startDate, endDate=endDate, userId=userId)
        with self._db.session() as db:
            db.add(booking)
            db.commit()
            db.refresh(booking)
            return booking
