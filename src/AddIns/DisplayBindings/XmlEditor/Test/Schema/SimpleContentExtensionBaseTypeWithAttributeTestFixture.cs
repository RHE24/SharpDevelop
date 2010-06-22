﻿// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Matthew Ward" email="mrward@users.sourceforge.net"/>
//     <version>$Revision$</version>
// </file>

using System;
using ICSharpCode.XmlEditor;
using NUnit.Framework;

namespace XmlEditor.Tests.Schema
{
	[TestFixture]
	public class SimpleContentExtensionBaseTypeWithAttributeTestFixture : SchemaTestFixtureBase
	{
		XmlCompletionItemCollection linkElementAttributes;
		string schemaNamespace = "http://ddue.schemas.microsoft.com/authoring/2003/5";
		
		public override void FixtureInit()
		{
			XmlElementPath path = new XmlElementPath();
			path.AddElement(new QualifiedName("link", schemaNamespace));
			path.NamespacesInScope.Add(new XmlNamespace("xlink", "http://www.w3.org/1999/xlink"));
			
			linkElementAttributes = SchemaCompletion.GetAttributeCompletion(path);
			linkElementAttributes.Sort();
		}
		
		[Test]
		public void LinkElementHasAddressAndXlinkHrefAttribute()
		{
			XmlCompletionItemCollection expectedAttributes = new XmlCompletionItemCollection();
			expectedAttributes.Add(new XmlCompletionItem("address", XmlCompletionItemType.XmlAttribute));
			expectedAttributes.Add(new XmlCompletionItem("xlink:href", XmlCompletionItemType.XmlAttribute));
			
			Assert.AreEqual(expectedAttributes.ToArray(), linkElementAttributes.ToArray());
		}
		
		protected override string GetSchema()
		{
			return 
				"<schema xmlns='http://www.w3.org/2001/XMLSchema'\r\n" +
				"    xmlns:maml='http://ddue.schemas.microsoft.com/authoring/2003/5' \r\n" +
				"    xmlns:doc='http://ddue.schemas.microsoft.com/authoring/internal'\r\n" +
				"    xmlns:xlink='http://www.w3.org/1999/xlink'\r\n" +
				"    targetNamespace='http://ddue.schemas.microsoft.com/authoring/2003/5' \r\n" +
				"    elementFormDefault='qualified'\r\n" +
				"    attributeFormDefault='unqualified'>\r\n" +
				"\r\n" +
				"<element ref='maml:link' />\r\n" +
				"<element name='link' type='maml:inlineLinkType' />\r\n" +
 				"<element name='legacyLink' type='maml:inlineLinkType' />\r\n" +
				"\r\n" +
				"<complexType name='inlineLinkType' mixed='true'>\r\n" +
				"    <simpleContent>\r\n" +
				"        <extension base='maml:textType'>\r\n" +
				"            <attributeGroup ref='maml:linkingGroup' />\r\n" +
				"        </extension>\r\n" +
				"    </simpleContent>\r\n" +
				"</complexType>\r\n" +
				"\r\n" +
				"<complexType name='textType'>\r\n" +
				"    <simpleContent>\r\n" +
				"        <extension base='normalizedString'>\r\n" +
				"            <attributeGroup ref='maml:contentIdentificationSharingAndConditionGroup'/>\r\n" +
				"        </extension>\r\n" +
				"    </simpleContent>\r\n" +
				"</complexType>\r\n" +
				"\r\n" +
				"<attributeGroup name='contentIdentificationSharingAndConditionGroup'>\r\n" +
				"    <attributeGroup ref='maml:addressAttributeGroup'/>\r\n" +
				"</attributeGroup>\r\n" +
				"\r\n" +
				"    <attributeGroup name='addressAttributeGroup'>\r\n" +
				"        <attribute name='address' type='ID'/>\r\n" +
				"    </attributeGroup>\r\n" +
				"\r\n" +
				"    <attributeGroup name='linkingGroup'>\r\n" +
				"        <attribute ref='xlink:href'/>\r\n" +
				"    </attributeGroup>\r\n" +
				"</schema>";
		}
	}
}