Feature: Navigation

@integration
Scenario Outline: Add Robot to Table
	Given the robot exists
	And I have a table of height 5 and width 5
	When I place the robot at <x> and <y> facing <orientation>
	Then the value of the status will be <status>
	And the status will contain the message <message>
	And the robot <is_added> to the table at <x> and <y>

	Examples:
		| id | x   | y   | orientation | status | message | is_added |
		| 1  | 0   | 0   | "North"     | "Ok"   | "Ok"    | "true"   |
		| 2  | 0   | 4   | "North"     | "Ok"   | "Ok"    | "true"   |
		| 3  | 4   | 0   | "North"     | "Ok"   | "Ok"    | "true"   |
		| 4  | 2   | 2   | "North"     | "Ok"   | "Ok"    | "true"   |
		| 5  | 4   | 4   | "North"     | "Ok"   | "Ok"    | "true"   |
		| 6  | 0   | 5   | "North"     | "Ok"   | "Ok"    | "false"  |
		| 7  | 5   | 0   | "North"     | "Ok"   | "Ok"    | "false"  |
		| 8  | -1  | 2   | "North"     | "Ok"   | "Ok"    | "false"  |
		| 9  | 2   | -1  | "North"     | "Ok"   | "Ok"    | "false"  |
		| 10 | -10 | -10 | "North"     | "Ok"   | "Ok"    | "false"  |