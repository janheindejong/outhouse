from fastapi import FastAPI

from .api.routers.root import router
from .config import DB_URL
from .db.db import DbHandler


def get_app() -> FastAPI:
    app = FastAPI()
    app.state.db_handler = DbHandler(DB_URL)
    app.include_router(router)
    return app


app = get_app()
