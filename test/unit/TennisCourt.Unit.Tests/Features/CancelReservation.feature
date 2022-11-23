Feature: CancelReservation

A short summary of the feature

@tag1
Scenario: CancelReservationWhenValidId
	Given the id of a existing and active reservation 
	When canceling is requested
	Then resevation status should change to <reservationStatus>
    Examples:
	| reservationStatus |
	| CANCELED          |