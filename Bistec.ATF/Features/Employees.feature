Feature: Employees

Test CRUD operations for employee api

@employee
Scenario: Get list of Employees
	Given A new admin is created with password pass123
	When Get Employee list api is called
	Then response should have 200 status code
