from fastapi import APIRouter, Depends
from sqlalchemy.orm import Session

from ..db.services import UserService
from .dependencies import get_db_session
from .schemas import User, UserIn

router = APIRouter()


@router.get("/", response_model=list[User])
def get_all(session: Session = Depends(get_db_session)):
    return UserService(session).get_all()


@router.post("/", response_model=User)
def post(user: UserIn, session: Session = Depends(get_db_session)):
    return UserService(session).create(name=user.name)


@router.get("/{id}", response_model=User)
def get(id: int, session: Session = Depends(get_db_session)):
    return UserService(session).get(id)


@router.delete("/{id}", response_model=User)
def delete(id: int, session: Session = Depends(get_db_session)):
    return UserService(session).delete(id)
