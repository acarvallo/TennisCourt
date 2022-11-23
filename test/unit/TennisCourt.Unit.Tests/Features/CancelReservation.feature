Feature: CancelReservation

A short summary of the feature

Scenario: CancelReservationWhenValidId
	Given the id of a existing and active reservation 
	When canceling is requested
	Then resevation status should change to <reservationStatus>
    Examples:
	| reservationStatus |
	| CANCELED          |

Scenario: CancelReservationWhenInvalidId
	Given the id of a non existing reservation 
	When canceling is requested
	Then should return essage error <message>
    Examples:
	| message                    |
	| Invalid reservation ID     |