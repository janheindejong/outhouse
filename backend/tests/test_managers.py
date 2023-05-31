import pytest
from app.managers import UserManager, UserDbAdapter
from app.entities import User
from unittest.mock import create_autospec


@pytest.fixture()
def user_db() -> UserDbAdapter:
    return create_autospec(UserDbAdapter)


@pytest.fixture()
def user_manager(user_db: UserDbAdapter) -> UserManager:
    return UserManager(user_db)


def test_create_user(user_manager: UserManager, user_db: UserDbAdapter):
    user_db.create_user.return_value = 123
    user = user_manager.create_user(name="John Doe")
    user_db.create_user.assert_called_once_with("John Doe")
    assert user == User(name="John Doe", id=123)


def test_get_user_by_id(user_manager: UserManager, user_db: UserDbAdapter):
    user_db.get_user.return_value = {"name": "John Doe", "id": 123}
    user = user_manager.get_user_by_id(123)
    user_db.get_user.assert_called_once_with(123)
    assert user == User(name="John Doe", id=123)
