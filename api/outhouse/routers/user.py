from fastapi import APIRouter, Depends
from pydantic import BaseModel
from sqlalchemy.orm import Session

from ..config import get_db_session
from ..db import UserService

router = APIRouter()


class UserIn(BaseModel):
    name: str


class User(UserIn):
    id: int

    class Config:
        orm_mode = True


@router.get("/", response_model=list[User])
def get(session: Session = Depends(get_db_session)):
    return UserService(session).get_all()


@router.post("/", response_model=User)
def post(user: UserIn, session: Session = Depends(get_db_session)):
    return UserService(session).create(name=user.name)
