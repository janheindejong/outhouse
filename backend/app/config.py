import os


class Config:
    def __init__(self) -> None:
        self.DB_URL = os.getenv(
            "DB_URL", "sqlite:///data/db.sqlite?check_same_thread=false"
        )


config = Config()
