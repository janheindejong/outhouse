FROM python:3.11-alpine 

RUN pip install poetry 

COPY poetry.lock pyproject.toml ./ 

RUN poetry install --without dev 

COPY outhouse outhouse 

EXPOSE 8000
CMD ["poetry", "run", "uvicorn", "outhouse.main:app"]
