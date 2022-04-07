Feature: Employees

As an admin
I can add or remove employees
So that I can manage company

@employee @messured
Scenario: Get list of Employees
	Given A new admin is created with username $$randomstring and password pass123
	When Get Employee list api is called
	Then Status code should be 200
