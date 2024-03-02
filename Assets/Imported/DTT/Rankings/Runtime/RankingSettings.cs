using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DTT.Rankings.Runtime
{
    /// <summary>
    /// Scriptable object for leaderboard configuration.
    /// </summary>
    [CreateAssetMenu(menuName = "DTT/Rankings/RankingSettings", fileName = "New Ranking Settings")]
    public class RankingSettings : ScriptableObject
    {
        /// <summary>
        /// The amount of rows in the leaderboard.
        /// </summary>
        [SerializeField]
        [Tooltip("The max amount of rows in the leaderboard.")]
        private int _maxRows = DEFAULT_AMOUNT_ROWS;

        /// <summary>
        /// Default amount of rows in the leaderboard.
        /// </summary>
        public const int DEFAULT_AMOUNT_ROWS = 5;

        /// <summary>
        /// One or multiple pages
        /// </summary>
        [SerializeField]
        [Tooltip("Show one or multiple pages")]
        private bool _usePagination = false;

        /// <summary>
        /// Should the current player be shown separately.
        /// </summary>
        [SerializeField]
        [Tooltip("Should the current player be shown separately.")]
        private bool _highlightCurrentPlayer = false;

        /// <summary>
        /// The amount of rows in the leaderboard.
        /// </summary>
        public int MaxRows => _maxRows;

        /// <summary>
        /// One or multiple pages.
        /// </summary>
        public bool UsePagination => _usePagination;
        
        /// <summary>
        /// Should the current player be shown separately.
        /// </summary>
        public bool HighlightCurrentPlayer => _highlightCurrentPlayer;
    }
}