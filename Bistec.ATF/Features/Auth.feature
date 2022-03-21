Feature: Auth

Call authentication related apis

@smoke @auth
Scenario: Create Admin
	Given Admin username is $$randomstring
	And Admin password is pass123
	When Create admin api is called
	Then response should have 201 status code
	And response should have a valid access_token

@smoke @auth
Scenario: Create Admin without password
	Given Admin username is $$randomstring
	And Admin password is <null>
	When Create admin api is called
	Then response should have 400 status code

@smoke @auth
Scenario: Login as Admin
	Given A new admin is created with password pass123
	And Admin password is pass123
	When Login admin api is called
	Then response should have 201 status code
	And response should have a valid access_token

@smoke @auth
Scenario: Login as Admin with invalid password
	Given A new admin is created with password pass123
	And Admin password is 1234
	When Login admin api is called
	Then response should have 401 status code
