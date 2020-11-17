/*
 * DESIGNSTREAKS CONFIDENTIAL
 * __________________
 *
 *  Copyright © DesignStreaks - 2010 - 2020
 *  All Rights Reserved.
 *
 * NOTICE:  All information contained herein is, and remains
 * the property of DesignStreaks and its suppliers, if any.
 * The intellectual and technical concepts contained
 * herein are proprietary to DesignStreaks and its suppliers and may
 * be covered by Australian, U.S. and Foreign Patents,
 * patents in process, and are protected by trade secret or copyright law.
 * Dissemination of this information or reproduction of this material
 * is strictly forbidden unless prior written permission is obtained
 * from DesignStreaks.
 */

namespace ToyRobot.Test.Steps
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using TechTalk.SpecFlow;
    using TechTalk.SpecFlow.Assist;
    using ToyRobot.Model;
    using ToyRobot.Service.Commands;
    using ToyRobot.Service.Processors;
    using Xunit;
    using Table = Model.Table;

    [Binding, Scope(Feature = "Processor")]
    public class ProcessorSteps
    {
        private readonly ScenarioContext scenarioContext;

        public ProcessorSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext ?? throw new ArgumentNullException(nameof(scenarioContext));
        }


        [Given(@"the robot exists")]
        public void GivenTheRobotExists()
        {
            var robot = new Model.Robot();
            this.scenarioContext.Set(robot, "robot");
        }

        [Given(@"I have a table of height (.*) and width (.*)")]
        public void GivenIHaveATableOfHeightAndWidth(int height, int width)
        {
            var table = new Model.Table(height, width);

            this.scenarioContext.Set(table, "table");
        }

        [Given(@"the following commands are executed")]
        public void GivenTheFollowingCommandsAreExecuted(TechTalk.SpecFlow.Table table)
        {
            var robot = this.scenarioContext.Get<Robot>("robot");
            var commands = new List<object>();

            IEnumerable<CommandDetails> commandDetails = table.CreateSet<CommandDetails>();
            foreach (var commandDetail in commandDetails)
            {
#pragma warning disable CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).
                object x = commandDetail.commandType switch
                {
                    CommandTypes.Place => new PlaceCommand(robot, new Bearing
                    {
                        Position = new Position(commandDetail.x, commandDetail.y),
                        Orientation = commandDetail.orientation.ToEnum<Orientation>()
                    }
                    ),
                    CommandTypes.Move => new MoveCommand(robot),
                    CommandTypes.Report => new ReportCommand(robot),
                    CommandTypes.Left => new TurnCommand(robot, Direction.Left),
                    CommandTypes.Right => new TurnCommand(robot, Direction.Right)
                };
#pragma warning restore CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).

                commands.Add(x);
            }

            this.scenarioContext.Set(commands, "commands");
        }

        [When(@"all commands are processed")]
        public void WhenAllCommandsAreProcessed()
        {
            var table = this.scenarioContext.Get<Table>("table");
            var commands = this.scenarioContext.Get<List<object>>("commands");

            var output = new StringWriter();
            var processor = new Processor(output);

            var status = processor.Process(table, commands);

            this.scenarioContext.Set(output.ToString().Split(Environment.NewLine), "reports");
        }

        [Then(@"the output contains (.*)")]
        public void ThenTheOutputContains(string p0, TechTalk.SpecFlow.Table table)
        {
            IEnumerable<ReportDetails> reports = table.CreateSet<ReportDetails>();
            var output = this.scenarioContext.Get<string[]>("reports");

            foreach(var report in reports)
            {
                Assert.Equal(report.report, output[report.id-1]);
            }
        }


        private record CommandDetails(int id, int step, CommandTypes commandType, int x, int y, string orientation);

        private record ReportDetails(int id, string report);
    }
}
