import pathlib

import pytest

from app.adapters import SQLMembershipDbAdapter, SQLUserDbAdapter
from app.drivers import SQLiteConnection


@pytest.fixture
def test_db_path(tmpdir: pathlib.Path):
    path = tmpdir / "db.sqlite"
    conn = SQLiteConnection(path)
    cur = conn.cursor()
    with open("./sql/create_test_db.sql") as f:
        operations = f.read().split(";")
    for operation in operations:
        cur.execute(operation)
    conn.commit()
    conn.close()
    return path


@pytest.fixture
def sql_user_db_adapter(test_db_path: pathlib.Path) -> SQLUserDbAdapter:
    return SQLUserDbAdapter(SQLiteConnection(test_db_path))


@pytest.fixture
def sql_membership_db_adapter(test_db_path: pathlib.Path) -> SQLMembershipDbAdapter:
    return SQLMembershipDbAdapter(SQLiteConnection(test_db_path))
