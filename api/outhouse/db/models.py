from sqlalchemy import Column, DateTime, ForeignKey, Integer, String
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import relationship

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
