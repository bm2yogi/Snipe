using System;
using NUnit.Framework;

namespace WritingGeneratedTestsToTheFilesystem
{
	public class WritingGeneratedTestsToTheFilesystemContext
	{
		protected void Given_a_specfile()
		{
			// not implemented.
		}
		
		protected void Given_the_specfile_defines_one_context()
		{
			// not implemented.
		}
		
		protected void Given_the_specfile_defines_one_scenario()
		{
			// not implemented.
		}
		
		protected void Given_the_specfile_defines_multiple_scenarios()
		{
			// not implemented.
		}
		
		protected void Given_the_specfile_defines_multiple_contexts()
		{
			// not implemented.
		}
		
		protected void Given_the_specfile_defines_multiple_scenarios_for_each_context()
		{
			// not implemented.
		}
		
		protected void When_writing_out_the_generated_tests()
		{
			// not implemented.
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
			Assert.Fail("Not implemented.");
		}
		
		[Test]
		public void It_should_create_one_base_class_file_for_each_context()
		{
			Assert.Fail("Not implemented.");
		}
		
		[Test]
		public void It_should_create_one_test_class_for_each_scenario()
		{
			Assert.Fail("Not implemented.");
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
			Assert.Fail("Not implemented.");
		}
		
		[Test]
		public void It_should_create_one_base_class_file_for_each_context()
		{
			Assert.Fail("Not implemented.");
		}
		
		[Test]
		public void It_should_create_one_test_class_for_each_scenario()
		{
			Assert.Fail("Not implemented.");
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
			Assert.Fail("Not implemented.");
		}
		
		[Test]
		public void It_should_create_one_base_class_file_for_each_context()
		{
			Assert.Fail("Not implemented.");
		}
		
		[Test]
		public void It_should_create_one_test_class_for_each_scenario()
		{
			Assert.Fail("Not implemented.");
		}
		
	}
	
}

