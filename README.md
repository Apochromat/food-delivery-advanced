# Food Delivery API Advanced
## Description
This is a food delivery API that allows users to order food from restaurants.
## Installation
1. Clone the repository
2. Install the requirements:
   - RabbitMQ 3.11+ (with Erlang 25.3+) 
   - PostgreSQL 15+
3. Fill databases connection strings into `ConnectionStrings` section of:
   - `Delivery.AuthAPI/appsettings.json`
   - `Delivery.BackendAPI/appsettings.json`
   - `Delivery.AdminPanel/appsettings.json`
   - `Delivery.NotificationAPI/appsettings.json`
4. Fill RabbitMQ connection string into `RabbitMQ` section of`:
   - `Delivery.BackendAPI/appsettings.json`
   - `Delivery.NotificationAPI/appsettings.json`
5. Fill `DefaultUsersConfig` section of `Delivery.AuthAPI/appsettings.json` with admin credentials
6. Run services:
   -  `Delivery.AuthAPI`
   -  `Delivery.BackendAPI`
   -  `Delivery.Notification`
   -  `Delivery.Notification.Client`
   -  `Delivery.AdminPanel`
7. Login or press "n" in `Delivery.Notification.Client` to begin listening for notifications
