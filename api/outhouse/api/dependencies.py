from fastapi import Request

from ..db.db import DbHandler, Session


def get_db_session(request: Request) -> Session:
    db_handler: DbHandler = request.app.state.db_handler
    with db_handler.session() as session:
        return session
