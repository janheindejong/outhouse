from fastapi import APIRouter, Depends

from ...db.services import UserService
from ..dependencies import get_service
from ..schemas import UserIn, UserInDB

router = APIRouter()


@router.get("/", response_model=list[UserInDB])
def get_all(service: UserService = Depends(get_service(UserService))):
    return service.get_all()


@router.post("/", response_model=UserInDB)
def post(user: UserIn, service: UserService = Depends(get_service(UserService))):
    return service.create(name=user.name)


@router.get("/{id}", response_model=UserInDB)
def get(id: int, service: UserService = Depends(get_service(UserService))):
    return service.get(id)


@router.delete("/{id}", response_model=UserInDB)
def delete(id: int, service: UserService = Depends(get_service(UserService))):
    return service.delete(id)
