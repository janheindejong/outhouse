from datetime import datetime
from typing import Generic, Optional, Type, TypeVar

from sqlalchemy.orm import Session

from .models import Base, Booking, Outhouse, User

ModelType = TypeVar("ModelType", bound=Base)


class AbstractService(Generic[ModelType]):

    _model: Type[ModelType]

    def __init__(self, session: Session) -> None:
        self._session = session

    def get_all(self):
        return self._session.query(self._model).all()

    def get(self, id: int) -> Optional[ModelType]:
        return self._session.query(self._model).get(id)

    def delete(self, id: int) -> Optional[ModelType]:
        item = self._session.query(self._model).get(id)
        self._session.delete(item)
        self._session.commit()
        return item

    def _create(self, model: ModelType) -> ModelType:
        self._session.add(model)
        self._session.commit()
        self._session.refresh(model)
        return model


class UserService(AbstractService[User]):

    _model = User

    def create(self, name: str) -> User:
        user = self._model(name=name)
        return self._create(user)


class OuthouseService(AbstractService[Outhouse]):

    _model = Outhouse

    def create(self, name: str) -> Outhouse:
        outhouse = self._model(name=name)
        return self._create(outhouse)


class BookingService(AbstractService[Booking]):

    _model = Booking

    def get_by_user_and_outhouse_id(
        self, userId: Optional[int] = None, outhouseId: Optional[int] = None
    ) -> list[Booking]:
        kwargs = {}
        if outhouseId:
            kwargs["outhouseId"] = outhouseId
        if userId:
            kwargs["userId"] = userId
        return self._session.query(Booking).filter_by(**kwargs).all()

    def create(
        self, startDate: datetime, endDate: datetime, userId: int, outhouseId: int
    ) -> Booking:
        booking = self._model(
            startDate=startDate, endDate=endDate, userId=userId, outhouseId=outhouseId
        )
        return self._create(booking)
