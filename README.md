# Screenshot SaaS

## Prerequisites

* Docker (and shared drive)
* .NET Core 2.2*
* Firefox*
* MongoDb*
* RabbitMQ*

*If you don't run it using the docker-compose files.

## How to run it

### Docker

Run Docker locally:

    start.ps1

or

    docker-compose -f .\docker-compose.infrastructure.yml -f docker-compose.yml build
    docker-compose -f .\docker-compose.infrastructure.yml -f docker-compose.yml up -d

If you want to run your services outside Docker and only with the infrastructure components you can run

    docker-compose -f .\docker-compose.infrastructure.yml -f up

### Endpoints

There are two endpoints exposed. 

    POST /api/batches
    GET  /api/screenshots

If you use the default settings it will be accessible through http://localhost:5000. You can see example requests in the Postman collection in the repository

`/api/batches`

`/api/batches` accepts a json body

    {
        "urls": [ 
            "https://github.com/",
            "https://www.google.com/"
        ]
    }

The specified URL:s will be screen captured. They will then eventually be available at `/api/screenshots`

## Architecture

The application consist of two services. API and Processor.

The API is responsible for exposing API endpoints to the client. The API is communicating with the Processor via asynchronous messages (a simple implementation of RabbitMQ is the one used here).

The Processor will process incoming messages of type `GenerateScreenshotMessage` and sceen capture the URL in the message by communicating with a selenium hub. The screenshot is then persisted in a MongoDB instance.

If you run the application via docker you will have access to Mongo Express to view the data in the database via `http://localhost:8080`. The name of the database is `Screenshots_dkr`.