﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using ICSharpCode.PackageManagement.Design;
using ICSharpCode.PackageManagement.EnvDTE;
using ICSharpCode.SharpDevelop.Project;
using NUnit.Framework;
using PackageManagement.Tests.Helpers;

namespace PackageManagement.Tests.EnvDTE
{
	[TestFixture]
	public class ReferenceTests
	{
		Reference reference;
		TestableProject msbuildProject;
		FakePackageManagementProjectService fakeProjectService;
		TestableDTEProject project;
		
		void CreateReference(string name)
		{
			project = new TestableDTEProject();
			msbuildProject = project.TestableProject;
			ReferenceProjectItem referenceProjectItem = msbuildProject.AddReference(name);
			fakeProjectService = project.FakeProjectService;
			CreateReference(project, referenceProjectItem);
		}
		
		void CreateReference(Project project, ReferenceProjectItem referenceProjectItem)
		{
			reference = new Reference(project, referenceProjectItem);
		}
		
		TestableProject CreateProjectReference()
		{
			project = new TestableDTEProject();
			msbuildProject = project.TestableProject;
			TestableProject referencedProject = ProjectHelper.CreateTestProject();
			ProjectReferenceProjectItem referenceProjectItem = msbuildProject.AddProjectReference(referencedProject);
			fakeProjectService = project.FakeProjectService;
			CreateReference(project, referenceProjectItem);
			return referencedProject;
		}
		
		[Test]
		public void Name_ReferenceNameIsSystemXml_ReturnsSystemXml()
		{
			CreateReference("System.Xml");
			string name = reference.Name;
			
			Assert.AreEqual("System.Xml", name);
		}
		
		[Test]
		public void Remove_RemoveSystemXmlReferenceFromProject_ProjectReferenceRemoved()
		{
			CreateReference("System.Xml");
			
			reference.Remove();
			
			int count = msbuildProject.Items.Count;
			
			Assert.AreEqual(0, count);
		}
		
		[Test]
		public void Remove_RemoveSystemXmlReferenceFromProject_ProjectIsSaved()
		{
			CreateReference("System.Xml");
			
			reference.Remove();
			
			bool saved = msbuildProject.IsSaved;
			
			Assert.IsTrue(saved);
		}
		
		[Test]
		public void SourceProject_SystemXmlReference_ReturnsNull()
		{
			CreateReference("System.Xml");
			
			Project project = reference.SourceProject;
			
			Assert.IsNull(project);
		}
		
		[Test]
		public void SourceProject_ReferenceIsProjectReference_ReturnsReferencedProject()
		{
			TestableProject referencedProject = CreateProjectReference();
			referencedProject.FileName = @"d:\projects\referencedproject.csproj";
			
			Project project = reference.SourceProject;
			
			Assert.AreEqual(@"d:\projects\referencedproject.csproj", project.FileName);
		}
	}
}
