using CsharpExpressionDumper.Abstractions;
using CsharpExpressionDumper.ConstructorResolvers;
using CsharpExpressionDumper.Extensions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace CsharpExpressionDumper.Tests
{
    public class CsharpExpressionDumperTests
    {
        [Fact]
        public void Can_Dump_String_To_Csharp()
        {
            // Arrange
            const string input = "hello";

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be("@\"hello\"");
        }

        [Fact]
        public void Can_Dump_String_With_Double_Quotes_To_Csharp()
        {
            // Arrange
            const string input = "\"hello\"";

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be("@\"\"\"hello\"\"\"");
        }

        [Fact]
        public void Can_Dump_String_With_NewLine_To_Csharp()
        {
            // Arrange
            const string input = @"
";

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be(@"@""
""");
        }

        [Fact]
        public void Can_Dump_String_With_At_Sign_To_Csharp()
        {
            // Arrange
            const string input = "@";

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be("@\"@\"");
        }

        [Fact]
        public void Can_Dump_Null_To_Csharp()
        {
            // Act
            var actual = Dump((object)null);

            // Assert
            actual.Should().Be("null");
        }

        [Fact]
        public void Can_Dump_Int_To_Csharp()
        {
            // Act
            var actual = Dump(1);

            // Assert
            actual.Should().Be("1");
        }

        [Fact]
        public void Can_Dump_Double_To_Csharp()
        {
            // Act
            var actual = Dump(1.5);

            // Assert
            actual.Should().Be("1.5");
        }

        [Fact]
        public void Can_Dump_Boolean_To_Csharp()
        {
            // Act
            var actual = Dump(true);

            // Assert
            actual.Should().Be("true");
        }

        [Fact]
        public void Can_Dump_Enum_To_Csharp()
        {
            // Act
            var actual = Dump(MyEnumeration.First);

            // Assert
            actual.Should().Be("CsharpExpressionDumper.Tests.MyEnumeration.First");
        }

        [Fact]
        public void Can_Dump_DateTime_To_Csharp()
        {
            // Arrange
            var input = new DateTime(2000, 1, 1);

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be("new System.DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)");
        }

        [Fact]
        public void Can_Dump_TimeSpan_To_Csharp()
        {
            // Arrange
            var input = new TimeSpan(1, 2, 3, 4, 5);

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be("new System.TimeSpan(1, 2, 3, 4, 5)");
        }

        [Fact]
        public void Can_Dump_Char_To_Csharp()
        {
            // Act
            var actual = Dump('z');

            // Assert
            actual.Should().Be("'z'");
        }

        [Fact]
        public void Can_Dump_Char_Array_To_Csharp()
        {
            // Arrange
            var input = new[] { 'a', 'b', 'c' };

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be(@"new[]
{
    'a',
    'b',
    'c',
}");
        }

        [Fact]
        public void Can_Dump_Guid_To_Csharp()
        {
            // Act
            var actual = Dump(new Guid("9f1ffe01-c83b-436a-9c5c-3f1b82dcd616"));

            // Assert
            actual.Should().Be(@"new System.Guid(""9f1ffe01-c83b-436a-9c5c-3f1b82dcd616"")");
        }

        [Fact]
        public void Can_Dump_KeyValuePair_To_Csharp()
        {
            // Arrange
            var input = new KeyValuePair<string, object>("Key", 123.45);

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be(@"new System.Collections.Generic.KeyValuePair<System.String, System.Object>(@""Key"", 123.45)");
        }

        [Fact]
        public void Can_Dump_Type_To_Csharp()
        {
            // Arrange
            var input = typeof(string);

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be("typeof(System.String)");
        }

        [Fact]
        public void Can_Dump_Uri_To_Csharp()
        {
            // Arrange
            var input = new Uri("http://www.google.com/");

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be(@"new System.Uri(""http://www.google.com/"")");
        }

        [Fact]
        public void Can_Dump_Version_To_Csharp()
        {
            // Arrange
            var input = new Version(1, 2, 3, 4);

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be("new System.Version(1, 2, 3, 4)");
        }

        [Fact]
        public void Can_Dump_Tuple_2_To_Csharp()
        {
            // Arrange
            var input = new Tuple<string, int>("hello", 123);

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be(@"new System.Tuple<System.String,System.Int32>
(
    item1: @""hello"",
    item2: 123
)");
        }

        [Fact]
        public void Can_Dump_Tuple_3_To_Csharp()
        {
            // Arrange
            var input = new Tuple<string, int, bool>("hello", 123, true);

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be(@"new System.Tuple<System.String,System.Int32,System.Boolean>
(
    item1: @""hello"",
    item2: 123,
    item3: true
)");
        }

        [Fact]
        public void Can_Dump_ValueTuple_2_To_Csharp()
        {
            // Arrange
            var input = new ValueTuple<string, int>("test", 19);

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be(@"new System.ValueTuple<System.String, System.Int32>(@""test"", 19)");
        }

        [Fact]
        public void Can_Dump_ValueTuple_3_To_Csharp()
        {
            // Arrange
            var input = new ValueTuple<string, int, bool>("test", 19, true);

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be(@"new System.ValueTuple<System.String, System.Int32, System.Boolean>(@""test"", 19, true)");
        }

        [Fact]
        public void Can_Dump_ValueTuple_4_To_Csharp()
        {
            // Arrange
            var input = new ValueTuple<string, int, bool, bool>("test", 19, true, false);

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be(@"new System.ValueTuple<System.String, System.Int32, System.Boolean, System.Boolean>(@""test"", 19, true, false)");
        }

        [Fact]
        public void Can_Dump_ValueTuple_5_To_Csharp()
        {
            // Arrange
            var input = new ValueTuple<string, int, bool, bool, string>("test", 19, true, false, "pfff");

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be(@"new System.ValueTuple<System.String, System.Int32, System.Boolean, System.Boolean, System.String>(@""test"", 19, true, false, @""pfff"")");
        }

        [Fact]
        public void Can_Dump_StringArray_To_Csharp()
        {
            // Act
            var actual = Dump(new[] { "a", "b" });

            // Assert
            actual.Should().Be(@"new[]
{
    @""a"",
    @""b"",
}");
        }

        [Fact]
        public void Can_Dump_ObjectArray_To_Csharp()
        {
            // Act
            var actual = Dump(new object[] { "a", 1, false });

            // Assert
            actual.Should().Be(@"new System.Object[]
{
    @""a"",
    1,
    false,
}");
        }

        [Fact]
        public void Can_Dump_GenericListOfObject_To_Csharp()
        {
            // Arrange
            var input = new List<object>(new object[] { "a", 1, false });

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be(@"new System.Collections.Generic.List<System.Object>(new System.Object[]
{
    @""a"",
    1,
    false,
} )");
        }

        [Fact]
        public void Can_Dump_Filled_GenericListOfBaseClass_To_Csharp()
        {
            // Arrange
            var input = new List<MyBaseClass>(new MyBaseClass[] { new MyBaseClass(), new MyOverrideClass() });

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be(@"new System.Collections.Generic.List<CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyBaseClass>(new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyBaseClass[]
{
    new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyBaseClass(),
    new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyOverrideClass(),
} )");
        }

        [Fact]
        public void Can_Dump_Empty_GenericListOfBaseClass_To_Csharp()
        {
            // Arrange
            var input = new List<MyBaseClass>();

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be(@"new System.Collections.Generic.List<CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyBaseClass>(new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyBaseClass[]
{
} )");
        }

        [Fact]
        public void Can_Dump_Flat_Class_To_Csharp()
        {
            // Arrange
            var input = new MyFlatClass { Property1 = "hello", Property2 = 1, Property3 = true };

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be(@"new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyFlatClass
{
    Property1 = @""hello"",
    Property2 = 1,
    Property3 = true,
}");
        }

        [Fact]
        public void Can_Dump_Recursive_Class_To_Csharp()
        {
            // Arrange
            var input = new MyRecursiveClass
            {
                Property1 = "Root",
                Property2 = new MyRecursiveClass
                {
                    Property1 = "Child",
                    Property2 = new MyRecursiveClass
                    {
                        Property1 = "Grandchild"
                    }
                }
            };

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be(@"new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyRecursiveClass
{
    Property1 = @""Root"",
    Property2 = new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyRecursiveClass
    {
        Property1 = @""Child"",
        Property2 = new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyRecursiveClass
        {
            Property1 = @""Grandchild"",
            Property2 = null,
        },
    },
}");
        }

        [Fact]
        public void Can_Dump_Class_With_StringArray_To_Csharp()
        {
            // Arrange
            var input = new MyClassWithStringArray { Property1 = "test", Property2 = new[] { "item1", "item2" } };

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be(@"new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyClassWithStringArray
{
    Property1 = @""test"",
    Property2 = new[]
    {
        @""item1"",
        @""item2"",
    },
}");
        }

        [Fact]
        public void Can_Dump_Class_With_FlatClassArray_To_Csharp()
        {
            // Arrange
            var input = new MyClassWithFlatClassArray { Property1 = "test", Property2 = new[] { new MyFlatClass(), new MyFlatClass() } };

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be(@"new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyClassWithFlatClassArray
{
    Property1 = @""test"",
    Property2 = new[]
    {
        new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyFlatClass
        {
            Property1 = null,
            Property2 = 0,
            Property3 = false,
        },
        new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyFlatClass
        {
            Property1 = null,
            Property2 = 0,
            Property3 = false,
        },
    },
}");
        }

        [Fact]
        public void Can_Dump_Class_With_FlatClassICollection_To_Csharp()
        {
            // Arrange
            var input = new MyClassWithFlatClassICollection { Property1 = "test", Property2 = new[] { new MyFlatClass(), new MyFlatClass { Property1 = "test" } } };

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be(@"new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyClassWithFlatClassICollection
{
    Property1 = @""test"",
    Property2 = new[]
    {
        new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyFlatClass
        {
            Property1 = null,
            Property2 = 0,
            Property3 = false,
        },
        new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyFlatClass
        {
            Property1 = @""test"",
            Property2 = 0,
            Property3 = false,
        },
    },
}");
        }

        [Fact]
        public void Can_Dump_Class_With_FlatClassCollection_To_Csharp()
        {
            // Arrange
            var input = new MyClassWithFlatClassCollection { Property1 = "test", Property2 = new Collection<MyFlatClass>(new[] { new MyFlatClass(), new MyFlatClass { Property1 = "test" } }) };

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be(@"new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyClassWithFlatClassCollection
{
    Property1 = @""test"",
    Property2 = new System.Collections.ObjectModel.Collection<CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyFlatClass>(new[]
    {
        new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyFlatClass
        {
            Property1 = null,
            Property2 = 0,
            Property3 = false,
        },
        new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyFlatClass
        {
            Property1 = @""test"",
            Property2 = 0,
            Property3 = false,
        },
    } ),
}");
        }

        [Fact]
        public void Can_Dump_Class_With_FlatClassIReadOnlyCollection_To_Csharp()
        {
            // Arrange
            var input = new MyClassWithFlatClassIReadOnlyCollection { Property1 = "test", Property2 = new[] { new MyFlatClass(), new MyFlatClass { Property1 = "test" } } };

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be(@"new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyClassWithFlatClassIReadOnlyCollection
{
    Property1 = @""test"",
    Property2 = new[]
    {
        new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyFlatClass
        {
            Property1 = null,
            Property2 = 0,
            Property3 = false,
        },
        new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyFlatClass
        {
            Property1 = @""test"",
            Property2 = 0,
            Property3 = false,
        },
    },
}");
        }

        [Fact]
        public void Can_Dump_Class_With_FlatClassReadOnlyCollection_To_Csharp()
        {
            // Arrange
            var input = new MyClassWithFlatClassReadOnlyCollection { Property1 = "test", Property2 = new ReadOnlyCollection<MyFlatClass>(new[] { new MyFlatClass(), new MyFlatClass { Property1 = "test" } }) };

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be(@"new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyClassWithFlatClassReadOnlyCollection
{
    Property1 = @""test"",
    Property2 = new System.Collections.ObjectModel.ReadOnlyCollection<CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyFlatClass>(new[]
    {
        new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyFlatClass
        {
            Property1 = null,
            Property2 = 0,
            Property3 = false,
        },
        new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyFlatClass
        {
            Property1 = @""test"",
            Property2 = 0,
            Property3 = false,
        },
    } ),
}");
        }

        [Fact]
        public void Can_Dump_Class_With_FlatClassIList_To_Csharp()
        {
            // Arrange
            var input = new MyClassWithFlatClassIList { Property1 = "test", Property2 = new[] { new MyFlatClass(), new MyFlatClass { Property1 = "test" } } };

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be(@"new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyClassWithFlatClassIList
{
    Property1 = @""test"",
    Property2 = new[]
    {
        new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyFlatClass
        {
            Property1 = null,
            Property2 = 0,
            Property3 = false,
        },
        new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyFlatClass
        {
            Property1 = @""test"",
            Property2 = 0,
            Property3 = false,
        },
    },
}");
        }

        [Fact]
        public void Can_Dump_Class_With_FlatClassList_To_Csharp()
        {
            // Arrange
            var input = new MyClassWithFlatClassList { Property1 = "test", Property2 = new List<MyFlatClass>(new[] { new MyFlatClass(), new MyFlatClass { Property1 = "test" } }) };

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be(@"new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyClassWithFlatClassList
{
    Property1 = @""test"",
    Property2 = new System.Collections.Generic.List<CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyFlatClass>(new[]
    {
        new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyFlatClass
        {
            Property1 = null,
            Property2 = 0,
            Property3 = false,
        },
        new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyFlatClass
        {
            Property1 = @""test"",
            Property2 = 0,
            Property3 = false,
        },
    } ),
}");
        }

        [Fact]
        public void Can_Dump_Class_With_FlatClassIDictionary_To_Csharp()
        {
            // Arrange
            var input = new MyClassWithIDictionary
            {
                Property1 = new Dictionary<string, object>
                {
                    ["key1"] = "test",
                    ["key2"] = 2
                }
            };

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be(@"new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyClassWithIDictionary
{
    Property1 = new System.Collections.Generic.Dictionary<System.String, System.Object>
    {
        [@""key1""] = @""test"",
        [@""key2""] = 2,
    },
}");
        }

        [Fact]
        public void Can_Dump_AnonyousObject_To_Csharp()
        {
            // Arrange
            var input = new { Property1 = "test", Property2 = 2 };

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be(@"new 
{
    Property1 = @""test"",
    Property2 = 2,
}");
        }

        [Fact]
        public void Can_Dump_AnonyousObjectArray_To_Csharp()
        {
            // Arrange
            var input = new[]
            {
                 new { Property1 = "test", Property2 = 2 },
                 new { Property1 = "TEST", Property2 = 3 },
            };

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be(@"new[]
{
    new 
    {
        Property1 = @""test"",
        Property2 = 2,
    },
    new 
    {
        Property1 = @""TEST"",
        Property2 = 3,
    },
}");
        }

        [Fact]
        public void Can_Dump_DynamicObject_To_Csharp()
        {
            // Arrange
            dynamic input = new ExpandoObject();
            input.Property1 = "test";
            input.Property2 = 3;

            // Act
            var actual = (string)Dump(input, new ExpandoObjectHandler("input"));

            // Assert
            actual.Should().Be(@"dynamic input = new System.Dynamic.ExpandoObject();
input.Property1 = @""test"";
input.Property2 = 3;
");
        }

        [Fact]
        public void Can_Dump_Class_With_ReadOnlyProperty()
        {
            // Arrange
            var input = new MyClassWithReadOnlyProperty { Property1 = "Test" };

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be(@"new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyClassWithReadOnlyProperty
{
    Property1 = @""Test"",
}");
        }

        [Fact]
        public void Can_Dump_Class_With_Only_ReadOnly_Properties()
        {
            // Arrange
            var input = new MyClassWithOnlyReadOnlyProperties();

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be("new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyClassWithOnlyReadOnlyProperties()");
        }

        [Fact]
        public void Can_Dump_Immutable_Class_With_One_Constructor()
        {
            // Arrange
            var input = new MyImmutableClass("test", 2);

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be(@"new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyImmutableClass
(
    property1: @""test"",
    property2: 2
)");
        }

        [Fact]
        public void Can_Dump_Immutable_Class_With_Two_Constructors()
        {
            // Arrange
            var input = new MyImmutableClassWithTwoCtors("test", 2);

            // Act
            var actual = Dump(input, new IConstructorResolver[]
            {
                new CustomConstructorResolver(),
                new DefaultConstructorResolver()
            });

            // Assert
            actual.Should().Be(@"new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyImmutableClassWithTwoCtors
(
    property1: @""test"",
    property2: 2
)");
        }

        [Fact]
        public void Can_Dump_Half_Immutable_Class_With_One_Constructor_CtorArgumentIsNotWritable()
        {
            // Arrange
            var input = new MyHalfImmutableClass1("test") { Property2 = 2 };

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be(@"new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyHalfImmutableClass1
(
    property1: @""test""
)
{
    Property2 = 2,
}");
        }

        [Fact]
        public void Can_Dump_Half_Immutable_Class_With_One_Constructor_CtorArgumentIsWritable()
        {
            // Arrange
            var input = new MyHalfImmutableClass2("test") { Property2 = 2 };

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be(@"new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyHalfImmutableClass2
(
    property1: @""test""
)
{
    Property2 = 2,
}");
        }

        [Fact]
        public void Can_Dump_Nested_Immutable_Class_With_One_Constructor()
        {
            // Arrange
            var input = new MyNestedImmutableClass("Root", 1, new MyNestedImmutableClass("Child", 2, new MyNestedImmutableClass("Grandchild", 3, null)));

            // Act
            var actual = Dump(input);

            // Assert
            actual.Should().Be(@"new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyNestedImmutableClass
(
    property1: @""Root"",
    property2: 1,
    property3: new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyNestedImmutableClass
    (
        property1: @""Child"",
        property2: 2,
        property3: new CsharpExpressionDumper.Tests.CsharpExpressionDumperTests.MyNestedImmutableClass
        (
            property1: @""Grandchild"",
            property2: 3,
            property3: null
        )
    )
)");
        }

        [Fact]
        public void Can_Dump_Class_With_Custom_TypeHandler()
        {
            // Arrange
            var input = new MyImmutableClass("test", 3);

            // Act
            var actual = Dump(input, new MyCustomTypeHandler());

            // Assert
            actual.Should().Be(@"MyImmutableClass.Create(@""test"", 3)");
        }

        [Fact]
        public void Dump_Throws_InvalidOperationExcepion_When_No_ObjectHandler_Supports_The_Object_Instance()
        {
            // Arrange
            var sut = new CsharpExpressionDumper
            (
                Enumerable.Empty<IObjectHandler>(),
                Enumerable.Empty<ICustomTypeHandler>(),
                Default.TypeNameFormatters,
                Default.ConstructorResolvers,
                Default.ReadOnlyPropertyResolvers
            );

            // Act & Assert
            sut.Invoking(x => x.Dump("hello world"))
               .Should().Throw<InvalidOperationException>()
               .WithMessage("There is no object handler which supports object of type [System.String]");
        }

        private string Dump<T>(T input, params ICustomTypeHandler[] customTypeHandlers)
        {
            var sut = new CsharpExpressionDumper
            (
                Default.ObjectHandlers,
                customTypeHandlers.Concat(Default.CustomTypeHandlers),
                Default.TypeNameFormatters,
                Default.ConstructorResolvers,
                Default.ReadOnlyPropertyResolvers
            );
            return sut.Dump(input, typeof(T));
        }

        private string Dump<T>(T input, IConstructorResolver[] constructorResolvers)
        {
            var sut = new CsharpExpressionDumper
            (
                Default.ObjectHandlers,
                Default.CustomTypeHandlers,
                Default.TypeNameFormatters,
                constructorResolvers,
                Default.ReadOnlyPropertyResolvers
            );
            return sut.Dump(input, typeof(T));
        }

        public class MyFlatClass
        {
            public string Property1 { get; set; }
            public int Property2 { get; set; }
            public bool Property3 { get; set; }
        }

        public class MyRecursiveClass
        {
            public string Property1 { get; set; }
            public MyRecursiveClass Property2 { get; set; }
        }

        public class MyClassWithStringArray
        {
            public string Property1 { get; set; }
            public string[] Property2 { get; set; }
        }

        public class MyClassWithFlatClassArray
        {
            public string Property1 { get; set; }
            public MyFlatClass[] Property2 { get; set; }
        }

        public class MyClassWithFlatClassICollection
        {
            public string Property1 { get; set; }
            public ICollection<MyFlatClass> Property2 { get; set; }
        }

        public class MyClassWithFlatClassCollection
        {
            public string Property1 { get; set; }
            public Collection<MyFlatClass> Property2 { get; set; }
        }

        public class MyClassWithFlatClassIReadOnlyCollection
        {
            public string Property1 { get; set; }
            public IReadOnlyCollection<MyFlatClass> Property2 { get; set; }
        }

        public class MyClassWithFlatClassReadOnlyCollection
        {
            public string Property1 { get; set; }
            public ReadOnlyCollection<MyFlatClass> Property2 { get; set; }
        }

        public class MyClassWithFlatClassIList
        {
            public string Property1 { get; set; }
            public IList<MyFlatClass> Property2 { get; set; }
        }

        public class MyClassWithFlatClassList
        {
            public string Property1 { get; set; }
            public List<MyFlatClass> Property2 { get; set; }
        }

        public class MyClassWithIDictionary
        {
            public IDictionary<string, object> Property1 { get; set; }
        }

        public class MyClassWithDictionary
        {
            public Dictionary<string, object> Property1 { get; set; }
        }

        public class MyClassWithReadOnlyProperty
        {
            public string Property1 { get; set; }
            public int Property2 { get; }

            public MyClassWithReadOnlyProperty()
            {
                Property2 = 3;
            }
        }

        public class MyClassWithOnlyReadOnlyProperties
        {
            public string Property1 { get; }
            public int Property2 { get; }

            public MyClassWithOnlyReadOnlyProperties()
            {
                Property1 = "Test";
                Property2 = 13;
            }
        }

        public class MyImmutableClass
        {
            public string Property1 { get; }
            public int Property2 { get; }

            public MyImmutableClass(string property1, int property2)
            {
                Property1 = property1;
                Property2 = property2;
            }
        }

        public class MyImmutableClassWithTwoCtors
        {
            public string Property1 { get; }
            public int Property2 { get; }

            public MyImmutableClassWithTwoCtors(string property1)
            {
                Property1 = property1;
            }

            public MyImmutableClassWithTwoCtors(string property1, int property2)
            {
                Property1 = property1;
                Property2 = property2;
            }
        }

        public class CustomConstructorResolver : IConstructorResolver
        {
            public ConstructorInfo Resolve(Type type)
            {
                if (type == typeof(MyImmutableClassWithTwoCtors))
                {
                    return type.GetConstructors().Single(x => x.GetParameters().Length == 2);
                }

                return null;
            }
        }

        public class MyHalfImmutableClass1
        {
            public string Property1 { get; }
            public int Property2 { get; set; }

            public MyHalfImmutableClass1(string property1)
            {
                Property1 = property1;
            }
        }

        public class MyHalfImmutableClass2
        {
            public string Property1 { get; set; }
            public int Property2 { get; set; }

            public MyHalfImmutableClass2(string property1)
            {
                Property1 = property1;
            }
        }

        public class MyNestedImmutableClass
        {
            public string Property1 { get; }
            public int Property2 { get; }
            public MyNestedImmutableClass Property3 { get; }

            public MyNestedImmutableClass(string property1, int property2, MyNestedImmutableClass property3)
            {
                Property1 = property1;
                Property2 = property2;
                Property3 = property3;
            }
        }

        public class MyCustomTypeHandler : ICustomTypeHandler
        {
            public bool Process(CustomTypeHandlerCommand command, ICsharpExpressionDumperCallback callback)
            {
                if (command.Instance is MyImmutableClass imm)
                {
                    callback.ChainAppendPrefix()
                            .ChainAppend("MyImmutableClass.Create(")
                            .ChainAppendFormattedString(imm.Property1)
                            .ChainAppend(", ")
                            .ChainAppend(imm.Property2)
                            .ChainAppend(')')
                            .ChainAppendSuffix();
                    return true;
                }

                return false;
            }
        }

        public class ExpandoObjectHandler : ICustomTypeHandler
        {
            private readonly string _variableName;

            public ExpandoObjectHandler(string variableName)
            {
                _variableName = variableName;
            }

            public bool Process(CustomTypeHandlerCommand command, ICsharpExpressionDumperCallback callback)
            {
                if (command.Instance is ExpandoObject expandoObject)
                {
                    callback.AppendLine($"dynamic {_variableName} = new System.Dynamic.ExpandoObject();");
                    foreach (var keyValuePair in expandoObject)
                    {
                        callback.Append(new string(' ', command.Level * 4));
                        callback.Append($"{_variableName}.{keyValuePair.Key} = ");
                        callback.ProcessRecursive(keyValuePair.Value, keyValuePair.Value?.GetType(), command.Level);
                        callback.AppendLine(";");
                    }
                    return true;
                }

                return false;
            }
        }

        public class MyBaseClass
        {
        }

        public class MyOverrideClass : MyBaseClass
        {
        }
    }

    public enum MyEnumeration
    {
        First,
        Second
    }
}
