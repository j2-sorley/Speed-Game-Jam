#if UNITY_TEST_FRAMEWORK

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System;
using DTT.Rankings.Runtime;
using System.Linq;

namespace DTT.Rankings.Tests.Runtime
{
    /// <summary>
    /// Tests the functionalities of the leaderboard.
    /// </summary>
    public class Test_Leaderboard
    {
        /// <summary>
        /// Tests sorting the rankings by name ascending.
        /// </summary>
        [Test]
        public void Test_SortCollectionAscending_ByName()
        {
            // Arrange.
            Leaderboard<Rank> board = new Leaderboard<Rank>(5, new RankingSaverJSON<Rank>());
            List<Rank> sortedRankings = new List<Rank>();
            List<Rank> toSort = new List<Rank> { new Rank(1, "bbb", 200), new Rank(2, "aaa", 500) };
            List<Rank> expectedResult = toSort.OrderBy(x => x.Name).ToList();

            // Act.
            sortedRankings = board.SortCollectionAscending(x => x.Name, toSort);

            // Assert.
            Assert.AreEqual(expectedResult, sortedRankings);
        }

        /// <summary>
        /// Tests sorting the rankings by score ascending.
        /// </summary>
        [Test]
        public void Test_SortCollectionAscending_ByScore()
        {
            // Arrange.
            Leaderboard<Rank> board = new Leaderboard<Rank>(5, new RankingSaverJSON<Rank>());
            List<Rank> sortedRankings = new List<Rank>();
            List<Rank> toSort = new List<Rank> { new Rank(1, "bbb", 200), new Rank(2, "aaa", 500) };
            List<Rank> expectedResult = toSort.OrderBy(x => x.Score).ToList();

            // Act.
            sortedRankings = board.SortCollectionAscending(x => x.Score, toSort);

            // Assert.
            Assert.AreEqual(expectedResult, sortedRankings);
        }

        /// <summary>
        /// Tests sorting the rankings by rank ascending.
        /// </summary>
        [Test]
        public void Test_SortCollectionAscending_ByRank()
        {
            // Arrange.
            Leaderboard<Rank> board = new Leaderboard<Rank>(5, new RankingSaverJSON<Rank>());
            List<Rank> sortedRankings = new List<Rank>();
            List<Rank> toSort = new List<Rank> { new Rank(1, "bbb", 200), new Rank(2, "aaa", 500) };
            List<Rank> expectedResult = toSort.OrderBy(x => x.RankPosition).ToList();

            // Act.
            sortedRankings = board.SortCollectionAscending(x => x.RankPosition, toSort);

            // Assert.
            Assert.AreEqual(expectedResult, sortedRankings);
        }

        /// <summary>
        /// Tests sorting the rankings by name descending.
        /// </summary>
        [Test]
        public void Test_SortCollectionDescending_ByName()
        {
            // Arrange.
            Leaderboard<Rank> board = new Leaderboard<Rank>(5, new RankingSaverJSON<Rank>());
            List<Rank> sortedRankings = new List<Rank>();
            List<Rank> toSort = new List<Rank> { new Rank(1, "bbb", 200), new Rank(2, "aaa", 500) };
            List<Rank> expectedResult = toSort.OrderByDescending(x => x.Name).ToList();

            // Act.
            sortedRankings = board.SortCollectionDescending(x => x.Name, toSort);

            // Assert.
            Assert.AreEqual(expectedResult, sortedRankings);
        }

        /// <summary>
        /// Tests sorting the rankings by score descending.
        /// </summary>
        [Test]
        public void Test_SortCollectionDescending_ByScore()
        {
            // Arrange.
            Leaderboard<Rank> board = new Leaderboard<Rank>(5, new RankingSaverJSON<Rank>());
            List<Rank> sortedRankings = new List<Rank>();
            List<Rank> toSort = new List<Rank> { new Rank(1, "bbb", 200), new Rank(2, "aaa", 500) };
            List<Rank> expectedResult = toSort.OrderByDescending(x => x.Score).ToList();

            // Act.
            sortedRankings = board.SortCollectionDescending(x => x.Score, toSort);

            // Assert.
            Assert.AreEqual(expectedResult, sortedRankings);
        }

        /// <summary>
        /// Tests sorting the rankings by score descending.
        /// </summary>
        [Test]
        public void Test_SortCollectionDescending_ByRank()
        {
            // Arrange.
            Leaderboard<Rank> board = new Leaderboard<Rank>(5, new RankingSaverJSON<Rank>());
            List<Rank> sortedRankings = new List<Rank>();
            List<Rank> toSort = new List<Rank> { new Rank(1, "bbb", 200), new Rank(2, "aaa", 500) };
            List<Rank> expectedResult = toSort.OrderByDescending(x => x.RankPosition).ToList();

            // Act.
            sortedRankings = board.SortCollectionDescending(x => x.RankPosition, toSort);

            // Assert.
            Assert.AreEqual(expectedResult, sortedRankings);
        }

        /// <summary>
        /// Tests if the correct player is returned by an ID.
        /// </summary>
        [Test]
        public void Test_GetCurrentPlayerRank()
        {
            // Arrange.
            Leaderboard<Rank> board = new Leaderboard<Rank>(5, new RankingSaverJSON<Rank>());
            List<Rank> rankings = new List<Rank> { new Rank(1, "bbb", 200), new Rank(2, "aaa", 500) };
            Rank expectedRank = rankings[1];

            // Act.
            Rank returnedRank = board.GetCurrentPlayerRank(2, rankings);

            // Assert.
            Assert.AreEqual(expectedRank, returnedRank);
        }

        /// <summary>
        /// Tests if the page counter increments correctly.
        /// </summary>
        [Test]
        public void Test_NextPage()
        {
            // Arrange.
            Leaderboard<Rank> board = new Leaderboard<Rank>(5, new RankingSaverJSON<Rank>());

            // Act.
            board.NextPage();

            // Assert.
            Assert.AreEqual(1, board.CurrentPage);
        }

        /// <summary>
        /// Tests if the page counter decrements correctly.
        /// </summary>
        [Test]
        public void Test_PreviousPage()
        {
            // Arrange.
            Leaderboard<Rank> board = new Leaderboard<Rank>(5, new RankingSaverJSON<Rank>());

            // Act.
            board.PreviousPage();

            // Assert.
            Assert.AreEqual(1, board.CurrentPage);
        }
    }
}

#endif