from fastapi import APIRouter, Depends
from sqlalchemy.orm import Session

from ...db.services import UserService
from ..dependencies import get_db_session
from ..schemas import UserIn, UserInDB

router = APIRouter()


@router.get("/", response_model=list[UserInDB])
def get_all(session: Session = Depends(get_db_session)):
    return UserService(session).get_all()


@router.post("/", response_model=UserInDB)
def post(user: UserIn, session: Session = Depends(get_db_session)):
    return UserService(session).create(name=user.name)


@router.get("/{id}", response_model=UserInDB)
def get(id: int, session: Session = Depends(get_db_session)):
    return UserService(session).get(id)


@router.delete("/{id}", response_model=UserInDB)
def delete(id: int, session: Session = Depends(get_db_session)):
    return UserService(session).delete(id)
