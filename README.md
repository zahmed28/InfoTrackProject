# SEO Ranking Tracker
This document serves as the comprehensive technical guideline for the SEO Ranking Tracker project, which is part of the interview process for the senior C# developer role at Infotrack. Here, you'll find a detailed breakdown of all requirements, points of emphasis, and expectations for this task.

## Project Overview
Develop a lightweight and user-friendly application to automate the daily manual process of checking the Google search ranking of “www.infotrack.co.uk” for the keyword “land registry searches”. The application should display the positions (if any) within the top 100 search results where the URL is found.

## Functional Requirements:
1. Keyword and URL Input: Users must be able to enter a search phrase and a URL through a user interface.

2. Search Result Processing: The application should scrape the first 100 search results from Google for the provided keyword and identify the positions of the specified URL within these results.

3. Result Display: The application should display a string of numbers indicating the positions of the URL within the search results (e.g., “1, 10, 33”) or “0” if the URL is not found.

4. No Third-Party Libraries for Scraping: The logic to scrape Google search results should be custom-built without using third-party APIs or libraries.

# Technical Requirements:
This document outlines the interaction between a front-end application and a set of microservices through an API Gateway. The primary functionality provided is to allow users to determine the Google search ranking of specific keywords for a given website and to retrieve the history of search results.

## Components

### Front-end Application:
A user interface that accepts two inputs: a keyword and a website URL. It communicates with the backend services through the API Gateway.

### API Gateway (SearchService):
Acts as the entry point for the front-end application to interact with the backend microservices. It routes requests to the appropriate services and aggregates results.

### Microservices:

### GoogleWebScrapper Service:
 Takes a keyword and a website URL and returns the ranking of the website for the specified keyword on Google.
 ![image](https://github.com/zahmed28/SEORankingTracker/assets/86317150/aa36631b-2487-4ac3-a5b5-573d794a6a12)

![image](https://github.com/zahmed28/SEORankingTracker/assets/86317150/cc8b772a-f70e-44b2-ac24-1f50debb6aa1)

### SearchResultWrite Service:
Receives search ranking results and records them in a database for future reference.
![image](https://github.com/zahmed28/SEORankingTracker/assets/86317150/f8161c74-71c2-47e2-9dd1-46ec27384f58)

![image](https://github.com/zahmed28/SEORankingTracker/assets/86317150/418de68b-d6cc-49b5-8584-5d36dc8ec76d)

### SearchResultRead Service:
Retrieves a history of search results from the database.
![image](https://github.com/zahmed28/SEORankingTracker/assets/86317150/1301b1e2-8f37-4584-b402-4c6058578533)

![image](https://github.com/zahmed28/SEORankingTracker/assets/86317150/953db8a8-2fe6-44e6-95f1-38da3926fc4d)


## Technology Stack
●	Core Technology: C# .NET 8

●	Database: SQL Server Express with Entity Framework.

●	Rate Limiting: ASP.NET Core rate limiting middleware

●	Unit Testing: xUnit

●	SDKs/Libraries: MediatR, FluentValidation,FluentValidation.AspNetCore

●	Front End Framework : Angular 16	

●	Documentation: Swagger

## Development Considerations
●	Coding Best Practices: Following SOLID principles, clean code practices.

●	Unit Tests: Including test cases for all functionalities

●	Documentation: Comprehensive documentation of code, architecture, and design decisions.

●	Scalability & Maintainability: Ensuring that the platform can handle growth and is easy to maintain.

# Architectural Patterns:
●	Implementing CQRS (Command Query Responsibility Segregation) 

●	Domain-driven design, Gateway pattern.

## High Level Architecture: 

![image](https://github.com/zahmed28/SEORankingTracker/assets/86317150/822c5c0d-db3b-48cb-a358-5ab8292018f8)

## Workflow

### Search Operation:
The user enters a keyword and a website URL into the front-end application.

The front-end application sends a request to the API Gateway's Search action, carrying the keyword and URL.

The API Gateway forwards the request to the GoogleWebScrapper Service:

The service processes the request and determines the ranking of the specified website for the provided keyword on Google.
The service returns the ranking information back to the API Gateway.
Simultaneously, or subsequently (based on implementation), the API Gateway sends the ranking results to the SearchResultWrite Service:

This service takes the search results and stores them in the database.
A confirmation of recording is sent back to the API Gateway.
The API Gateway aggregates the responses (if needed) and sends the search ranking back to the front-end application.

![image](https://github.com/zahmed28/SEORankingTracker/assets/86317150/d9351483-7ec1-449f-860f-ef2e23600e5f)
![image](https://github.com/zahmed28/SEORankingTracker/assets/86317150/0a66946e-9bde-4780-82d6-6994fb740c0e)
![image](https://github.com/zahmed28/SEORankingTracker/assets/86317150/cb674f1b-bc0d-4431-84b7-b4c8237c2501)


### Search History Operation:
The user requests to view the search history from the front-end application.

The front-end application sends a request to the API Gateway's SearchHistory action.

The API Gateway forwards this request to the SearchResultRead Service.

The service retrieves the history of search results from the database.
It formats this data as required and returns it to the API Gateway.
The API Gateway sends the search history data back to the front-end application for display to the user.

![image](https://github.com/zahmed28/SEORankingTracker/assets/86317150/d21bf65a-a8ff-484f-9322-a150ab57b9f7)
![image](https://github.com/zahmed28/SEORankingTracker/assets/86317150/fa968f6e-6c4a-41f1-853d-f31e45d1f648)


# Database Script:
```

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'SEORankingTracker')
BEGIN
    
    CREATE DATABASE SEORankingTracker;
END
GO

USE SEORankingTracker;
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'SearchResults' AND type = 'U')
BEGIN   
    CREATE TABLE SearchResults (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Query VARCHAR(1024),
        ResultURL VARCHAR(2048),
        RankingIndices  VARCHAR(255),
        DateCreated DATETIME DEFAULT GETDATE()
    );
END
GO
```
# Front End App Setup

1.Ensure you have npm and Node.js installed on your system.

2.Open the folder SPA/my-angular-app.

3.Run 'npm i' to install dependencies and 'npm update' to update them.
Note: If you get unable to resolve dependencies error. Please delete package-lock.json file and start step 3.

4.Execute 'ng serve' to start the front-end server.

5.Access the application by navigating to localhost:4200 in your web browser.

# Back-End Services Setup
1.Open Services folder and open Services.sln

2.Build Services.sln and Run

3.Open APIGateway folder and open APIGateway.sln

4.Build APIGateway solution and run

# Enhancements:
Capability to remove records directly from the front-end interface.

Implementation of an authentication system, for example, Azure Active Directory.

Analysis of weekly trends for specific search terms or URLs.

Transmission of logs to a database for subsequent analysis.

Addition of more integration tests to cover edge cases.






