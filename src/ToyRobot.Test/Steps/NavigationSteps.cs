using System;
using TechTalk.SpecFlow;
using ToyRobot.Model;
using ToyRobot.Service.Commands;
using Xunit;

namespace ToyRobot.Test.Steps
{
    [Binding, Scope(Feature = "Navigation")]
    public class NavigationSteps
    {
        private readonly ScenarioContext scenarioContext;

        public NavigationSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext ?? throw new ArgumentNullException(nameof(scenarioContext));
            this.scenarioContext.Set<Scene>(new Scene(), "scene");
        }

        [Given(@"the robot exists")]
        public void GivenTheRobotExists()
        {
            var scene = scenarioContext.Get<Scene>("scene");
            scene.Robot = new Model.Robot();
            this.scenarioContext.Set(scene, "scene");
        }

        [Given(@"I have a table of height (.*) and width (.*)")]
        public void GivenIHaveATableOfHeightAndWidth(int height, int width)
        {
            var scene = scenarioContext.Get<Scene>("scene");
            scene.Table = new Model.Table(height, width);

            this.scenarioContext.Set(scene, "scene");
        }

        [When(@"I place the robot at (.*) and (.*) facing ""(.*)""")]
        public void WhenIPlaceTheRobotAtAndFacing(int x, int y, string orientation)
        {
            var scene = scenarioContext.Get<Scene>("scene");
            var bearing = new Bearing
            {
                Orientation = orientation.ToEnum<Orientation>(),
                Position = new Position(x, y)
            };
            this.scenarioContext.Set(bearing, "bearing");
            var status = new PlaceCommand(bearing).Execute(scene);
            this.scenarioContext.Set(status, "status");
            this.scenarioContext.Set(status.Data, "scene");
        }

        [Then(@"the value of the status will be ""(.*)""")]
        public void ThenTheValueOfTheStatusWillBe(string statusValue)
        {
            var status = this.scenarioContext.Get<Status<Scene>>("status");
            Assert.Equal(statusValue.ToEnum<Status.States>(), status.State);
        }

        [Then(@"the status will contain the message ""(.*)""")]
        public void ThenTheStatusWillContainTheMessage(string message)
        {
            var status = this.scenarioContext.Get<Status<Scene>>("status");
            Assert.Equal(message, status.Message);
        }

        [Then(@"the rrobot will be at (.*) and (.*) facing ""(.*)""")]
        public void ThenTheRrobotWillBeAtAndFacing(int x, int y, string orientation)
        {
            var scene = this.scenarioContext.Get<Scene>("scene");
            var table = scene.Table;

            var data = table[x, y];

            Assert.NotNull(data.Robot);
            Assert.Equal(data.Robot.Id, scene.Robot.Id);
            Assert.Equal(data.Orientation, orientation.ToEnum<Orientation>());
        }

        [Then(@"the robot ""(.*)"" to the table at (.*) and (.*)")]
        public void ThenTheRobotToTheTableAtAnd(string is_added, int result_x, int result_y)
        {
            var scene = this.scenarioContext.Get<Scene>("scene");
            var table = scene.Table;
            var data = table[result_x, result_y];

            _ = bool.TryParse(is_added, out var result);
            if (result)
            {
                Assert.NotNull(data.Robot);
                Assert.Equal(data.Robot.Id, scene.Robot.Id);
            }
            else
            {
                Assert.Null(data);
            }
        }

        [When(@"I move the robot forward")]
        public void WhenIMoveTheRobotForward()
        {
            var scene = scenarioContext.Get<Scene>("scene");
            var bearing = scenarioContext.Get<Bearing>("bearing");

            var status = new MoveCommand(bearing).Execute(scene);

            this.scenarioContext.Set(status, "status");
            this.scenarioContext.Set(status.Data, "scene");
        }

        [Then(@"the robot ""(.*)"" on the table to (.*) and (.*)")]
        public void ThenTheRobotOnTheTableToAnd(string has_moved, int result_x, int result_y)
        {
            var scene = this.scenarioContext.Get<Scene>("scene");
            var table = scene.Table;
            var data = table[result_x, result_y];

            _ = bool.TryParse(has_moved, out var result);
            if (result)
            {
                Assert.NotNull(data.Robot);
                Assert.Equal(data.Robot.Id, scene.Robot.Id);
            }
            else
            {
                var originalBearing = this.scenarioContext.Get<Bearing>("bearing");

                Assert.Equal(originalBearing.Position.X, result_x);
                Assert.Equal(originalBearing.Position.Y, result_y);
                Assert.Equal(data.Robot.Id, scene.Robot.Id);
            }
        }

        [Then(@"the robot ""(.*)"" and the original coordinate (.*) and (.*) are null")]
        public void ThenTheRobotAndTheOriginalCoordinateAndAreNull(string has_moved, int x, int y)
        {
            var scene = this.scenarioContext.Get<Scene>("scene");
            var table = scene.Table;
            var data = table[x, y];

            _ = bool.TryParse(has_moved, out var result);
            if (result)
            {
                Assert.Null(data);
            }
            else
            {
                Assert.NotNull(data.Robot);
                Assert.Equal(data.Robot.Id, scene.Robot.Id);
            }
        }

        //[Then(@"the original coordinate (.*) and (.*) are null")]
        //public void ThenTheOriginalCoordinateAndAreNull(int x, int y)
        //{
        //    ScenarioContext.Current.Pending();
        //}


    }
}
