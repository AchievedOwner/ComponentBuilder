using System.Collections.Generic;

namespace ComponentBuilder.Test
{
    public class ComponentBuilderExtensionsTest : TestBase
    {
        public ComponentBuilderExtensionsTest()
        {

        }

        [Fact]
        public void TestEnumCssClassAttribute()
        {
            TestEnum.Class1.GetCssClass().Should().Be("class1");
            TestEnum.Class2.GetCssClass().Should().Be("myclass");
            PrefixEnum.Value1.GetCssClass().Should().Be("prefix-value1");
            PrefixEnum.Value2.GetCssClass().Should().Be("prefix-value2");
        }
        [Fact]
        public void TestMergeReplace()
        {
            var dic1 = new Dictionary<string, string>
            {
                ["name"] = "admin"
            };

            var dic2 = new Dictionary<string, string>
            {
                ["name"] = "test",
                ["age"] = "12"
            };

            var merged = dic1.Merge(dic2);
            merged.Should().ContainKey("age").And.Contain(m => m.Key == "name" && m.Value == "test");
        }
        [Fact]
        public void TestMergeIgnoreDuplicate()
        {
            var dic1 = new Dictionary<string, string>
            {
                ["name"] = "admin"
            };

            var dic2 = new Dictionary<string, string>
            {
                ["name"] = "test",
                ["age"] = "12"
            };

            var merged = dic1.Merge(dic2, false);
            merged.Should().ContainKey("age").And.Contain(m => m.Key == "name" && m.Value == "admin");
        }

        enum TestEnum
        {
            Class1,
            [CssClass("myclass")] Class2,
        }

        [CssClass("prefix-")]
        enum PrefixEnum
        {
            Value1,
            Value2
        }
    }
}
