Feature: RescheduleReservation

A short summary of the feature

@tag1
Scenario: RescheduleReservationNewDate
	Given the id of existing and active reservation and with date <addDays> ahead and new date available to reschedule
	When reschedule reservation is requested
	Then same reservation id with status <statusReservation> and date changed
	Examples: 
	| statusReservation | addDays |
	| RESCHEDULED       | 2       |

Scenario: RescheduleReservationNewDateUnavailabe
	Given the id of existing and active reservation and with date <addDays> ahead and new date unavailable to reschedule
	When reschedule reservation is requested
	Then should return error message <message>
	Examples: 
	| message           | addDays |
	| Date not availabe | 2       |