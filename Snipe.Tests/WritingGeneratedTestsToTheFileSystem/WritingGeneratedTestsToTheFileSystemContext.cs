using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Snipe;
using SnipeLib;

namespace WritingGeneratedTestsToTheFilesystem
{
	public class WritingGeneratedTestsToTheFilesystemContext
	{
	    protected Mock<IApplicationHost> TheApplicationHost = new Mock<IApplicationHost>();

        private readonly Dictionary<string, Context> _contexts = new Dictionary<string, Context>();
	    private readonly SpecFile _specFile = new SpecFile();

		protected void Given_a_specfile()
		{
		    _specFile.Contexts = _contexts;
		}
		
		protected void Given_the_specfile_defines_one_context()
		{
            _specFile.Contexts.Add("thefirstContext", new Context(new[] {"Context:", "the", "first", "context"}));
		    TheApplicationHost.Setup(host => host.CreateDirectory("TheFirstContext"));
            TheApplicationHost.Setup(host => host.WriteFile(@"TheFirstContext\TheFirstContextContext.cs", It.IsAny<string>()));


		}
		
		protected void Given_the_specfile_defines_one_scenario()
		{
            _contexts.First().Value.Scenarios.Add(new Scenario(new[] {"Scenario:", "the", "first", "scenario" }));
            TheApplicationHost.Setup(host => host.WriteFile(@"TheFirstContext\TheFirstScenario.cs", It.IsAny<string>()));

		}
		
		protected void Given_the_specfile_defines_multiple_scenarios()
        {
            _contexts.First().Value.Scenarios.Add(new Scenario(new[] { "Scenario:", "the", "first", "scenario" }));
            _contexts.First().Value.Scenarios.Add(new Scenario(new[] { "Scenario:", "the", "second", "scenario" }));

            TheApplicationHost.Setup(host => host.WriteFile(@"TheFirstContext\TheFirstScenario.cs", It.IsAny<string>()));
            TheApplicationHost.Setup(host => host.WriteFile(@"TheFirstContext\TheSecondScenario.cs", It.IsAny<string>()));
		}
		
		protected void Given_the_specfile_defines_multiple_contexts()
        {
            _specFile.Contexts.Add("thefirstContext", new Context(new[] { "Context:", "the", "first", "context" }));
            _specFile.Contexts.Add("thesecondContext", new Context(new[] { "Context:", "the", "second", "context" }));

            TheApplicationHost.Setup(host => host.CreateDirectory("TheFirstContext"));
            TheApplicationHost.Setup(host => host.WriteFile(@"TheFirstContext\TheFirstContextContext.cs", It.IsAny<string>()));
            TheApplicationHost.Setup(host => host.CreateDirectory("TheSecondContext"));
            TheApplicationHost.Setup(host => host.WriteFile(@"TheSecondContext\TheSecondContextContext.cs", It.IsAny<string>()));
		}
		
		protected void Given_the_specfile_defines_multiple_scenarios_for_each_context()
        {
            _contexts.Values.ElementAt(0).Scenarios.Add(new Scenario(new[] { "Scenario:", "the", "first", "scenario" }));
            _contexts.Values.ElementAt(0).Scenarios.Add(new Scenario(new[] { "Scenario:", "the", "second", "scenario" }));

            _contexts.Values.ElementAt(1).Scenarios.Add(new Scenario(new[] { "Scenario:", "the", "third", "scenario" }));
            _contexts.Values.ElementAt(1).Scenarios.Add(new Scenario(new[] { "Scenario:", "the", "fourth", "scenario" }));

            TheApplicationHost.Setup(host => host.WriteFile(@"TheFirstContext\TheFirstScenario.cs", It.IsAny<string>()));
            TheApplicationHost.Setup(host => host.WriteFile(@"TheFirstContext\TheSecondScenario.cs", It.IsAny<string>()));
            TheApplicationHost.Setup(host => host.WriteFile(@"TheSecondContext\TheThirdScenario.cs", It.IsAny<string>()));
            TheApplicationHost.Setup(host => host.WriteFile(@"TheSecondContext\TheFourthScenario.cs", It.IsAny<string>()));
		}
		
		protected void When_writing_out_the_generated_tests()
		{
		    var blockWriter = new BlockWriter();
		    var builder = new ContextSpecBuilder(new ContextBuilder(TheApplicationHost.Object, blockWriter),
		                                         new ScenarioBuilder(TheApplicationHost.Object, blockWriter));
            builder.Build(_specFile);
		}
		
	}
	
	[TestFixture]
	public class The_specfile_defines_one_scenario_for_one_context : WritingGeneratedTestsToTheFilesystemContext
	{
		[TestFixtureSetUp]
		protected void BeforeAll()
		{
			Given_a_specfile();
			Given_the_specfile_defines_one_context();
			Given_the_specfile_defines_one_scenario();
			When_writing_out_the_generated_tests();
		}
		
		[TestFixtureTearDown]
		protected void AfterAll()
		{
		}
		
		[Test]
		public void It_should_create_one_folder_for_each_context()
		{
            TheApplicationHost.Verify(host => host.CreateDirectory("TheFirstContext"),Times.Once());
		}
		
		[Test]
		public void It_should_create_one_base_class_file_for_each_context()
        {
            TheApplicationHost.Verify(host => host.WriteFile(@"TheFirstContext\TheFirstContextContext.cs", It.IsAny<string>()), Times.Once());
		}
		
		[Test]
		public void It_should_create_one_test_class_for_each_scenario()
		{
            TheApplicationHost.Verify(host => host.WriteFile(@"TheFirstContext\The_first_scenario.cs", It.IsAny<string>()), Times.Once());
        }
		
	}
	
	[TestFixture]
	public class The_specfile_defines_multiple_scenarios_for_one_context : WritingGeneratedTestsToTheFilesystemContext
	{
		[TestFixtureSetUp]
		protected void BeforeAll()
		{
			Given_a_specfile();
			Given_the_specfile_defines_one_context();
			Given_the_specfile_defines_multiple_scenarios();
			When_writing_out_the_generated_tests();
		}
		
		[TestFixtureTearDown]
		protected void AfterAll()
		{
		}
		
		[Test]
		public void It_should_create_one_folder_for_each_context()
		{
            TheApplicationHost.Verify(host => host.CreateDirectory(It.IsAny<string>()), Times.Once());
        }
		
		[Test]
		public void It_should_create_one_base_class_file_for_each_context()
		{
            TheApplicationHost.Verify(host => host.WriteFile(@"TheFirstContext\TheFirstContextContext.cs", It.IsAny<string>()), Times.Once());
        }
		
		[Test]
		public void It_should_create_one_test_class_for_each_scenario()
		{
            TheApplicationHost.Verify(host => host.WriteFile(@"TheFirstContext\The_first_scenario.cs", It.IsAny<string>()), Times.Once());
            TheApplicationHost.Verify(host => host.WriteFile(@"TheFirstContext\The_second_scenario.cs", It.IsAny<string>()), Times.Once());
        }
		
	}
	
	[TestFixture]
	public class The_specfile_defines_multiple_scenarios_for_multiple_contexts : WritingGeneratedTestsToTheFilesystemContext
	{
		[TestFixtureSetUp]
		protected void BeforeAll()
		{
			Given_a_specfile();
			Given_the_specfile_defines_multiple_contexts();
			Given_the_specfile_defines_multiple_scenarios_for_each_context();
			When_writing_out_the_generated_tests();
		}
		
		[TestFixtureTearDown]
		protected void AfterAll()
		{
		}
		
		[Test]
		public void It_should_create_one_folder_for_each_context()
		{
            TheApplicationHost.Verify(host => host.CreateDirectory(It.IsAny<string>()), Times.Exactly(2));
        }
		
		[Test]
		public void It_should_create_one_base_class_file_for_each_context()
		{
            TheApplicationHost.Verify(host => host.WriteFile(@"TheFirstContext\TheFirstContextContext.cs", It.IsAny<string>()), Times.Once());
            TheApplicationHost.Verify(host => host.WriteFile(@"TheSecondContext\TheSecondContextContext.cs", It.IsAny<string>()), Times.Once());
        }
		
		[Test]
		public void It_should_create_one_test_class_for_each_scenario()
        {
            TheApplicationHost.Verify(host => host.WriteFile(@"TheFirstContext\The_first_scenario.cs", It.IsAny<string>()), Times.Once());
            TheApplicationHost.Verify(host => host.WriteFile(@"TheFirstContext\The_second_scenario.cs", It.IsAny<string>()), Times.Once());
            TheApplicationHost.Verify(host => host.WriteFile(@"TheSecondContext\The_third_scenario.cs", It.IsAny<string>()), Times.Once());
            TheApplicationHost.Verify(host => host.WriteFile(@"TheSecondContext\The_fourth_scenario.cs", It.IsAny<string>()), Times.Once());
        }
		
	}
	
}

