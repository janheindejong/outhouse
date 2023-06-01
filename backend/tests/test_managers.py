import pytest

from app.db_adapters import SQLUserDbAdapter
from app.entities import User
from app.managers import UserManager


@pytest.fixture()
def user_manager(sql_user_db_adapter: SQLUserDbAdapter) -> UserManager:
    return UserManager(sql_user_db_adapter)


def test_create_user(user_manager: UserManager):
    user = user_manager.create("John Doe", "john.doe@comp.com")
    assert user == User(name="John Doe", email="john.doe@comp.com", id=3)


def test_get_user_by_id(user_manager: UserManager):
    user = user_manager.get_by_id(1)
    assert user == User(name="Piet", email="piet@comp.com", id=1)


def test_unknown_user(user_manager: UserManager):
    assert user_manager.get_by_id(1234) is None
