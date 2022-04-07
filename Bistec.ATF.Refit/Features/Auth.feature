Feature: Auth

Call authentication related apis

@smoke @auth
Scenario: Create Admin
	Given Admin username is $$randomstring
	And Admin password is pass123
	When Create admin api is called
	Then Status code should be 201
	And response should have a valid access_token
