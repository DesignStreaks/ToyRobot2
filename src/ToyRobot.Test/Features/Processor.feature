Feature: Processor

@integration
Scenario: Add two numbers
	Given the robot exists
	And I have a table of height 5 and width 5
	And the following commands are executed
		| id  | command_type | x | y | orientation | 
		| 1.1 | Place        | 2 | 2 | North       | 
		| 1.2 | Move         |   |   |             | 
		| 1.3 | Move         |   |   |             | 
		| 1.4 | Left         |   |   |             | 
		| 1.5 | Report       |   |   |             | 
		| 2.1 | Move         |   |   |             | 
		| 2.2 | Report       |   |   |             | 
		| 3.1 | Move         |   |   |             | 
		| 3.2 | Report       |   |   |             | 
		| 4.1 | Move         |   |   |             | 
		| 4.2 | Report       |   |   |             | 
		| 5.1 | Left         |   |   |             | 
		| 5.2 | Report       |   |   |             | 
		| 6.1 | Right        |   |   |             | 
		| 6.2 | Right        |   |   |             | 
		| 6.3 | Move         |   |   |             | 
		| 6.4 | Report       |   |   |             | 
	When all commands are processed
	Then the output contains <report>
		| id | report    |
		| 1  | 2,4,West  |
		| 2  | 1,4,West  |
		| 3  | 0,4,West  |
		| 4  | 0,4,West  |
		| 5  | 0,4,South |
		| 6  | 0,4,North |