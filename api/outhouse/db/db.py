from contextlib import contextmanager
from typing import Generator

from sqlalchemy import create_engine
from sqlalchemy.orm import Session, sessionmaker


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
