# Chapter 7 - Exercise 1
##  Objectives
- Learn how to use value objects
- Learn how to use entities
- Learn how to use **D**ata **T**ransfer **O**bjects
- Learn how to use FluentValidation
- Learn how to use global error handling middleware
- Learn how to use Swagger
- Learn how to use a in-memory database
- Learn how to use database agnostic triggers
- Learn how to setup a REST API

## Goal
The goal of this course is to create an e-commerce website with some CMS for administrators.
### Administrators can:
- Customers
    - Create 
    - Update
    - Get details for a specific customer
    - Get a paged list of all customers which is searchable
    - Place an order for a customer (sometimes we get orders by phone)
- Products
    - Create 
    - Update
    - Delete
    - Get details for a specific product
    - Get a paged list of all products which is searchable
    - Add a tag to a product
- Tags (Labels for products)
    - Create 
    - Update
    - Get a paged list of all tags

### Customers can:
- Products
    - Get details for a specific product
    - Get a paged list of all products which is searchable
    - Add them to their cart
    - Place an order

In this exercise we'll be focussing on setting-up a descent architecture and the back-end side of things. In the next chapter we'll implement the **U**ser **I**nterface using Blazor Web Assembly(WASM).

## Solution Structure
- We've used `dotnet blazorwasm --hosted -o BogusStore` to generate the solution and added a few class libraries.
- **BogusStore.Client**
    - There is nothing going on here, it's the default blazor wasm template. We'll implement the UI in the next chapter.
    - **You do not need to modify anything in this project**
- **BogusStore.Domain**
    - Is mostly implemented and for this exercise and might change a bit if we go further.
    - **You do not need to modify anything in this project**
- **BogusStore.Fakers**
    - Contains all the Fakers to be used in UnitTests and/or to seed the database.
    - Using these fakers can be benefitial when working with big test suites.
    - **You do not need to modify anything in this project**
- **BogusStore.Persistence**
    - Contains the in-memory database we'll be using in our services. Using Entity Framework Core, we can easily swap to a real database such as MySql, MS SQL, SQLite,...
    - You do not have to worry about this project at the moment, in the chapter "Big Data the new raw material" we'll discuss it. Just know that we can create, read, update and delete.
    - Since it's in-memory each time we restart the server, we'll go back to the initial state.
    - The initial state is provided by the `FakeSeeder` class which populates bogus data in the database using fakers.
    - **You do not need to modify anything in this project**
- **BogusStore.Server**
    - The REST-API which also serves our static Blazor Client files.
    - `ProductController` is already implemented and can be used as reference to implement other functionalities.
    - Controllers are kept "skinny", since we use the `BogusStore.Services` layer.
    - **You do need to modify this project**
- **BogusStore.Services**
    - Together with the `BogusStore.Domain` the heart of our business problem.
    - **You do need to modify this project**
- **BogusStore.Shared**
    - Contains Data Transfer Objects and contracts between the client and the server.
    - **You do need to modify this project**

## Packages used
- BogusStore.Domain
    - [Ardalis Guardclauses](https://github.com/ardalis/GuardClauses)
- BogusStore.Fakers
    - [Bogus](https://github.com/bchavez/Bogus)
- BogusStore.Persistence
    - [Entity Framework Core In-memory](https://learn.microsoft.com/en-us/ef/core/providers/in-memory/?tabs=dotnet-core-cli)
    - [EntityFrameworkCore Triggered](https://github.com/koenbeuk/EntityFrameworkCore.Triggered)
- BogusStore.Server
    - [Swashbuckle AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
    - [Swashbuckle AspNetCore Annotations](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
    - [MicroElements Swashbuckle FluentValidation](https://github.com/micro-elements/MicroElements.Swashbuckle.FluentValidation)
    - [FluentValidation AspNetCore](https://github.com/FluentValidation/FluentValidation.AspNetCore)
- BogusStore.Shared
    - [FluentValidation](https://github.com/FluentValidation/FluentValidation)

## Exercise
Make a directory called `chapter7`
```
mkdir chapter7
```
Clone the `solution` branch
```
git clone --branch solution --single-branch https://github.com/HOGENT-Web/csharp-ch-7-exercise-2.git
```
Rename the folder `csharp-ch-7-exercise-2` to `solution`
```
mv csharp-ch-7-exercise-2 solution
```
Run the solution and navigate to the https://localhost:5003/swagger page.
```
cd solution/src/server
dotnet run
```

You will notice that all the endpoints work and can be tested. This should be your final result by **not** looking at the solution's implementation.

Make sure you're in the `chapter7` folder and clone the `main` branch
```
cd ../../..
git clone --branch main --single-branch https://github.com/HOGENT-Web/csharp-ch-7-exercise-2.git
```
Rename the folder `csharp-ch-7-exercise-2` to `exercise`
```
mv csharp-ch-7-exercise-2 exercise
```
Run the exercise and navigate to the https://localhost:5001/swagger page.
```
cd exercise/src/server
dotnet run
```

You will notice that only the `Product` endpoints are implemented. Implement all the other endpoints as you see fit. But make sure the functionality is **exactly** the same as the solution.

## Guidelines
- Use the `ProductController` and `ProductService` as a reference.
- Do not look at the solution's implementation, but try to work it out on your own and the slides.
- Try to use the `Domain` as much as possible.
- Almost all the infrastructure is setup based on the slides.
- Start with the Tags, these are the easiest to implement.
- Do not modify the following projects:
    - Domain
    - Persistence
    - Client (we're going headless anyway)
    - Fakers

## Solution
A possible solution can be found [here](https://github.com/HOGENT-Web/csharp-ch-7-exercise-2/tree/solution#solution).