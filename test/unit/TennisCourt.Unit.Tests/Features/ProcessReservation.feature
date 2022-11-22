Feature: ProcessReservation

A short summary of the feature

@tag1
Scenario: ProcessReservationAvailableDate
	Given selected date 01/10/2020
	When date is available to reservation
	Then process reservation returing a new reservation id.
