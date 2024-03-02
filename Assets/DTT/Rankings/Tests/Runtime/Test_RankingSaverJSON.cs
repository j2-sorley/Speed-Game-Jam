#if UNITY_TEST_FRAMEWORK

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System;
using DTT.Rankings.Runtime;
using System.IO;

namespace DTT.Rankings.Tests.Runtime
{
    /// <summary>
    /// Tests the loading and saving of ranking data in JSON.
    /// </summary>
    public class Test_RankingSaverJSON
    {
        /// <summary>
        /// Setup a file for load testing.
        /// </summary>
        [OneTimeSetUp]
        public void Setup()
        {
            IRankingSaver<Rank> saver = new RankingSaverJSON<Rank>();
            Rank[] rows = new Rank[] { new Rank(1, "test", 100) };
            string path = Application.persistentDataPath + "/TestData.json";

            saver.Save(path, rows);
        }

        /// <summary>
        /// Tests the saving of rankings.
        /// </summary>
        [Test]
        public void Test_Save()
        {
            // Arrange.
            IRankingSaver<Rank> saver = new RankingSaverJSON<Rank>();
            Rank[] rows = new Rank[] { new Rank(1, "test", 100), new Rank(2, "test2", 200) };
            string path = Application.persistentDataPath + "/SavingTestData.json";

            // Act.
            saver.Save(path, rows);

            // Assert.
            Assert.IsTrue(File.Exists(path));
        }

        /// <summary>
        /// Tests the loading of rankings.
        /// </summary>
        [Test]
        public void Test_Load()
        {
            // Arrange.
            IRankingSaver<Rank> saver = new RankingSaverJSON<Rank>();
            Leaderboard<Rank> board = new Leaderboard<Rank>(5, new RankingSaverJSON<Rank>());

            // Act.
            saver.Load(Application.persistentDataPath + "/TestData.json", board.callback);

            // Assert.
            Assert.AreEqual(1, board.Rankings.Count);
        }

        /// <summary>
        /// Delete the testing files.
        /// </summary>
        [OneTimeTearDown]
        public void Cleanup()
        {
            File.Delete(Application.persistentDataPath + "/TestData.json");
            File.Delete(Application.persistentDataPath + "/SavingTestData.json");
        }
    }
}

#endif