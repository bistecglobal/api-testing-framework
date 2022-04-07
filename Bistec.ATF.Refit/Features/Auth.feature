Feature: Auth

As a user,
I can create an admin
so that I can use the auth token for employee apis

@smoke @auth
Scenario: Create Admin
	Given Admin username is $$randomstring
	And Admin password is pass123
	When Create admin api is called
	Then Status code should be 201
	And response should have a valid access_token
