from fastapi import APIRouter

router = APIRouter()


@router.get("/")
def root():
    return "Hello, Willie!"