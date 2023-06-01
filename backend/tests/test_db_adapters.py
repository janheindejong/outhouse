from app.db_adapters import SQLUserDbAdapter


def test_create_users_return_value(sql_user_db_adapter: SQLUserDbAdapter):
    id = sql_user_db_adapter.create(name="Piet", email="piet@comp.com")
    assert id == 3


def test_get_users(sql_user_db_adapter: SQLUserDbAdapter):
    users = sql_user_db_adapter.get_by_id(1), sql_user_db_adapter.get_by_id(2)
    assert users == (
        {"name": "Piet", "id": 1, "email": "piet@comp.com"},
        {"name": "Kees", "id": 2, "email": "kees@comp.com"},
    )


def test_unknown_user(sql_user_db_adapter: SQLUserDbAdapter):
    user = sql_user_db_adapter.get_by_id(3)
    assert user is None
