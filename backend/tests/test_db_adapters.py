import pytest

from app.db_adapters import SQLUserDbAdapter
from app.db_drivers import SQLiteConnection

from typing import Tuple


@pytest.fixture
def conn_to_tmp_db(tmpdir):
    conn = SQLiteConnection(tmpdir / "db.sqlite")
    cur = conn.cursor()
    cur.execute(
        """
        CREATE TABLE user (
            id INTEGER PRIMARY KEY, 
            name TEXT NOT NULL
        )
        """
    )
    conn.commit()
    return conn


@pytest.fixture
def user_db_adapter(conn_to_tmp_db: SQLiteConnection) -> SQLUserDbAdapter:
    return SQLUserDbAdapter(conn_to_tmp_db)


class TestCRUD:
    @pytest.fixture(autouse=True)
    def create_users(self, user_db_adapter: SQLUserDbAdapter):
        user_db_adapter.create(name="Piet")
        user_db_adapter.create(name="Kees")

    def test_create_users_return_value(self, user_db_adapter: SQLUserDbAdapter):
        id = user_db_adapter.create(name="Piet")
        assert id == 3

    def test_get_users(self, user_db_adapter: SQLUserDbAdapter) -> Tuple[int, int, int]:
        users = user_db_adapter.get(1), user_db_adapter.get(2)
        assert users == ({"name": "Piet", "id": 1}, {"name": "Kees", "id": 2})
