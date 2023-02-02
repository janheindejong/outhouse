import os

DB_URL = os.getenv("DB_URL", "sqlite:///data/db.sqlite?check_same_thread=false")
