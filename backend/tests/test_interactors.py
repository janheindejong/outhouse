import pytest

from app.adapters import SQLMembershipDbAdapter, SQLUserDbAdapter
from app.interactors import UserInteractor


@pytest.fixture()
def user_manager(
    sql_user_db_adapter: SQLUserDbAdapter,
    sql_membership_db_adapter: SQLMembershipDbAdapter,
) -> UserInteractor:
    return UserInteractor(sql_user_db_adapter, sql_membership_db_adapter)


def test_create_user(user_manager: UserInteractor):
    user = user_manager.create("John Doe", "john.doe@comp.com")
    assert user == {"name": "John Doe", "email": "john.doe@comp.com", "id": 3}


def test_get_user_by_id(user_manager: UserInteractor):
    user = user_manager.get_by_id(1)
    assert user == {"name": "Piet", "email": "piet@comp.com", "id": 1}


def test_get_user_by_email(user_manager: UserInteractor):
    user = user_manager.get_by_email("piet@comp.com")
    assert user == {"name": "Piet", "email": "piet@comp.com", "id": 1}


def test_unknown_user(user_manager: UserInteractor):
    assert user_manager.get_by_id(1234) is None
