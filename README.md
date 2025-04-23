# 🧩 Backend Development Task Collection

This repository includes four backend-focused tasks, each demonstrating different concepts: algorithmic logic, REST API development, Dockerization, and deployment using Google Cloud Run.

---

## 📂 Project Structure

```
/Task1_DenominationRoutine/          # ATM payout calculation routine (C# Console App)
/Task2_MinimalRestApi_CustomerManager/  # Minimal REST API with customer validation
/Task3_Dockerize/                    # Dockerization of the Customer Manager API
/Task4_Google_CloudDeployment/       # Deployment details and Cloud Run service URL
```

---

## 🟦 Task 1 - Denomination Routine

A C# console application that calculates valid ATM payout combinations using denominations:
- 10 EUR
- 50 EUR
- 100 EUR

### Features:
- Calculates payout options for amounts like 30, 50, 60, up to 980 EUR.
- Outputs sorted combinations.

---

## 🟩 Task 2 - Minimal REST API (Customer Manager)

.NET 9 Minimal API to manage customer records.

### Features:
- `GET /customers`: List all customers.
- `POST /customers`: Add customers with validation:
  - Fields: `FirstName`, `LastName`, `Age`, `Id`
  - Age ≥ 18, IDs unique, sorted by `LastName`, then `FirstName`
- Local data: `customers.json`
- Swagger UI enabled.

---

## 🐳 Task 3 - Dockerization of Customer Manager API

Containerizes the Customer Manager API from Task 2.

### Structure:
```
/0-BuildCommand.txt
/Dockerfile
/Program.cs
/Task3_Dockerize.csproj
/customers.json
```

### Build and Run:
```bash
docker build -t task3-customer-api .
docker run -d -p 8080:80 --name task3-customer-api-container task3-customer-api
```
Optional persistent mount:
```bash
docker run -d -p 8080:80 -v $(pwd)/customers.json:/app/customers.json --name task3-customer-api-container task3-customer-api
```

---

## ☁️ Task 4 - Google Cloud Deployment

Deployed on **Google Cloud Run**, project ID:

```
customer-api-180224641587
```

### Deployment URL:
```
https://customer-api-180224641587.us-central1.run.app/customers
```

Test using Postman.

### Deployment Steps:
```bash
docker build -t gcr.io/customer-api-180224641587/customer-api .
docker push gcr.io/customer-api-180224641587/customer-api
gcloud run deploy customer-api   --image gcr.io/customer-api-180224641587/customer-api   --platform managed   --region us-central1   --allow-unauthenticated
```

### Persistence Note:
No persistence on Cloud Run Free Tier. Use **Cloud Storage Buckets** (paid plan) for persistent data.

---

## 🧱 Design Philosophy

Kept minimal due to task scale. Can be refactored with:
- SOLID principles
- Clean architecture
- Design patterns

---

## 👤 Author

**Aydın Seçer**  
📧 asecer@yildiz.edu.tr  
🔗 GitHub: [github.com/aydinsecer](https://github.com/aydinsecer)
