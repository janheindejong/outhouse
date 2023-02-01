from fastapi import FastAPI

from .routers.calendar import router as calendar_router
from .routers.root import router as root_router
from .routers.user import router as user_router


def get_app() -> FastAPI:
    app = FastAPI()
    app.include_router(root_router)
    app.include_router(user_router, prefix="/user")
    app.include_router(calendar_router, prefix="/calendar")
    return app


app = get_app()
