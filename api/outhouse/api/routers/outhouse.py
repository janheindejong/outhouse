from fastapi import APIRouter, Depends
from sqlalchemy.orm import Session

from ...db.services import OuthouseService
from ..dependencies import get_db_session
from ..schemas import Outhouse, OuthouseIn

router = APIRouter()


@router.get("/", response_model=list[Outhouse])
def get_all(session: Session = Depends(get_db_session)):
    return OuthouseService(session).get_all()


@router.post("/", response_model=Outhouse)
def post(outhouse: OuthouseIn, session: Session = Depends(get_db_session)):
    return OuthouseService(session).create(name=outhouse.name)


@router.get("/{id}", response_model=Outhouse)
def get(id: int, session: Session = Depends(get_db_session)):
    return OuthouseService(session).get(id)


@router.delete("/{id}", response_model=Outhouse)
def delete(id: int, session: Session = Depends(get_db_session)):
    return OuthouseService(session).delete(id)
