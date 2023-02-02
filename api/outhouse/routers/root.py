from fastapi import APIRouter

from . import calendar, user, outhouse

router = APIRouter()


@router.get("/")
def root():
    return "Hello, Willie!"


router.include_router(user.router, prefix="/user")
router.include_router(outhouse.router, prefix="/outhouse")
router.include_router(calendar.router, prefix="/calendar")
