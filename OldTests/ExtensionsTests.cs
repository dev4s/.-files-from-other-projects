using System;
using NUnit.Framework;

namespace Dev4s.WebClient.UnitTests
{
    [TestFixture]
    public class ExtensionsTests
    {
        private class TestClass { }

        [Test]
        public void ReplaceDoesNotAllowSomeValues()
        {
            //Arrange
            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => string.Empty.Replace(0, "234"));
            Assert.Throws<ArgumentNullException>(() => "test/{0}".Replace(0, null));

            Assert.Throws<ArgumentException>(() => "test/{0}".Replace(0, new TestClass()));
            Assert.Throws<ArgumentException>(() => "test".Replace(0, "asda"));
        }

        [Test]
        public void ReplaceShouldReplaceAllCharactersWithSpecifiedObjects()
        {
            //Arrange
            const string text1 = "{0}/{1}";
            const string text2 = "{{0}/{1}";
            const string text3 = "{0}}/{1}";

            //Act
            var result1 = text1.Replace(0, "bla");
            var result2 = text2.Replace(0, "bla");
            var result3 = text3.Replace(0, "bla");

            var result4 = text1.Replace(1, "bla");
            var result5 = text2.Replace(1, "bla");
            var result6 = text3.Replace(1, "bla");

            var result7 = "{123}/{124}".Replace(123, "bla");
            var result8 = "{123}/{124}".Replace(124, "bla");
            var result9 = "{{123}/{124}".Replace(123, "bla");
            var result10 = "{123}}/{124}".Replace(123, "bla");
            var result11 = "{123}}/{124}".Replace(124, "bla");

            var result12 = text1.Replace(0, string.Empty).Replace(1, string.Empty);
            var result13 = "{0}/{1}/{2}".Replace(0, (uint)12).Replace(1, 12).Replace(2, 'c');

            var result14 = "{/}".Replace(0, 12);
            var result15 = "}/{".Replace(0, 12);


            //Assert
            Assert.That(result1, Is.EqualTo("bla/{1}"));
            Assert.That(result2, Is.EqualTo("{bla/{1}"));
            Assert.That(result3, Is.EqualTo("bla}/{1}"));

            Assert.That(result4, Is.EqualTo("{0}/bla"));
            Assert.That(result5, Is.EqualTo("{{0}/bla"));
            Assert.That(result6, Is.EqualTo("{0}}/bla"));

            Assert.That(result7, Is.EqualTo("bla/{124}"));
            Assert.That(result8, Is.EqualTo("{123}/bla"));
            Assert.That(result9, Is.EqualTo("{bla/{124}"));
            Assert.That(result10, Is.EqualTo("bla}/{124}"));
            Assert.That(result11, Is.EqualTo("{123}}/bla"));

            Assert.That(result12, Is.EqualTo("/"));
            Assert.That(result13, Is.EqualTo("12/12/c"));

            Assert.That(result14, Is.EqualTo("{/}"));
            Assert.That(result15, Is.EqualTo("}/{"));
        }
    }
}