# SoloWifi Billing API

**SoloWifi Billing** loyihasi — Docker yordamida ishlaydigan WebAPI, Kafka va PostgreSQL bilan integratsiyalangan tizim. WebAPI Kafka orqali `order-paid` topic-dan xabarlarni oladi va ma’lumotlar bazasini yangilaydi.

---

## Talablar

- Docker ≥ 20.x
- Docker Compose ≥ 1.29
- .NET 8 SDK (agar local mashinadan run qilmoqchi bo‘lsangiz)

---

## Loyihani ishga tushirish

### 1️⃣ Docker Compose orqali

```bash
docker-compose up -d
