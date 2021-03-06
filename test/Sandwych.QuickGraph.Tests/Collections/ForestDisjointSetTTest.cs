// <copyright file="ForestDisjointSetTTest.cs" company="Jonathan de Halleux">Copyright http://quickgraph.codeplex.com/</copyright>
using System;
using System.Linq;
using QuickGraph.Collections;
using System.Collections.Generic;
using Xunit;

namespace QuickGraph.Collections
{
    /// <summary>This class contains parameterized unit tests for ForestDisjointSet`1</summary>
    public class ForestDisjointSetTTest
    {
        public void Unions(int elementCount, KeyValuePair<int, int>[] unions)
        {
            Assert.True(0 < elementCount);
            //FIXME PexSymbolicValue.Minimize(elementCount);
            Assert.True(unions.All(
                u => 0 <= u.Key && u.Key < elementCount && 0 <= u.Value && u.Value < elementCount
            ));

            var target = new ForestDisjointSet<int>();
            // fill up with 0..elementCount - 1
            for (int i = 0; i < elementCount; i++)
            {
                target.MakeSet(i);
                Assert.True(target.Contains(i));
                Assert.Equal(i + 1, target.ElementCount);
                Assert.Equal(i + 1, target.SetCount);
            }

            // apply Union for pairs unions[i], unions[i+1]
            for (int i = 0; i < unions.Length; i++)
            {
                var left = unions[i].Key;
                var right = unions[i].Value;

                var setCount = target.SetCount;
                bool unioned = target.Union(left, right);
                // should be in the same set now
                Assert.True(target.AreInSameSet(left, right));
                // if unioned, the count decreased by 1
                Assert.Equal(unioned, setCount - 1 == target.SetCount);
            }
        }
    }
}
