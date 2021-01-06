# FlowerShop project IMD

## Introduction

This is the first version of our flowershop API.

## Usage

First you will have to change the database connection data in appsettings.json to the data of your personal local server environment. Next, run the command `dotnet ef database update` in the your terminal so you can make use of the endpoints of the API.

You can use `dotnet watch run` to run the project; after you get the notification that the application started navigate to <http://localhost:5000/swagger/index.html> or <https://localhost:5001/swagger/index.html>; you will get an overview with all the API methods and a quick method to execute them.

Read through the code; it has been extensively commented.

## Explanation

At the moment we coded basic endpoints such as GET, POST, PUT and DELETE,
in both "stores" and "flowers". We also used mapping, Dto's.

## TODO

- custom endpoints as given in the instructions (overview on stores that have the highest revenue for example).
- Testing part
- Making Async for POST work
