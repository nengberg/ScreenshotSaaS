# Screenshot SaaS

## Prerequisites

* Docker (and shared drive)
* .NET Core 2.2*
* Firefox installed*
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


### Architecture

