﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.4.0.0
//      SpecFlow Generator Version:3.4.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace ToyRobot.Test.Features
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.4.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class NavigationFeature : object, Xunit.IClassFixture<NavigationFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "Navigation.feature"
#line hidden
        
        public NavigationFeature(NavigationFeature.FixtureData fixtureData, ToyRobot_Test_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features", "Navigation", null, ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableTheoryAttribute(DisplayName="Add Robot to Table")]
        [Xunit.TraitAttribute("FeatureTitle", "Navigation")]
        [Xunit.TraitAttribute("Description", "Add Robot to Table")]
        [Xunit.TraitAttribute("Category", "integration")]
        [Xunit.InlineDataAttribute("1", "0", "0", "\"North\"", "\"Ok\"", "\"Ok\"", "\"true\"", new string[0])]
        [Xunit.InlineDataAttribute("2", "0", "4", "\"North\"", "\"Ok\"", "\"Ok\"", "\"true\"", new string[0])]
        [Xunit.InlineDataAttribute("3", "4", "0", "\"North\"", "\"Ok\"", "\"Ok\"", "\"true\"", new string[0])]
        [Xunit.InlineDataAttribute("4", "2", "2", "\"North\"", "\"Ok\"", "\"Ok\"", "\"true\"", new string[0])]
        [Xunit.InlineDataAttribute("5", "4", "4", "\"North\"", "\"Ok\"", "\"Ok\"", "\"true\"", new string[0])]
        [Xunit.InlineDataAttribute("6", "0", "5", "\"North\"", "\"Ok\"", "\"Ok\"", "\"false\"", new string[0])]
        [Xunit.InlineDataAttribute("7", "5", "0", "\"North\"", "\"Ok\"", "\"Ok\"", "\"false\"", new string[0])]
        [Xunit.InlineDataAttribute("8", "-1", "2", "\"North\"", "\"Ok\"", "\"Ok\"", "\"false\"", new string[0])]
        [Xunit.InlineDataAttribute("9", "2", "-1", "\"North\"", "\"Ok\"", "\"Ok\"", "\"false\"", new string[0])]
        [Xunit.InlineDataAttribute("10", "-10", "-10", "\"North\"", "\"Ok\"", "\"Ok\"", "\"false\"", new string[0])]
        public virtual void AddRobotToTable(string id, string x, string y, string orientation, string status, string message, string is_Added, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "integration"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            string[] tagsOfScenario = @__tags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("id", id);
            argumentsOfScenario.Add("x", x);
            argumentsOfScenario.Add("y", y);
            argumentsOfScenario.Add("orientation", orientation);
            argumentsOfScenario.Add("status", status);
            argumentsOfScenario.Add("message", message);
            argumentsOfScenario.Add("is_added", is_Added);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Add Robot to Table", null, tagsOfScenario, argumentsOfScenario);
#line 4
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 5
 testRunner.Given("the robot exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 6
 testRunner.And("I have a table of height 5 and width 5", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 7
 testRunner.When(string.Format("I place the robot at {0} and {1} facing {2}", x, y, orientation), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 8
 testRunner.Then(string.Format("the value of the status will be {0}", status), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 9
 testRunner.And(string.Format("the status will contain the message {0}", message), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 10
 testRunner.And(string.Format("the robot {0} to the table at {1} and {2}", is_Added, x, y), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.4.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                NavigationFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                NavigationFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
