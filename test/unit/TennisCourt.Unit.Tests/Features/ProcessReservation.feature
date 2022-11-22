Feature: ProcessReservation

Make a reservation date of avalaible court

Scenario: ProcessReservationAvailableDate
	Given selected advanced date and amount of 100.00
	When date is available to reservation
	Then process reservation returing a valid GUID reservation id
