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

    docker-compose -f .\docker-compose.infrastructure.yml up -d

### Endpoints

There are two endpoints exposed. 

    POST /api/batches
    GET  /api/screenshots

If you use the default settings it will be accessible at `http://localhost:5000`. You can see example requests in the Postman collection `Screenshot SaaS.postman_collection.json` in the repository.

`/api/batches` accepts a json body

    {
        "urls": [ 
            "https://github.com/",
            "https://www.google.com/"
        ]
    }

The specified URL:s will be screen captured. They will then eventually be available at `/api/screenshots`

    {
        "count": 1,
        "screenshots": [
            {
                "data": "iVBORw0KGgoAA"
            }
         ]
    }

## Architecture

The application consist of two backend services: API and Processor.

The API is responsible for exposing API endpoints to the client. The API is communicating with the Processor via asynchronous messages (a simple implementation of RabbitMQ is used).

The Processor will process incoming messages of type `GenerateScreenshotMessage` and screen capture the URL in the message by communicating with a selenium hub. The screenshot is then persisted in a MongoDB instance.

If you run the application via docker you will have access to Mongo Express to view the data in the database at `http://localhost:8080`. The name of the database is `Screenshots_dkr`.

### Scaling

This architecture allows the application to be scaled horizontally to support high load. The heavy lifting will be handled by the Processor. This service could easily be scaled to handle many messages. In the docker example the screenshot processing is done using [Selenium-Grid](https://www.seleniumhq.org/docs/07_selenium_grid.jsp). With this setup you can run screenshot processing in parallel using multiple nodes.

To try this out you can start the services using the following docker-compose command

    docker-compose -f .\docker-compose.infrastructure.yml -f .\docker-compose.yml up --scale screenshot-processor=3 --scale firefoxnode=3

This will create three instances of `screenshot-processor` and three instances of `firefoxnode`. The work  will be distributed among those nodes.

#### Things to consider

With this setup only one instance of MongoDB is used. To make sure that this won't be the bottleneck replicas can be used. If hosted in any cloud provider (Azure, AWS or GCP) their blob storages can be used.

No load balancer is used in this example application for simplicity. In a real world scenario nginx or any other good load balancer should be used to handle load properly.

The message queue implementation of RabbitMQ is really simplified, just to examplify decoupling between the services and make them autonomous and subject for scaling. In a real world scenario this should be configured using multiple nodes to form a cluster. That will ensure high availability of queues and increase the throughput. Resiliency of connection and retries for messages should also be implemented.

In the API when requesting the screenshots the response is not very optimized now. Several improvements could be done here such as filtering and pagination.