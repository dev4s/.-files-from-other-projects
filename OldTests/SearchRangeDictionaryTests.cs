using NUnit.Framework;

namespace Dev4s.WebClient.UnitTests
{
    [TestFixture]
    public class SearchRangeDictionaryTests
    {
        [Test]
        public void AddingRangesShouldWorkFromMethod()
        {
            //Arrange
            var testSearch = new SearchRange();
            // ReSharper disable UseObjectOrCollectionInitializer
            var searchDict = new SearchRangeDictionary();
            // ReSharper restore UseObjectOrCollectionInitializer

            //Act
            searchDict.Add(testSearch);

            //Assert
            AddingTest(searchDict, testSearch);
        }

        [Test]
        public void AddingRangesShouldWorkFromCollectionInitializer()
        {
            //Arrange
            var testSearch = new SearchRange();

            //Act
            var searchDict = new SearchRangeDictionary { testSearch };

            //Assert
            AddingTest(searchDict, testSearch);
        }

        [Test]
        public void AddingSearchRangeArrayFromCollectionInitializer()
        {
            //Arrange
            var testSearchArray = new[] { new SearchRange() };

            //Act
            var searchDict = new SearchRangeDictionary { testSearchArray };

            //Assert
            AddingTest(searchDict, testSearchArray[0]);
        }

        [Test]
        public void AddingSearchRangeArrayFromMethod()
        {
            //Arrange
            var testSearchArray = new[] { new SearchRange() };
            // ReSharper disable UseObjectOrCollectionInitializer
            var searchDict = new SearchRangeDictionary();
            // ReSharper restore UseObjectOrCollectionInitializer

            //Act
            searchDict.Add(testSearchArray);

            //Assert
            AddingTest(searchDict, testSearchArray[0]);
        }

        [Test]
        public void AddingSearchRangeArrayDoesItStayInOrder()
        {
            //Arrange
            var testSearchArray = new[] { new SearchRange(), new SearchRange(), new SearchRange() };

            //Act
            var searchDict = new SearchRangeDictionary { testSearchArray };

            //Assert
            Assert.That(searchDict[0], Is.EqualTo(testSearchArray[0]));
            Assert.That(searchDict[0], Is.Not.EqualTo(testSearchArray[1]));
            Assert.That(searchDict[0], Is.Not.EqualTo(testSearchArray[2]));

            Assert.That(searchDict[1], Is.Not.EqualTo(testSearchArray[0]));
            Assert.That(searchDict[1], Is.EqualTo(testSearchArray[1]));
            Assert.That(searchDict[1], Is.Not.EqualTo(testSearchArray[2]));

            Assert.That(searchDict[2], Is.Not.EqualTo(testSearchArray[0]));
            Assert.That(searchDict[2], Is.Not.EqualTo(testSearchArray[1]));
            Assert.That(searchDict[2], Is.EqualTo(testSearchArray[2]));
        }

        #region
        private static void AddingTest(SearchRangeDictionary searchDict, SearchRange testSearch)
        {
            Assert.That(searchDict.Count, Is.EqualTo(1));
            Assert.That(searchDict[0], Is.EqualTo(testSearch));
        }
        #endregion

        [Test]
        public void CheckIfWeCanRemoveOneOfSearchRangeFromDictonary()
        {
            RemoveTest(2);
        }

        [Test]
        public void RemoveInnerSearchRangeShouldRecalculateAllOfKeys()
        {
            RemoveTest(0);
            RemoveTest(1);
        }

        #region For remove test
        private static void RemoveTest(int key)
        {
            //Arrange
            var testSearch = new[] { new SearchRange(), new SearchRange(), new SearchRange() };
            var searchDict1 = new SearchRangeDictionary { testSearch };
            var searchDict2 = new SearchRangeDictionary { testSearch };

            //Act
            searchDict1.Remove(testSearch[key]);
            searchDict2.Remove(key);

            //Assert
            switch (key)
            {
                case 0:
                    Assert.That(searchDict1[0], Is.EqualTo(testSearch[1]));
                    Assert.That(searchDict1[1], Is.EqualTo(testSearch[2]));
                    Assert.That(searchDict2[0], Is.EqualTo(testSearch[1]));
                    Assert.That(searchDict2[1], Is.EqualTo(testSearch[2]));
                    break;

                case 1:
                    Assert.That(searchDict1[0], Is.EqualTo(testSearch[0]));
                    Assert.That(searchDict1[1], Is.EqualTo(testSearch[2]));
                    Assert.That(searchDict2[0], Is.EqualTo(testSearch[0]));
                    Assert.That(searchDict2[1], Is.EqualTo(testSearch[2]));
                    break;

                case 2:
                    Assert.That(searchDict1[0], Is.EqualTo(testSearch[0]));
                    Assert.That(searchDict1[1], Is.EqualTo(testSearch[1]));
                    Assert.That(searchDict2[0], Is.EqualTo(testSearch[0]));
                    Assert.That(searchDict2[1], Is.EqualTo(testSearch[1]));
                    break;
            }

            Assert.That(searchDict1.Count, Is.EqualTo(2));
            Assert.That(searchDict2.Count, Is.EqualTo(2));
        }
        #endregion

        [Test]
        public void RemoveNotExistingSearchRangeShouldDoNothing()
        {
            //Arrange
            var notExistingSearchRange = new SearchRange();
            var existingSearchRange1 = new SearchRange();
            var existingSearchRange2 = new SearchRange();
            var searchDict1 = new SearchRangeDictionary { existingSearchRange1, existingSearchRange2 };
            var searchDict2 = new SearchRangeDictionary { existingSearchRange1, existingSearchRange2 };

            //Act
            //Assert
            Assert.DoesNotThrow(() => searchDict1.Remove(notExistingSearchRange));
            Assert.That(searchDict1[0], Is.EqualTo(existingSearchRange1));
            Assert.That(searchDict1[1], Is.EqualTo(existingSearchRange2));
            Assert.That(searchDict1.Count, Is.EqualTo(2));
            Assert.DoesNotThrow(() => searchDict2.Remove(80));
            Assert.That(searchDict2[0], Is.EqualTo(existingSearchRange1));
            Assert.That(searchDict2[1], Is.EqualTo(existingSearchRange2));
            Assert.That(searchDict2.Count, Is.EqualTo(2));
        }

        [Test]
        public void CheckIfWeCanClearSearchRangeDictionary()
        {
            //Arrange
            var searchDict = new SearchRangeDictionary { new SearchRange(), new SearchRange() };

            //Act
            searchDict.Clear();

            //Assert
            Assert.That(searchDict, Is.Empty);
        }
    }
}