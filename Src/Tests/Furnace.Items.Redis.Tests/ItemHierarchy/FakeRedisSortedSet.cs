using System.Collections;
using System.Collections.Generic;
using ServiceStack.Redis;

namespace Furnace.Items.Redis.Tests.ItemHierarchy
{
    public class FakeRedisSortedSet : IRedisSortedSet
    {
        public List<string> Set { get; private set; }

        public FakeRedisSortedSet()
        {
            Set = new List<string>();
        }
        public IEnumerator<string> GetEnumerator()
        {
            return Set.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Set.GetEnumerator();
        }

        public void Add(string item)
        {
            Set.Add(item);
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(string item)
        {
            throw new System.NotImplementedException();
        }

        public void CopyTo(string[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(string item)
        {
            throw new System.NotImplementedException();
        }

        public int Count { get; private set; }
        public bool IsReadOnly { get; private set; }
        public string Id { get; private set; }
        public List<string> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public List<string> GetRange(int startingRank, int endingRank)
        {
            throw new System.NotImplementedException();
        }

        public List<string> GetRangeByScore(string fromStringScore, string toStringScore)
        {
            throw new System.NotImplementedException();
        }

        public List<string> GetRangeByScore(string fromStringScore, string toStringScore, int? skip, int? take)
        {
            throw new System.NotImplementedException();
        }

        public List<string> GetRangeByScore(double fromScore, double toScore)
        {
            throw new System.NotImplementedException();
        }

        public List<string> GetRangeByScore(double fromScore, double toScore, int? skip, int? take)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveRange(int fromRank, int toRank)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveRangeByScore(double fromScore, double toScore)
        {
            throw new System.NotImplementedException();
        }

        public void StoreFromIntersect(params IRedisSortedSet[] ofSets)
        {
            throw new System.NotImplementedException();
        }

        public void StoreFromUnion(params IRedisSortedSet[] ofSets)
        {
            throw new System.NotImplementedException();
        }

        public long GetItemIndex(string value)
        {
            throw new System.NotImplementedException();
        }

        public double GetItemScore(string value)
        {
            throw new System.NotImplementedException();
        }

        public void IncrementItemScore(string value, double incrementByScore)
        {
            throw new System.NotImplementedException();
        }

        public string PopItemWithHighestScore()
        {
            throw new System.NotImplementedException();
        }

        public string PopItemWithLowestScore()
        {
            throw new System.NotImplementedException();
        }
    }
}
