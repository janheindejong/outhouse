FROM python:3.11-slim

# Set workdir to isolate files from 
WORKDIR /app

# Install dependencies
COPY requirements.txt requirements.txt 
RUN pip install -r requirements.txt --root-user-action=ignore --no-cache-dir

# Copy sources
COPY . .

# Configure how application should be run
ENV DB_URL "sqlite:///data/db.sqlite"
EXPOSE 8000
CMD ["uvicorn", "outhouse.main:app", "--host", "0.0.0.0", "--port", "8000"]
