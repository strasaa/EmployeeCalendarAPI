# Employee Calendar
Small demo project of employee schedule tracker

[Backend part](https://github.com/strasaa/EmployeeCalendarAPI) (.NET, C#) 

[Frontend part](https://github.com/strasaa/events-app) (Angular, TypeScript)

## About
This project is small application for managing and tracking schedule of the employees. So far it is in the end of the first iteration - version 1, for demo, 
presentation purposes only. 
Goal of the final product is to offer functionality to manage employees and their scheduled events.

## Design
Three tier architecture is used and split into frontend (Angular, Typescript) and backend (.NET, C#). Used database is the Microsoft SQL Server.

### Database
There are three entities: `Employee`, `CalendarEvent` and `CalendarEventType`.
`Employee` represents person with first name, surname and title properties. `CalendarEventType` is list of the `CalendarEvents`, 
such as work, business trip, vacation, doctor's appointment etc. `CalendarEvent` itself is representation of time slot of one employee, 
e.g. two employees on same meeting in the same time have two different `CalendarEvents`. Text note could be added to `CalendarEvent` and it is one type, 
therefore relationships are one to many between `Employee` and `CalendarEvent`; one to many between `CalendarEventType` and `CalendarEvent`.

![Database](https://github.com/strasaa/EmployeeCalendarAPI/blob/master/database.png?raw=true)

Database was generated with Entity Framework **Code-First** approach.

### Backend 
Backend is standard Web Api application with Entity Framework and LINQ queries. 
Additionally, there is the Repository pattern, implemented itself with interfaces usage. 
Ideally it would be combine with Unit of Work pattern, howewer I consider UoF unnecesary due to the Entity Framework. 
Repositories in this solution are functional like a services and they encapsulate `application context` in controllers.

All the base functionality is implemented. The future sprint should cover security issues. JSON Web Token and user management should be added. 
REST API is well documented with **Swagger**. It consists of three controllers, each corresponding with one entity. 
All base requests are implemented (GET, POST, PUT, DELETE). `CalendarEventController` is  extended with several GET requests specific for employee and time interval 
(employee Id and dates in ISO format are required).

![API](https://github.com/strasaa/EmployeeCalendarAPI/blob/master/api.png?raw=true)

### Frontend
Frontend is Angular single page application written in TypeScript and it uses Material Design components. 
There is `main` landing component and `toolbar` component for navigation. Other three components are part of the `employee` module.
  
`employee-list` component provides list of all employees in system. By clicking on record, user is redirected to the `employee-detail`. 
Possible future improvement could be filtering by name functionality.

![Employee list](https://github.com/strasaa/events-app/blob/master/employee-list.png?raw=true)

The page Employees card is composed from two components `employee-detail` and `employee-events`. 
It is used for showing employee detail and possible update. 
Employee could be deleted by *Delete* button and same page is used with blank form if new employee is created. 
This functionality is accessible with toolbar shortcut. 

![Employee detail](https://github.com/strasaa/events-app/blob/master/employee-detail.png?raw=true)

Part of the Employees card is the events overview. 
It shows all events of the employee in the interval. 
Default dates are set to one month in past and one month in the future, creating two months long interval. 
User can adjust dates by date pickers and confirming with button *Filter*. 

Next sprint should focus on the creating custom calendar component for events management in same nature as users are used to from other calendar products. 
Additionally, login and security should be implementent whent it is supported by backend.
