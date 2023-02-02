from contextlib import contextmanager
from typing import Generator

from sqlalchemy import create_engine
from sqlalchemy.orm import Session, sessionmaker

# SQLite by default doesn't enforce foreign keys. We can set this behavior like so,
# but it is a bit of a workaround, and is not compatible with other DB's, hence the
# exceptionhandling.
# @event.listens_for(Engine, "connect")
# def set_sqlite_pragma(dbapi_connection, connection_record):
#     try:
#         cursor = dbapi_connection.cursor()
#         cursor.execute("PRAGMA foreign_keys=ON")
#         cursor.close()
#     except ProgrammingError:
#         logger.info("Received programming error; nothing to be afraid of.")


class DbHandler:
    def __init__(self, db_url: str) -> None:
        self._session_factory = sessionmaker(
            autocommit=False,
            autoflush=False,
            bind=create_engine(db_url, echo=False),
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
