import pytest

from app.db_adapters import SQLUserDbAdapter
from app.db_drivers import SQLiteConnection


@pytest.fixture
def conn_to_tmp_db(tmpdir):
    conn = SQLiteConnection(tmpdir / "db.sqlite")
    cur = conn.cursor()
    with open("./sql/create_test_db.sql") as f:
        operations = f.read().split(";")
    for operation in operations:
        cur.execute(operation)
    conn.commit()
    return conn


@pytest.fixture
def user_db_adapter(conn_to_tmp_db: SQLiteConnection) -> SQLUserDbAdapter:
    return SQLUserDbAdapter(conn_to_tmp_db)


class TestCRUD:
    def test_create_users_return_value(self, user_db_adapter: SQLUserDbAdapter):
        id = user_db_adapter.create(name="Piet", email="piet@comp.com")
        assert id == 3

    def test_get_users(self, user_db_adapter: SQLUserDbAdapter):
        users = user_db_adapter.get_by_id(1), user_db_adapter.get_by_id(2)
        assert users == (
            {"name": "Piet", "id": 1, "email": "piet@comp.com"},
            {"name": "Kees", "id": 2, "email": "kees@comp.com"},
        )

    def test_unknown_user(self, user_db_adapter: SQLUserDbAdapter):
        user = user_db_adapter.get_by_id(3)
        assert user is None
