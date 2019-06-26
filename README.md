# Screenshot SaaS

## Prerequisites

* Docker (and shared drive)
* .NET Core 2.2*
* Firefox*
* MongoDb*
* RabbitMQ*

*If you plan to run it locally and not with Docker

## Docker

Run Docker locally:

    start.ps1

or

    docker-compose -f .\docker-compose.infrastructure.yml -f docker-compose.yml build
    docker-compose -f .\docker-compose.infrastructure.yml -f docker-compose.yml up -d

If you want to run your services outside Docker and only with the infrastructure components you can run

    docker-compose -f .\docker-compose.infrastructure.yml -f up

## Endpoints

There are two endpoints exposed. 

    POST /api/batches
    GET  /api/screenshots

If you use the default settings it will be accessible through http://localhost:5000. You can see example requests in the Postman collection in the repository

`/api/batches` is an endpoint where you can submit URL:s that will be screen captured. They will then eventually be available at `/api/screenshots`

## Architecture

