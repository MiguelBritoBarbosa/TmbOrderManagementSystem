# TMB Order Management System (Concept Test)

This project is part of the selection process for a Full-stack developer position at 
TMB Education. Your goal is to build an API using the Entity Framework and a front-end with React to consume it


### For testing
Before testing, make sure you have Docker installed on your machine.

1. Clone the project
2. configure the env files
   - in the front-end directory create a .env file and copy the values from the .env.example
   - The same must be done for the backend
3. run in both the backend and frontend directories: `docker compose up -d`
4. Access [http://localhost:3000](http://localhost:3000) to see the front-end application
5. You can access the swagger documentation of API in [http://localhost:8080/swagger/index.html
](http://localhost:8080/swagger/index.html)

<hr>

#### disclaimer
this project is using Service Bus from Microsoft Azure and have a limited time for the free license so if you cloning this repository after some time probably this functionality isen't working. You can test with your Azure account changin the AzureServiceBus key in appsettings.json

