Feature: RescheduleReservation

A short summary of the feature

@tag1
Scenario: RescheduleReservationNewDate
	Given the id of existing and active reservation
	When reschedule reservation is requested
	Then same reservation id with status <statusReservation>
	Examples: 
	| statusReservation |
	| RESCHEDULED       |
