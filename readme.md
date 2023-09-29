# AirTravel Management System

This repository contains the AirTravel Management System, a web service designed to manage airTravel-related data. The system is built using .NET 7 and utilizes a PostgreSQL database for efficient data storage. You can easily set up and deploy the system using Docker.

## Table of Contents

- [Introduction](#introduction)
- [Technologies Used](#technologies-used)
- [Features](#features)
- [Installation](#installation)
- [Usage](#usage)
- [Contributors](#contributors)
- [License](#license)

## Introduction

The AirTravel Management System is a powerful tool for handling flight-related information, including flight tickets, passengers, and documents. It offers various functionalities to create, read, update, and delete records, as well as generate reports for passengers during specific time periods.

## Technologies Used

- **Backend**: .NET 7 / C#
- **Database**: PostgreSQL
- **Deployment**: Docker

## Features

The AirTravel Management System provides the following essential features:

- **Read List of Tickets**: Retrieve a list of flight tickets.
- **Read List of Tickets by Passenger**: Fetch a list of flight tickets for a specific passenger.
- **Read List of Documents by Passenger**: Access a list of documents for a specific passenger.
- **Read Full Ticket Information**: Retrieve detailed information about a flight ticket, including passenger and document details.
- **Edit Ticket Information**: Update flight ticket information.
- **Edit Passenger Information**: Modify passenger details.
- **Edit Document Information**: Edit document details.
- **Delete Ticket Information**: Remove flight ticket information from the system.
- **Delete Passenger Information**: Delete passenger information.
- **Delete Document Information**: Delete document details.
- **Generate Passenger Report**: Generate a report for a passenger within a specified time period. This report includes flights booked and flown during that period.

## Installation

To set up the AirTravel Management System, follow these steps:

1. Clone this repository to your local machine.

2. Install Docker on your machine if it is not already installed.

3. Navigate to the project root directory in your terminal.

4. Run the following Docker Compose command to start the project:

    ```bash
    docker-compose up -d
    ```

   This will build and start the application containers. You can access the application by navigating to http://localhost in your web browser.

## Usage

1. Use the provided API endpoints to interact with the AirTravel Management System. Documentation for these endpoints can be found at [API Documentation](#).

2. Implement the desired functionalities in your web application or client as per your specific requirements.

3. Explore the various features and reports to efficiently manage flight-related data.

## Contributors

- Alexey Ionov

## License

This project is licensed under the [MIT License](LICENSE).

---