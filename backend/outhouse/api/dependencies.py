from typing import Callable, Type

from fastapi import Depends, Request

from ..db.db import DbHandler, Session
from ..db.services import AbstractService


def get_db_session(request: Request) -> Session:
    db_handler: DbHandler = request.app.state.db_handler
    with db_handler.session() as session:
        return session


def get_service(
    service_type: Type[AbstractService],
) -> Callable[[Session], AbstractService]:
    def _get_repo(session: Session = Depends(get_db_session)):
        return service_type(session)

    return _get_repo
