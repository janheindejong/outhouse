import os


class Config:
    def __init__(self) -> None:
        self.DB_URL = os.getenv("DB_URL", "./data/db.sqlite")


config = Config()
