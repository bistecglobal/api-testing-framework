Feature: Auth

Call authentication related apis

@smoke @auth
Scenario: Create Admin
	Given Admin username is chandima
	And Admin password is pass123
	When Create admin api is called
	Then response should have 201 status code
	And response should have a valid access_token
