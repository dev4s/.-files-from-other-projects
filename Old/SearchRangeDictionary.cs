using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Dev4s.WebClient
{
    public class SearchRangeDictionary : IEnumerable
    {
        private SortedDictionary<int, SearchRange> _searchRanges = new SortedDictionary<int, SearchRange>();

        public SearchRange this[int key]
        {
            get { return _searchRanges[key]; }
        }

        public int Count
        {
            get { return _searchRanges.Count; }
        }

        public void Add(SearchRange item)
        {
            _searchRanges.Add(_searchRanges.Count, item);
        }

        public void Add(IEnumerable<SearchRange> items)
        {
            foreach (var t in items)
            {
                Add(t);
            }
        }

        public void Clear()
        {
            _searchRanges.Clear();
        }

        public void Remove(int key)
        {
            if (_searchRanges.Remove(key))
            {
                ReorderDictionary();
            }
        }

        public void Remove(SearchRange item)
        {
            var tempItem = _searchRanges.SingleOrDefault(x => x.Value.Equals(item));
            if (tempItem.Value == null) return;

            if (_searchRanges.Remove(tempItem.Key))
            {
                ReorderDictionary();
            }
        }

        public IEnumerator GetEnumerator()
        {
            return _searchRanges.GetEnumerator();
        }

        private void ReorderDictionary()
        {
            var tempSearchRanges = new SortedDictionary<int, SearchRange>();
            for (var i = 0; i < _searchRanges.Max(x => x.Key) + 1; i++)
            {
                try
                {
                    tempSearchRanges.Add(tempSearchRanges.Count, _searchRanges[i]);
                }
                catch (KeyNotFoundException) { }
            }

            _searchRanges = new SortedDictionary<int, SearchRange>(tempSearchRanges);
        }
    }
}