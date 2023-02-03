from fastapi import APIRouter, Depends

from ...db.services import OuthouseService
from ..dependencies import get_service
from ..schemas import OuthouseIn, OuthouseInDB

router = APIRouter()


@router.get("/", response_model=list[OuthouseInDB])
def get_all(service: OuthouseService = Depends(get_service(OuthouseService))):
    return service.get_all()


@router.post("/", response_model=OuthouseInDB)
def post(
    outhouse: OuthouseIn,
    service: OuthouseService = Depends(get_service(OuthouseService)),
):
    return service.create(name=outhouse.name)


@router.get("/{id}", response_model=OuthouseInDB)
def get(id: int, service: OuthouseService = Depends(get_service(OuthouseService))):
    return service.get(id)


@router.delete("/{id}", response_model=OuthouseInDB)
def delete(id: int, service: OuthouseService = Depends(get_service(OuthouseService))):
    return service.delete(id)
