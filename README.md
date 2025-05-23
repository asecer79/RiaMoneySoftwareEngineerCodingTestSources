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

## ➡️ API Usage Details (Local & Google Cloud Run)

### 🟢 GET /customers
Retrieve all customer records.

#### ✅ Local Example:
```
GET http://localhost:8080/customers
```

#### ✅ Cloud Run Example:
```
GET https://customer-api-180224641587.us-central1.run.app/customers
```

#### 📤 Example Response:
```json
[
  {
    "firstName": "John",
    "lastName": "Doe",
    "age": 28,
    "id": 1
  },
  {
    "firstName": "Jane",
    "lastName": "Smith",
    "age": 35,
    "id": 2
  }
]
```

---

### 🟠 POST /customers
Add new customers with validation.

#### ✅ Local Example:
```
POST http://localhost:8080/customers
```

#### ✅ Cloud Run Example:
```
POST https://customer-api-180224641587.us-central1.run.app/customers
```

#### 📥 Example Request Body (JSON):
```json
[
  {
    "firstName": "Alice",
    "lastName": "Johnson",
    "age": 25,
    "id": 3
  },
  {
    "firstName": "Bob",
    "lastName": "Williams",
    "age": 40,
    "id": 4
  }
]
```

#### 🟢 Successful Response:
```json
{
  "added": 4,
  "errors": []
}
```

#### ❌ Error Response Example (if validation fails):
```json
{
  "added": 4,
  "errors": [
    "Customer under 18: Alice Johnson",
    "Duplicate ID: 2"
  ]
}
```

---

### ⚠️ Validation Rules:
- All fields (`firstName`, `lastName`, `age`, `id`) are required.
- `age` must be 18 or older.
- `id` must be unique.
- Customers are inserted sorted by last name, then first name.

- ---

## 🧱 Design Philosophy

The current implementation has been intentionally kept **minimal** due to the scale and requirements of the tasks.  
However, if needed, the solution can be **refactored, extended, and redesigned** using modern software engineering principles and patterns, including but not limited to:

- **SOLID principles**  
- **Clean Architecture**  
- **Design Patterns** (such as Repository, Factory, Dependency Injection)  
- **Onion Architecture**  
- **CQRS (Command Query Responsibility Segregation)**  
- **Event-Based Architecture**  
- **Microservices or Modular Monolith approaches**  
- Or **any other technology or architectural style** required for scaling, maintainability, or specific business needs.

> 💡 I am fully capable of adapting the solution to any of these architectures or technologies upon request.





## 👤 Author

**Aydın Seçer**  
📧 asecer@yildiz.edu.tr  
🔗 GitHub: [github.com/asecer79](https://github.com/asecer79)
