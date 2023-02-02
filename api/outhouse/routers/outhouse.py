from fastapi import APIRouter, Depends
from pydantic import BaseModel
from sqlalchemy.orm import Session

from ..db.services import OuthouseService
from .dependencies import get_db_session


class OuthouseIn(BaseModel):
    name: str


class Outhouse(OuthouseIn):
    id: int

    class Config:
        orm_mode = True


router = APIRouter()


@router.get("/", response_model=list[Outhouse])
def get_bookings(session: Session = Depends(get_db_session)):
    return OuthouseService(session).get_all()


@router.post("/", response_model=Outhouse)
def post_booking(outhouse: OuthouseIn, session: Session = Depends(get_db_session)):
    return OuthouseService(session).create(name=outhouse.name)
