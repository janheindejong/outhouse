import pathlib

import pytest
from fastapi.testclient import TestClient

from app.factories import UserInteractorFactory
from app.main import app, user_interface_factory


@pytest.fixture
def client(test_db_path: pathlib.Path):
    client = TestClient(app)
    app.dependency_overrides[user_interface_factory] = UserInteractorFactory(
        test_db_path
    )
    return client


def test_get_user_by_id(client: TestClient):
    resp = client.get("/user/1")
    assert resp.status_code == 200
    assert resp.json() == {"name": "Piet", "email": "piet@comp.com", "id": 1}


def test_get_user_by_email(client: TestClient):
    resp = client.get("/user", params={"email": "piet@comp.com"})
    assert resp.status_code == 200
    assert resp.json() == {"name": "Piet", "email": "piet@comp.com", "id": 1}


def test_post_user(client: TestClient):
    post_resp = client.post(
        "/user", json={"name": "John Doe", "email": "john.doe@comp.com"}
    )
    assert post_resp.status_code == 200
    get_resp = client.get("/user/3")
    assert get_resp.json() == {
        "name": "John Doe",
        "email": "john.doe@comp.com",
        "id": 3,
    }
