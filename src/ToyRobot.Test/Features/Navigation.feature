Feature: Navigation

@integration
Scenario Outline: Add Robot to Table
	Given the robot exists
	And I have a table of height 5 and width 5
	When I place the robot at <x> and <y> facing <orientation>
	Then the value of the status will be <status>
	And the status will contain the message <message>

	Examples:
		| id | x   | y   | orientation | status | message |
		| 1  | 0   | 0   | "North"     | "Ok"   | "Ok"    |
		| 2  | 0   | 4   | "North"     | "Ok"   | "Ok"    |
		| 3  | 4   | 0   | "North"     | "Ok"   | "Ok"    |
		| 4  | 2   | 2   | "North"     | "Ok"   | "Ok"    |
		| 5  | 4   | 4   | "North"     | "Ok"   | "Ok"    |
		| 6  | 0   | 5   | "North"     | "Ok"   | "Ok"    |
		| 7  | 5   | 0   | "North"     | "Ok"   | "Ok"    |
		| 8  | -1  | 2   | "North"     | "Ok"   | "Ok"    |
		| 9  | 2   | -1  | "North"     | "Ok"   | "Ok"    |
		| 10 | -10 | -10 | "North"     | "Ok"   | "Ok"    |