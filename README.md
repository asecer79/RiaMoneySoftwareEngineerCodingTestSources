# 🧩 Project Collection: Backend Development Tasks

This repository contains four backend-focused tasks, each addressing different aspects of algorithmic problem-solving, API development, containerization, and deployment.

---

## 🟦 Task 1 - Denomination Routine
A C# console application that calculates all valid payout combinations using ATM denominations:
- 10 EUR
- 50 EUR
- 100 EUR

Sample amounts include: 30 EUR, 50 EUR, 60 EUR, 80 EUR, up to 980 EUR.

---

## 🟩 Task 2 - Minimal REST API (Customer Manager)
Minimal API developed in .NET 9 for managing customer records.

### Features:
- `GET /customers`: List all customers.
- `POST /customers`: Add customers with validation:
  - Fields required: `FirstName`, `LastName`, `Age`, `Id`
  - Age must be 18+
  - IDs must be unique
  - Sorted insertion by `LastName`, then `FirstName`
- Uses `customers.json` as the local data store.
- Swagger UI enabled for testing.

---

## 🐳 Task 3 - Dockerization: Minimal Customer API

### Project Structure:
```
/0-BuildCommand.txt          # Build and run instructions
/Dockerfile                  # Dockerfile for containerization
/Program.cs                  # Main API logic (Minimal API)
/Task3_Dockerize.csproj      # Project file
/customers.json              # Local JSON data store
```

### Build and Run Instructions:
```bash
docker build -t Task2_MinimalRestApi_CustomerManager .
docker run -d -p 8080:80 --name Task2_MinimalRestApi_CustomerManager-container Task2_MinimalRestApi_CustomerManager
```

API URL: `http://localhost:8080/customers`

#### Persistent Storage (Optional):
```bash
docker run -d -p 8080:80   -v $(pwd)/customers.json:/app/customers.json   --name Task2_MinimalRestApi_CustomerManager-container Task2_MinimalRestApi_CustomerManager
```

---

## ☁️ Task 4 - Google Cloud Deployment

The Dockerized Minimal REST API was deployed to **Google Cloud Run**.

### 🌐 Deployment URL:
```
https://customer-api-180224641587.us-central1.run.app/customers
```

> Please use **Postman** for testing.

### Deployment Commands (Used on Google Cloud Console):
```bash
docker build -t gcr.io/180224641587/Task2_MinimalRestApi_CustomerManager .
docker push gcr.io/180224641587/customer-api
gcloud run deploy customer-api   --image gcr.io/180224641587/Task2_MinimalRestApi_CustomerManager   --platform managed   --region us-central1   --allow-unauthenticated
```

### Persistence Notice:
I couldn't make `customers.json` persistent on Cloud Run because I couldn't purchase a bucket, but I could easily do it with the paid version.  
Recommended solution: **Google Cloud Storage Buckets**.

---

## 🧱 Design Philosophy
These projects are intentionally kept minimal. SOLID principles or design patterns (Repository, Factory, Dependency Injection) were not applied due to project scope.  
> If required, I am ready to restructure the code with any desired software architecture.

---

## 👤 Author

**Aydın Seçer**  
📧 asecer@yildiz.edu.tr  
🔗 [github.com/aydinsecer](https://github.com/asecer79)
