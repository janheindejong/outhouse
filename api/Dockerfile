FROM python:3.11-slim

# Install and configure Poetry
RUN pip install poetry \
    && poetry config virtualenvs.create false

COPY poetry.lock pyproject.toml ./ 

RUN poetry install --without dev 

COPY outhouse outhouse 

EXPOSE 8000
CMD ["uvicorn", "outhouse.main:app", "--host", "0.0.0.0", "--port", "8000"]
