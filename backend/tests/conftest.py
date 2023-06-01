import pytest

from app.db_adapters import SQLUserDbAdapter
from app.db_drivers import SQLiteConnection


@pytest.fixture
def test_db_conn(tmpdir):
    conn = SQLiteConnection(tmpdir / "db.sqlite")
    cur = conn.cursor()
    with open("./sql/create_test_db.sql") as f:
        operations = f.read().split(";")
    for operation in operations:
        cur.execute(operation)
    conn.commit()
    return conn


@pytest.fixture
def sql_user_db_adapter(test_db_conn: SQLiteConnection) -> SQLUserDbAdapter:
    return SQLUserDbAdapter(test_db_conn)
