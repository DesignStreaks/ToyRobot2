using System;
using TechTalk.SpecFlow;
using ToyRobot.Model;
using ToyRobot.Service.Commands;
using Xunit;
using Table = ToyRobot.Model.Table;

namespace ToyRobot.Test.Steps
{
    [Binding, Scope(Feature = "Navigation")]
    public class NavigationSteps
    {
        private readonly ScenarioContext scenarioContext;

        public NavigationSteps(ScenarioContext scenarioContext)
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

        [When(@"I place the robot at (.*) and (.*) facing ""(.*)""")]
        public void WhenIPlaceTheRobotAtAndFacing(int x, int y, string orientation)
        {
            var robot = scenarioContext.Get<Robot>("robot");
            var table = scenarioContext.Get<Table>("table");
            var bearing = new Bearing
            {
                Orientation = orientation.ToEnum<Orientation>(),
                Position = new Position(x, y)
            };
            this.scenarioContext.Set(bearing, "bearing");

            var status = new PlaceCommand(robot, bearing).Execute(table);

            this.scenarioContext.Set(status, "status");
            this.scenarioContext.Set(status.Data, "table");
        }

        [Then(@"the value of the status will be ""(.*)""")]
        public void ThenTheValueOfTheStatusWillBe(string statusValue)
        {
            var status = this.scenarioContext.Get<Status<Table>>("status");
            Assert.Equal(statusValue.ToEnum<Status.States>(), status.State);
        }

        [Then(@"the status will contain the message ""(.*)""")]
        public void ThenTheStatusWillContainTheMessage(string message)
        {
            var status = this.scenarioContext.Get<Status<Table>>("status");
            Assert.Equal(message, status.Message);
        }

        [Then(@"the rrobot will be at (.*) and (.*) facing ""(.*)""")]
        public void ThenTheRrobotWillBeAtAndFacing(int x, int y, string orientation)
        {
            var robot = scenarioContext.Get<Robot>("robot");
            var table = scenarioContext.Get<Table>("table");

            var data = table[robot.Id];

            Assert.NotNull(data.Robot);
            Assert.Equal(data.Robot.Id, robot.Id);
            Assert.Equal(data.Bearing.Orientation, orientation.ToEnum<Orientation>());
        }

        [Then(@"the robot ""(.*)"" to the table at (.*) and (.*)")]
        public void ThenTheRobotToTheTableAtAnd(string is_added, int result_x, int result_y)
        {
            var status = scenarioContext.Get<Status<Table>>("status");
            var robot = scenarioContext.Get<Robot>("robot");
            var table = status.Data;
            var data = table[robot.Id];

            _ = bool.TryParse(is_added, out var result);
            if (result)
            {
                Assert.NotNull(data.Robot);
                Assert.Equal(data.Robot.Id, robot.Id);
            }
            else
            {
                Assert.Null(data);
            }
        }

        [When(@"I move the robot forward")]
        public void WhenIMoveTheRobotForward()
        {
            var robot = scenarioContext.Get<Robot>("robot");
            var table = scenarioContext.Get<Table>("table");

            var status = new MoveCommand(robot).Execute(table);

            this.scenarioContext.Set(status, "status");
            this.scenarioContext.Set(status.Data, "table");
        }

        [Then(@"the robot ""(.*)"" on the table to (.*) and (.*)")]
        public void ThenTheRobotOnTheTableToAnd(string has_moved, int result_x, int result_y)
        {
            var status = scenarioContext.Get<Status<Table>>("status");
            var robot = scenarioContext.Get<Robot>("robot");
            var table = status.Data;
            var data = table[robot.Id];

            _ = bool.TryParse(has_moved, out var result);
            if (result)
            {
                Assert.NotNull(data.Robot);
                Assert.Equal(data.Robot.Id, robot.Id);
            }
            else
            {
                var originalBearing = this.scenarioContext.Get<Bearing>("bearing");

                Assert.Equal(originalBearing.Position.X, result_x);
                Assert.Equal(originalBearing.Position.Y, result_y);
                Assert.Equal(data.Robot.Id, robot.Id);
            }
        }

        [When(@"I Report the Robot Position")]
        public void WhenIReportTheRobotPosition()
        {
            var robot = scenarioContext.Get<Robot>("robot");
            var table = scenarioContext.Get<Table>("table");
            var status = new ReportCommand(robot).Execute(table);

            this.scenarioContext.Set(status, "status");
            this.scenarioContext.Set(status.Data, "report");
        }

        [Then(@"the report returns ""(.*)""")]
        public void ThenTheReportReturns(string report)
        {
            var data = this.scenarioContext.Get<string>("report");
            Assert.Equal(report, data);
        }





    }
}
