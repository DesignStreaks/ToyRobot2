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

Scenario Outline: Report Robot Position
	Given the robot exists
	And I have a table of height 5 and width 5
	When I place the robot at <x> and <y> facing <orientation>
	And I Report the Robot Position
	Then the report returns <report>

	Examples:
		| id | x   | y   | orientation | status | message | report      |
		| 1  | 0   | 0   | "North"     | "Ok"   | "Ok"    | "0,0,North" |
		| 2  | 0   | 4   | "North"     | "Ok"   | "Ok"    | "0,4,North" |
		| 3  | 4   | 0   | "North"     | "Ok"   | "Ok"    | "4,0,North" |
		| 4  | 2   | 2   | "North"     | "Ok"   | "Ok"    | "2,2,North" |
		| 5  | 4   | 4   | "North"     | "Ok"   | "Ok"    | "4,4,North" |
		| 6  | 2   | 2   | "South"     | "Ok"   | "Ok"    | "2,2,South" |
		| 7  | 2   | 2   | "East"      | "Ok"   | "Ok"    | "2,2,East"  |
		| 8  | 2   | 2   | "West"      | "Ok"   | "Ok"    | "2,2,West"  |
		| 9  | 0   | 5   | "North"     | "Ok"   | "Ok"    | ""          |
		| 10 | 5   | 0   | "North"     | "Ok"   | "Ok"    | ""          |
		| 11 | -1  | 2   | "North"     | "Ok"   | "Ok"    | ""          |
		| 12 | 2   | -1  | "North"     | "Ok"   | "Ok"    | ""          |
		| 13 | -10 | -10 | "North"     | "Ok"   | "Ok"    | ""          |

Scenario Outline: Move Robot around Table
	Given the robot exists
	And I have a table of height 5 and width 5
	When I place the robot at <x> and <y> facing <orientation>
	And I move the robot forward
	Then the value of the status will be <status>
	And the status will contain the message <message>
	And the robot <has_moved> on the table to <result_x> and <result_y>

	Examples:
		| id  | x | y | orientation | status | message | has_moved | result_x | result_y |
		| 1.1 | 2 | 0 | "North"     | "Ok"   | "Ok"    | "true"    | 2        | 1        |
		| 1.2 | 2 | 4 | "North"     | "Ok"   | "Ok"    | "false"   | 2        | 4        |
		| 1.3 | 4 | 2 | "North"     | "Ok"   | "Ok"    | "true"    | 4        | 3        |
		| 1.4 | 0 | 2 | "North"     | "Ok"   | "Ok"    | "true"    | 0        | 3        |
		| 1.5 | 2 | 2 | "North"     | "Ok"   | "Ok"    | "true"    | 2        | 3        |
		| 2.1 | 2 | 0 | "South"     | "Ok"   | "Ok"    | "false"   | 2        | 0        |
		| 2.2 | 2 | 4 | "South"     | "Ok"   | "Ok"    | "true"    | 2        | 3        |
		| 2.3 | 4 | 2 | "South"     | "Ok"   | "Ok"    | "true"    | 4        | 1        |
		| 2.4 | 0 | 2 | "South"     | "Ok"   | "Ok"    | "true"    | 0        | 1        |
		| 2.5 | 2 | 2 | "South"     | "Ok"   | "Ok"    | "true"    | 2        | 1        |
		| 3.1 | 2 | 0 | "East"      | "Ok"   | "Ok"    | "true"    | 3        | 0        |
		| 3.2 | 2 | 4 | "East"      | "Ok"   | "Ok"    | "true"    | 3        | 4        |
		| 3.3 | 4 | 2 | "East"      | "Ok"   | "Ok"    | "false"   | 4        | 2        |
		| 3.4 | 0 | 2 | "East"      | "Ok"   | "Ok"    | "true"    | 1        | 2        |
		| 3.5 | 2 | 2 | "East"      | "Ok"   | "Ok"    | "true"    | 3        | 2        |
		| 4.1 | 2 | 0 | "West"      | "Ok"   | "Ok"    | "true"    | 1        | 0        |
		| 4.2 | 2 | 4 | "West"      | "Ok"   | "Ok"    | "true"    | 1        | 4        |
		| 4.3 | 4 | 2 | "West"      | "Ok"   | "Ok"    | "true"    | 3        | 2        |
		| 4.4 | 0 | 2 | "West"      | "Ok"   | "Ok"    | "false"   | 0        | 2        |
		| 4.5 | 2 | 2 | "West"      | "Ok"   | "Ok"    | "true"    | 1        | 2        |

Scenario Outline: Turn Robot around Table
	Given the robot exists
	And I have a table of height 5 and width 5
	When I place the robot at <x> and <y> facing <orientation>
	And I turn the robot <direction>
	Then the value of the status will be <status>
	And the status will contain the message <message>
	And the robot is on the table at <result_x> and <result_y> facing <result_orientation>

	Examples:
		| id | x | y | orientation | direction | status | message | result_x | result_y | result_orientation |
		| 1  | 3 | 3 | "North"     | "Left"    | "Ok"   | "Ok"    | 3        | 3        | "West"             |
		| 2  | 3 | 3 | "North"     | "Right"   | "Ok"   | "Ok"    | 3        | 3        | "East"             |
		| 3  | 3 | 3 | "South"     | "Left"    | "Ok"   | "Ok"    | 3        | 3        | "East"             |
		| 4  | 3 | 3 | "South"     | "Right"   | "Ok"   | "Ok"    | 3        | 3        | "West"             |
		| 5  | 3 | 3 | "East"      | "Left"    | "Ok"   | "Ok"    | 3        | 3        | "North"            |
		| 6  | 3 | 3 | "East"      | "Right"   | "Ok"   | "Ok"    | 3        | 3        | "South"            |
		| 7  | 3 | 3 | "West"      | "Left"    | "Ok"   | "Ok"    | 3        | 3        | "South"            |
		| 8  | 3 | 3 | "West"      | "Right"   | "Ok"   | "Ok"    | 3        | 3        | "North"            |