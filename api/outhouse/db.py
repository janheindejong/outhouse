bookings: list[dict] = []


class DbHandler:
    def read(self) -> list[dict]:
        return bookings

    def create(self, booking: dict) -> int:
        booking["bookingId"] = len(bookings) + 1
        bookings.append(booking)
        return booking["bookingId"]
