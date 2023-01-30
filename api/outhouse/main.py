from fastapi import FastAPI

from .db import DbHandler
from .schemas import Booking, BookingIn

app = FastAPI()
db = DbHandler()


@app.get("/")
def home():
    return "Hello, Willie!"


@app.get("/booking", response_model=list[Booking])
def get_all_bookings():
    return db.read()


@app.post("/booking", response_model=Booking)
def post_booking(booking: BookingIn):
    booking_id = db.create(booking.dict())
    return {**{"bookingId": booking_id}, **booking.dict()}
