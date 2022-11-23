Feature: CancelReservation

A short summary of the feature

Scenario: CancelReservationWhenValidId
	Given the id of a existing and active reservation of <amount>
	When canceling is requested
	Then resevation status should change to <reservationStatus> and Refund same as <amount>
    Examples:
	| reservationStatus | amount |
	| CANCELED          | 100.00 |
 
Scenario: CancelReservationWhenInvalidId
	Given the id of a non existing reservation 
	When canceling is requested
	Then should return essage error <message>
    Examples:
	| message                    |
	| Invalid reservation ID     |