# 📦 **Microservice-Based E-commerce Application**

## 🚀 **Overview**
This project is a **Microservice-Based E-commerce Application** designed to handle a scalable, robust, and efficient online shopping platform. Each service is independently developed, deployed, and managed, ensuring flexibility, reliability, and maintainability.

## 🛠️ **Technologies Used**
- **Backend:** .NET Core, Node.js
- **Real-Time Communication:** SignalR
- **Reverse Proxy:** Gateway Service (YARP)
- **Authentication & Authorization:** JWT, 2-Factor Authentication
- **Database:** PostgreSQL
- **Messaging Queue:** RabbitMQ
- **APIs:** RESTful APIs

---

## 📚 **Microservices Overview**

### 1️⃣ **User Service**
- **Features:**
  - User Registration
  - Login with JWT Authentication
  - Two-Factor Authentication
  - Forgot Password Flow

### 2️⃣ **Email Service**
- **Features:**
  - Send verification emails
  - Forgot password emails
  - Order confirmation emails

### 3️⃣ **Cart Service**
- **Features:**
  - Add/Remove items from the cart
  - Update item quantities
  - Retrieve cart details

### 4️⃣ **Product Service**
- **Features:**
  - Product listing and search
  - Product details
  - Inventory management

### 5️⃣ **Category Service**
- **Features:**
  - Category management
  - Fetch products by category

### 6️⃣ **Notification Service**
- **Features:**
  - Real-time notifications using SignalR

### 7️⃣ **Order Service**
- **Features:**
  - Order placement and tracking
  - Payment integration (Bkash sandbox)

### 8️⃣ **Chat Service**
- **Features:**
  - Real-time customer support chat

### 9️⃣ **Gateway Service (Reverse Proxy)**
- **Features:**
  - API Gateway for routing requests
  - Load balancing
  - Enhanced security

### 🔟 **Coupon Service**
- **Features:**
  - Coupon generation and validation
  - Discount management

