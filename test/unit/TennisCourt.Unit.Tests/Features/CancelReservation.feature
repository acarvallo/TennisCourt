Feature: CancelReservation

A short summary of the feature

Scenario: CancelReservationWhenValidId
	Given the id of a existing and active reservation 
	When canceling is requested
	Then resevation status should change to <reservationStatus>
    Examples:
	| reservationStatus |
	| CANCELED          |