from fastapi import FastAPI

from .config import DB_URL
from .db.db import DbHandler
from .routers.root import router


def get_app() -> FastAPI:
    app = FastAPI()
    app.state.db_handler = DbHandler(DB_URL)
    app.include_router(router)
    return app


app = get_app()
