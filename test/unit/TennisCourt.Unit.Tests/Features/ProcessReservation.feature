Feature: ProcessReservation

Make a reservation date of avalaible court

Scenario: ProcessReservationAvailableDate
	Given selected date D plus <daysToAdd> and amount of <amount>
	When reservation is requested
	Then process reservation returing a valid GUID reservation id
	Examples: 
	| daysToAdd | amount |
	| 5         | 100    |
	| 6         | 10     |
	
Scenario: ProcessReservationUnavailabeDate
	Given selected date D plus <daysToAdd> and amount of <amount>
	When date selected is already reserved
	Then process reservation should return not available error message
	Examples: 
	| daysToAdd | amount |
	| 1         | 100    |
	| 2         | 10     |

Scenario: ProcessReservationretroactiveDate
	Given selected date D plus <daysToAdd> and amount of <amount>
	When reservation is requested
	Then process reservation should return invalid date error message
	Examples: 
	| daysToAdd | amount |
	| -1         | 100    |
	| -2         | 10     |


Scenario: ProcessReservationAmountInvalid
	Given selected date D plus <daysToAdd> and amount of <amount>
	When reservation is requested
	Then process reservation should return invalid amount error message
	Examples: 
	| daysToAdd | amount |
	| 7         | 0      |
	| 8         | -10    |