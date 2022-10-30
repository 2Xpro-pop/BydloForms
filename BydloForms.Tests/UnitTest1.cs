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
            var target = new Props(123, "�����", true);
            var current = new Props();

            var setter = new PropertySetter<Props>();

            setter.Property("�����", x => x.IntProp);
            setter.Property("��������", x => x.StrProp);
            setter.Property("�������", x => x.BoolProp);

            var props = new Dictionary<string, string>()
            {
                { "�����", "123" },
                { "��������", "�����" },
                { "�������", "true" },
            };

            setter.Set(props, current);

            Assert.That(target, Is.EqualTo(current));
        }
    }
}