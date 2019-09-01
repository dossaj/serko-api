Feature: Reservation
	In order to make reservations
	As a connecting client
	I want to be able to manage reservations

Background: 
	Given I have an api client for 'https://localhost:5001'
	And I have an valid token
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
	And I post the resource at '/api/v1/reservation'
	
@Reservation
Scenario: Get all reserations
	Given I have an api client for 'https://localhost:5001'
	And I have an valid token
	When I get the resource at '/api/v1/reservation'
	Then the result status code should be '200'
	And the number of results should not be '0'

Scenario: Get single reseration
	Given I have an api client for 'https://localhost:5001'
	And I have an valid token
	When I get the resource at '/api/v1/reservation/1'
	Then the result status code should be '200'
	And the result should have the id '1'

Scenario: Post a reservation with an empty cost centre
	Given I have an api client for 'https://localhost:5001'
	And I have an valid token
	And I have an the following reservation:
		| key         | value       |
		| vendor      | vendor      |
		| date        | 2019-01-01  |
		| description | description |
	And I have an the following expense:
		| key            | value  |
		| payment_method | card   |
		| total          | 100.10 |
	And I post the resource at '/api/v1/reservation'
	And I get the resource from the post at '/api/v1/reservation'
	Then the result status code should be '200'
	And The result cost centre should be unknown

Scenario: Post a reservation with a zero total
	Given I have an api client for 'https://localhost:5001'
	And I have an valid token
	And I have an the following reservation:
		| key         | value       |
		| vendor      | vendor      |
		| date        | 2019-01-01  |
		| description | description |
	And I have an the following expense:
		| key   | value |
		| total | 0     |
	And I post the resource at '/api/v1/reservation'
	Then the result status code should be '400'

Scenario: Post a reservation by email with valid format
	Given I have an api client for 'https://localhost:5001'
	And I have an valid token
	When I post the email 'EmailValid' to '/api/v1/reservation'
	Then the result status code should be '200'
	
Scenario: Post a reservation by email with missing closing tag
	Given I have an api client for 'https://localhost:5001'
	And I have an valid token
	When I post the email 'EmailInvalid' to '/api/v1/reservation'
	Then the result status code should be '400'
	
