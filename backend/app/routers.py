from fastapi import APIRouter, Depends, HTTPException
from pydantic import BaseModel, EmailStr

from .config import config
from .db_adapters import SQLConnection, SQLUserDbAdapter
from .db_drivers import SQLiteConnection
from .managers import UserDbAdapter, UserManager


class UserIn(BaseModel):
    name: str
    email: EmailStr


class User(UserIn):
    id: int


router = APIRouter()


def get_db_connection():
    conn = SQLiteConnection(config.DB_URL)
    try:
        yield conn
    finally:
        conn.close()


def get_db_adapter(conn: SQLConnection = Depends(get_db_connection)) -> UserDbAdapter:
    return SQLUserDbAdapter(conn)


def get_user_controller(db: UserDbAdapter = Depends(get_db_adapter)) -> UserManager:
    return UserManager(db)


@router.get("/user", response_model=User)
def get_user_by_email(
    email: EmailStr, user_controller: UserManager = Depends(get_user_controller)
):
    user = user_controller.get_by_email(email)
    if not user:
        raise HTTPException(404)
    return user


@router.get("/user/{id}", response_model=User)
def get_user(id: int, user_controller: UserManager = Depends(get_user_controller)):
    user = user_controller.get_by_id(id)
    if not user:
        raise HTTPException(404)
    return user


@router.post("/user")
def post_user(
    user: UserIn, user_controller: UserManager = Depends(get_user_controller)
):
    return user_controller.create(name=user.name, email=user.email)
