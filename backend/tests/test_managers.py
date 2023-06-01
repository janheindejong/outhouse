import pytest
from app.managers import UserManager, UserDbAdapter
from app.entities import User
from unittest.mock import create_autospec


USER = {"name": "John Doe", "id": 123, "email": "john.doe@company.com"}


@pytest.fixture()
def user_db() -> UserDbAdapter:
    return create_autospec(UserDbAdapter)


@pytest.fixture()
def user_manager(user_db: UserDbAdapter) -> UserManager:
    return UserManager(user_db)


def test_create_user(user_manager: UserManager, user_db: UserDbAdapter):
    user_db.create.return_value = 123
    user = user_manager.create(USER["name"], USER["email"])
    user_db.create.assert_called_once_with(USER["name"], USER["email"])
    assert user == User(**USER)


def test_get_user_by_id(user_manager: UserManager, user_db: UserDbAdapter):
    user_db.get_by_id.return_value = USER
    user = user_manager.get_by_id(123)
    user_db.get_by_id.assert_called_once_with(123)
    assert user == User(**USER)


def test_unknown_user(user_manager: UserManager, user_db: UserDbAdapter):
    user_db.get_by_id.return_value = None
    assert user_manager.get_by_id(1234) is None
