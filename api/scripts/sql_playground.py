""" Script to play around with SQLAlchemy """
from datetime import datetime

from sqlalchemy import create_engine, event
from sqlalchemy.orm import sessionmaker

from outhouse.db.models import Base, Booking


engine = create_engine("sqlite:///:memory:", echo=True)
event.listen(engine, "connect", lambda c, _: c.execute("pragma foreign_keys=on"))
Base.metadata.create_all(engine)
date = datetime.now()
Session = sessionmaker(bind=engine)
session = Session()


booking = Booking(startDate=date, endDate=date, userId=123, outhouseId=123)
session.add(booking)
session.commit()
session.refresh(booking)

print(booking.__dict__)
