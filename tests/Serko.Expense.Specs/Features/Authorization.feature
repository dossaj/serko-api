Feature: Authorisation
	In order to avoid unauthorized access
	As a client connecting
	I want to make sure all endpoints are authorised

@Authentication
Scenario: Invalid Access Token For Get
	Given I have an api client for 'https://localhost:5001'
	And I have an invalid token
	When I get the resource at '/api/v1/reservation'
	Then the result status code should be '401'

Scenario: Invalid Access Token For Get By Id
	Given I have an api client for 'https://localhost:5001'
	And I have an invalid token
	When I get the resource at '/api/v1/reservation/1'
	Then the result status code should be '401'

Scenario: Invalid Access Token For Post
	Given I have an api client for 'https://localhost:5001'
	And I have an invalid token
	And I have an the following reservation:
		| key         | value       |
		| vendor      | vendor      |
		| date        | 2019-01-01  |
		| description | description |
	And I have an the following expense:
		| key            | value  |
		| cost_centre    | cost   |
		| payment_method | card   |
		| total          | 100.10 |
	When I post the resource to '/api/v1/reservation'
	Then the result status code should be '401'
