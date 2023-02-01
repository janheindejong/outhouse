FROM python:3.11-slim

# Install and configure Poetry
RUN pip install poetry \
    && poetry config virtualenvs.create false

# Set workdir to isolate files from 
WORKDIR /app

# Install dependencies
COPY poetry.lock pyproject.toml ./ 
RUN poetry install --without dev 

# Copy sources
COPY . .

# Configure how application should be run
ENV DB_URL "sqlite:///data/db.sqlite"
EXPOSE 8000
CMD ["uvicorn", "outhouse.main:app", "--host", "0.0.0.0", "--port", "8000"]
