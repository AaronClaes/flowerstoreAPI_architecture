# FlowerShop project IMD

## Introduction

This is the final version of our flowershop API.

## Usage

First you will have to change the database connection data in appsettings.json to the data of your personal local server environment. Next, run the command `dotnet ef database update` in the your terminal so you can make use of the endpoints of the API.

You can use `dotnet watch run` to run the project; after you get the notification that the application started navigate to <http://localhost:5000/swagger/index.html> or <https://localhost:5001/swagger/index.html>; you will get an overview with all the API methods and a quick method to execute them.

Read through the code; it has been extensively commented.

## Explanation

In this version you have the basic endpoints (GET, PUT, POST, DELETE) of the Flowers, the Stores and the Sales. 
You have the option to register a sale that contains a Flower and a Store. 

This project is not entirely working. You can run it but it does not fully cooperate. 


## MISSING/NOT WORKING

- Connection with the BasicadressAPI.Vlaanderen => our .NET Framework is too outdated to implement the tools needed for implementation of an API.
- MongoDB connection does not fully function.
- Testing (unit and integration) does not fully function.