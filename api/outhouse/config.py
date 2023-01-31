import os

from sqlalchemy.orm import Session

from .db import DbHandler

db_handler = DbHandler(os.getenv("OUTHOUSE_DB_URL", "sqlite:///data/db.sqlite"))


def get_db_session() -> Session:
    with db_handler.session() as session:
        return session
