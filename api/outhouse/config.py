import os

from sqlalchemy.orm import Session

from .db import DbHandler

db_url = os.getenv("DB_URL", "sqlite:///data/db.sqlite?check_same_thread=false")

db_handler = DbHandler(db_url)


def get_db_session() -> Session:
    with db_handler.session() as session:
        return session
