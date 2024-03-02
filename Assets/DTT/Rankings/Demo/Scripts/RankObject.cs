using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DTT.Rankings.Demo
{
    /// <summary>
    /// UI object for each ranking.
    /// </summary>
    public class RankObject : MonoBehaviour
    {
        /// <summary>
        /// Rank ID.
        /// </summary>
        private int _id;

        /// <summary>
        /// Text for the rank.
        /// </summary>
        [SerializeField]
        [Tooltip("Text for rank position.")]
        private Text _rankText;

        /// <summary>
        /// Text for player name.
        /// </summary>
        [SerializeField]
        [Tooltip("Text for player name.")]
        private Text _nameText;

        /// <summary>
        /// Text for player score.
        /// </summary>
        [SerializeField]
        [Tooltip("Text for player score.")]
        private Text _scoreText;

        /// <summary>
        /// Rank ID.
        /// </summary>
        public int Id => _id;

        /// <summary>
        /// Sets all the data on the rank object.
        /// </summary>
        /// <param name="id">Rank ID.</param>
        /// <param name="name">Player name.</param>
        /// <param name="score">Player score.</param>
        public void SetData(int id, int rank, string name, int score)
        {
            _id = id;
            _rankText.text = $" {rank}";
            _nameText.text = $"Name: {name}";
            _scoreText.text = $"Score: {score}";
        }
    }
}

