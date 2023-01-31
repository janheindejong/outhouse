FROM python:3.11-slim

# Install and configure Poetry
RUN pip install poetry \
    && poetry config virtualenvs.create false

WORKDIR /app

# Install dependencies
COPY poetry.lock pyproject.toml ./ 
RUN poetry install --without dev 

# Env var containing URL to database
ENV DB_URL "sqlite:///data/db.sqlite"

# Copy sources
COPY alembic alembic
COPY outhouse outhouse

EXPOSE 8000
CMD ["uvicorn", "outhouse.main:app", "--host", "0.0.0.0", "--port", "8000"]
