from app.db_adapters import SQLUserDbAdapter
from app.entities import User


def test_create_user_return_value(sql_user_db_adapter: SQLUserDbAdapter):
    result = sql_user_db_adapter.create_user(name="Karel", email="karel@comp.com")
    assert result == User(**{"name": "Karel", "id": 3, "email": "karel@comp.com"})


def test_create_user(sql_user_db_adapter: SQLUserDbAdapter):
    sql_user_db_adapter.create_user(name="Karel", email="karel@comp.com")
    user_in_db = sql_user_db_adapter.get_user_by_id(3)
    assert user_in_db == User(**{"name": "Karel", "id": 3, "email": "karel@comp.com"})


def test_get_user_by_id(sql_user_db_adapter: SQLUserDbAdapter):
    users = sql_user_db_adapter.get_user_by_id(1), sql_user_db_adapter.get_user_by_id(2)
    assert users == (
        User(**{"name": "Piet", "id": 1, "email": "piet@comp.com"}),
        User(**{"name": "Kees", "id": 2, "email": "kees@comp.com"}),
    )


def test_get_user_by_email(sql_user_db_adapter: SQLUserDbAdapter):
    user = sql_user_db_adapter.get_user_by_email("piet@comp.com")
    assert user == User(**{"name": "Piet", "id": 1, "email": "piet@comp.com"})


def test_unknown_user(sql_user_db_adapter: SQLUserDbAdapter):
    user = sql_user_db_adapter.get_user_by_id(3)
    assert user is None
