FROM python:3.11-slim AS requirements

# Install poetry 
RUN pip install poetry

# Create requirements.txt
COPY pyproject.toml poetry.lock .
RUN poetry export -o /requirements.txt

# ---- PROD STAGE ----
FROM python:3.11-slim

# Install dependencies
COPY --from=requirements /requirements.txt ./
RUN pip install -r requirements.txt --root-user-action=ignore --no-cache-dir

# Copy sources
COPY . .

# Configure how application should be run
ENV DB_URL "sqlite:///data/db.sqlite"
EXPOSE 8000
CMD ["uvicorn", "app.app:app", "--host", "0.0.0.0", "--port", "8000"]
