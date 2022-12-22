Hello,

Distributed transaction solution in micro-service base on eventually consistency, also an eventbus with Outbox pattern.
  --> DotNetCore.CAP

There is a microservice called ReportService.

Postgresql was used as the database. While communicating with the database, EntityFramework Core from ORM tools was used. Also Combining repository pattern and unitofwork using EntityFramework Core.
The project was developed as NLayerArchitecture and coded in accordance with SOLID.
The project was developed step by step in the development branch, committed and merged into the master branch.
RabbitMq, one of the Message Broker systems used for asynchronous communication of microservices, was used,
It was implemented over the RabbitMq package of DotNetCore.CAP.

The scenario here is a request to create a report is made through the Report service. A record is created to send to the database, but no SaveChanges. It is then saved in the ReportCreatedEvent event list. In a single transaction via UnitOfWork, the transaction database is sent and the event is published.
In ReportService, the ConsumerService class subscribes to the published events over CAP and performs the necessary operations.

Published and Received events are kept by the CAP framework in the Published and Received tables in the database.
In addition, the status of the events published with the dashboard integration of CAP is displayed on the dashboard (/cap-dashboard).

ReportService starts from port 5001 in Kestrel. It is specified in the Ports.txt document.
The migration structure of the project was developed as code first.

The database will be created when ReportService stands up.
Then, when you run the project for the 2nd time, the Published and Received tables of the CAP will be created.
Connection strings are found in the appsettings.json file of the services.
There are swagger integration of service in the project.

Thank You.