import pathlib

import pytest
from fastapi.testclient import TestClient

from app.app import app
from app.db_drivers import SQLiteConnection
from app.routers import get_db_connection


@pytest.fixture
def client(test_db_path: pathlib.Path):
    client = TestClient(app)

    def get_test_db_conn():
        conn = SQLiteConnection(test_db_path)
        try:
            yield conn
        finally:
            conn.close()

    app.dependency_overrides[get_db_connection] = get_test_db_conn
    return client


def test_get_user_by_id(client: TestClient):
    resp = client.get("/v1/user/1")
    assert resp.status_code == 200
    assert resp.json() == {"name": "Piet", "email": "piet@comp.com", "id": 1}


def test_post_user(client: TestClient):
    post_resp = client.post(
        "/v1/user", json={"name": "John Doe", "email": "john.doe@comp.com"}
    )
    assert post_resp.status_code == 200
    get_resp = client.get("/v1/user/3")
    assert get_resp.json() == {
        "name": "John Doe",
        "email": "john.doe@comp.com",
        "id": 3,
    }
