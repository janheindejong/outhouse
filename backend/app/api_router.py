from fastapi import APIRouter, Depends
from .managers import UserManager, UserDbAdapter
from .db_drivers import SQLiteConnection
from .config import config, Config
from .db_adapters import SQLConnection, SQLUserDbAdapter

from pydantic import BaseModel


class UserIn(BaseModel):
    name: str


class User(UserIn):
    id: int


router = APIRouter()


def get_config() -> Config:
    return config


def get_db_connection(config: Config = Depends(get_config)) -> SQLConnection:
    return SQLiteConnection(config.DB_URL)


def get_db_handler(conn: SQLConnection = Depends(get_db_connection)) -> UserDbAdapter:
    return SQLUserDbAdapter(conn)


def get_user_controller(db: UserDbAdapter = Depends(get_db_handler)) -> UserManager:
    return UserManager(db)


@router.get("/user/{id}", response_model=User)
def get_user(id: int, user_controller: UserManager = Depends(get_user_controller)):
    return user_controller.get_user_by_id(id)


@router.post("/user")
def post_user(
    user: UserIn, user_controller: UserManager = Depends(get_user_controller)
):
    return user_controller.create_user(user.name)
