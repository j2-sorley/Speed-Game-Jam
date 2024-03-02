using System.Collections;
using System.Collections.Generic;
using System;

namespace DTT.Rankings.Runtime
{
    /// <summary>
    /// Rank entity. Custom rankings must derive from this class.
    /// </summary>
    [Serializable]
    public class Rank
    {
        /// <summary>
        /// Rank ID.
        /// </summary>
        public int _id;

        /// <summary>
        /// Player rank.
        /// </summary>
        public int _rankPosition;

        /// <summary>
        /// Player name.
        /// </summary>
        public string _name;

        /// <summary>
        /// Final score.
        /// </summary>
        public int _score;

        /// <summary>
        /// Rank ID.
        /// </summary>
        public int Id => _id;

        /// <summary>
        /// Player rank.
        /// </summary>
        public int RankPosition => _rankPosition;

        /// <summary>
        /// Player name.
        /// </summary>
        public string Name => _name;

        /// <summary>
        /// Final score.
        /// </summary>
        public int Score => _score;

        /// <summary>
        /// Initializes the rank with the given data.
        /// </summary>
        /// <param name="id">Rank ID.</param>
        /// <param name="name">Player name.</param>
        /// <param name="score">Player score.</param>
        public Rank(int id, string name, int score)
        {
            _id = id;
            _name = name;
            _score = score;
        }

        /// <summary>
        /// Returns a string with all rank details.
        /// </summary>
        public override string ToString() => $"ID: {_id} Rank: {_rankPosition} Name: {_name} Score: {_score}";
    }
}
