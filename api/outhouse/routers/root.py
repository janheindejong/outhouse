from fastapi import APIRouter

from . import calendar, user

router = APIRouter()


@router.get("/")
def root():
    return "Hello, Willie!"


router.include_router(user.router, prefix="/user")
router.include_router(calendar.router, prefix="/calendar")
