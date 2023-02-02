from sqlalchemy import Column, DateTime, ForeignKey, Integer, String, Table
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import relationship

Base = declarative_base()


user_outhouse_association_table = Table(
    "user_outhouse_association_table",
    Base.metadata,
    Column("user_id", ForeignKey("users.id"), primary_key=True),
    Column("outhouse_id", ForeignKey("outhouses.id"), primary_key=True),
)


class User(Base):
    __tablename__ = "users"

    id = Column(Integer, primary_key=True)
    name = Column(String)
    bookings = relationship("Booking", cascade="all, delete")
    outhouses = relationship(
        "Outhouse", secondary=user_outhouse_association_table, back_populates="users"
    )


class Outhouse(Base):
    __tablename__ = "outhouses"

    id = Column(Integer, primary_key=True)
    name = Column(String)
    bookings = relationship("Booking", cascade="all, delete")
    users = relationship(
        "User", secondary=user_outhouse_association_table, back_populates="outhouses"
    )


class Booking(Base):
    __tablename__ = "bookings"

    id = Column(Integer, primary_key=True)
    startDate = Column(DateTime)
    endDate = Column(DateTime)
    userId = Column(Integer, ForeignKey("users.id"), nullable=False)
    outhouseId = Column(Integer, ForeignKey("outhouses.id"), nullable=False)
    user = relationship("User", back_populates="bookings")
    outhouse = relationship("Outhouse", back_populates="bookings")
