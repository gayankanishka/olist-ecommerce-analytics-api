# Olist Ecommerce Analytics API

A RESTful API to expose big data analytics on Olist E-Commerce data. Big data analytics were processed using Apache hadoop deployed on Azure. This module is only responsible for exposing the results which are stored in Azure Blob Storage. This was done as a part of a MSc module project.

## High-level architecture diagram

> Repository contains only the Analytics API module

![alt text](https://github.com/gayankanishka/olist-ecommerce-analytics-api/blob/main/docs/olist-e-commerce-analytics-platform-v1.2.png?raw=true)

What's included:

- [.NET 5.0](https://dotnet.microsoft.com/download/dotnet/5.0)
- [MediatR](https://github.com/jbogard/MediatR)
- [CsvHelper](https://joshclose.github.io/CsvHelper/)
- [Azure Blob Storage](https://azure.microsoft.com/en-us/services/storage/blob-storage/)
- [Swagger](https://swagger.io/)
- [WebHDFS](https://hadoop.apache.org/docs/r1.0.4/webhdfs.html)

## Table of Content

- [Quick Start](#quick-start)
  - [Prerequisites](#prerequisites)
  - [Development Environment Setup](#development-environment-setup)
  - [Build and run](#build-and-run-from-source)
- [License](#license)

## Quick Start

After setting up your local DEV environment, you can clone this repository and run the solution. Make sure all the other interconnected services are running on the cloud.

### Prerequisites

You'll need the following tools:

- [.NET](https://dotnet.microsoft.com/download), version `>=5`
- [Visual Studio](https://visualstudio.microsoft.com/), version `>=2019` or [JetBrains Rider](https://jetbrains.com/rider/), version `>=2020`
- [AzureStorageEmulator](https://azure.microsoft.com/en-us/services/storage/blob-storage/blob-storage-emulator/) or [Azurite](https://github.com/Azure/Azurite)
- [Azure Storage Explorer](https://azure.microsoft.com/en-us/services/storage/blob-storage/blob-storage-explorer/)

### Development Environment Setup

First clone this repository locally.

- Install all of the the prerequisite tools mentioned above.

### Build and run from source

With Visual studio:
Open up the solutions using Visual studio.

- Restore solution `nuget` packages.
- Rebuild solution once.
- Run the solution.
- Local swagger URL [here](https://localhost:5001/swagger).

## License

Licensed under the [MIT](LICENSE) license.
