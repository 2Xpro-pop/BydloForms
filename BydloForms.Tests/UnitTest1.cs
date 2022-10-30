using BydloForms.Helpers;
using System.Diagnostics;

namespace BydloForms.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void TestSetter()
        {
            var target = new Props(123, "„мырь", true);
            var current = new Props();

            var setter = new PropertySetter<Props>();

            setter.Property("число", x => x.IntProp);
            setter.Property("погон€ло", x => x.StrProp);
            setter.Property("реально", x => x.BoolProp);

            var props = new Dictionary<string, string>()
            {
                { "число", "123" },
                { "погон€ло", "„мырь" },
                { "реально", "true" },
            };

            setter.Set(props, current);

            Assert.That(target, Is.EqualTo(current));
        }
    }
}