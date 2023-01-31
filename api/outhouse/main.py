import os

from fastapi import FastAPI

from .db import BookingRepository, DbHandler, UserRepository
from .schemas import Booking, BookingIn, User, UserIn

# Create app
app = FastAPI()

# Create database handlers
db = DbHandler(os.getenv("OUTHOUSE_DB_URL", "sqlite:///data/db.sqlite"))
user_repository = UserRepository(db)
booking_repository = BookingRepository(db)


@app.get("/")
def home():
    return "Hello, Willie!"


@app.get("/user", response_model=list[User])
def get_users():
    return user_repository.get_all()


@app.post("/user", response_model=User)
def post_user(user: UserIn):
    return user_repository.create(name=user.name)


@app.get("/booking", response_model=list[Booking])
def get_bookings():
    return booking_repository.get_all()


@app.post("/booking", response_model=Booking)
def post_booking(booking: BookingIn):
    return booking_repository.create(
        startDate=booking.startDate, endDate=booking.endDate, userId=booking.userId
    )
