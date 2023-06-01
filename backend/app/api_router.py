from fastapi import APIRouter, Depends
from .managers import UserManager, UserDbAdapter
from .config import config, Config
from .db_adapters import SQLConnection, SQLUserDbAdapter
from .db_drivers import SQLiteConnection

from pydantic import BaseModel, EmailStr


class UserIn(BaseModel):
    name: str
    email: EmailStr


class User(UserIn):
    id: int


router = APIRouter()


def get_config() -> Config:
    return config


def get_db_connection(config: Config = Depends(get_config)):
    conn = SQLiteConnection(config.DB_URL)
    try:
        yield conn
    finally:
        conn.close()


def get_db_handler(conn: SQLConnection = Depends(get_db_connection)) -> UserDbAdapter:
    return SQLUserDbAdapter(conn)


def get_user_controller(db: UserDbAdapter = Depends(get_db_handler)) -> UserManager:
    return UserManager(db)


@router.get("/user/{id}", response_model=User)
def get_user(id: int, user_controller: UserManager = Depends(get_user_controller)):
    return user_controller.get_by_id(id)


@router.post("/user")
def post_user(
    user: UserIn, user_controller: UserManager = Depends(get_user_controller)
):
    return user_controller.create(name=user.name, email=user.email)
