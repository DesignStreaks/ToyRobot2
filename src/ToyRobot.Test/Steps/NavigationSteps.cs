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
            scene.Table = new Model.Table { Height = height, Width = width };

            this.scenarioContext.Set(scene, "scene");
        }

        [When(@"I place the robot at (.*) and (.*) facing ""(.*)""")]
        public void WhenIPlaceTheRobotAtAndFacing(int x, int y, string orientation)
        {
            var scene = scenarioContext.Get<Scene>("scene");
            var bearing = new Bearing
            {
                Orientation = orientation.ToEnum<Orientation>(),
                Position = new Position
                {
                    X = x,
                    Y = y
                }
            };
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
    }
}
