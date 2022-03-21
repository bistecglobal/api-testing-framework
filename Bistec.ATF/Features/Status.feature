Feature: Status

Liveness Check of the server

@smoke
Scenario: Server is running
	Given status api is called
	When status is checked
	Then status should be ok
