import os

from fastapi import Depends, FastAPI, HTTPException
from fastapi.middleware.cors import CORSMiddleware
from pydantic import BaseModel, EmailStr

from .factories import UserInteractorFactory
from .interactors import UserInteractor

# Load environment
DB_URL = os.getenv("DB_URL", "./data/db.sql")

# Instantiate UserInterfaceFactory
user_interface_factory = UserInteractorFactory(DB_URL)


# Define app models
class UserIn(BaseModel):
    name: str
    email: EmailStr


class User(UserIn):
    id: int


# Instantiate app
app = FastAPI()
app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)


@app.get("/user", response_model=User)
def get_user_by_email(
    email: EmailStr, user_interactor: UserInteractor = Depends(user_interface_factory)
):
    user = user_interactor.get_by_email(email)
    if not user:
        raise HTTPException(404)
    return user


@app.get("/user/{id}", response_model=User)
def get_user(
    id: int, user_interactor: UserInteractor = Depends(user_interface_factory)
):
    user = user_interactor.get_by_id(id)
    if not user:
        raise HTTPException(404)
    return user


@app.post("/user")
def post_user(
    user: UserIn, user_interactor: UserInteractor = Depends(user_interface_factory)
):
    return user_interactor.create(name=user.name, email=user.email)
