using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DTT.Rankings.Runtime;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

namespace DTT.Rankings.Demo
{
    /// <summary>
    /// Used to control the UI of the leaderboard implementation.
    /// </summary>
    public class LeaderboardUI : MonoBehaviour
    {
        /// <summary>
        /// Ledarboard configuration.
        /// </summary>
        [SerializeField]
        [Tooltip("Ledarboard configuration.")]
        private RankingSettings _settings;

        /// <summary>
        /// Parent object for the rows.
        /// </summary>
        [SerializeField]
        [Tooltip("Parent object of the rows.")]
        private GameObject _rowContainer;

        /// <summary>
        /// Parent object for pagination.
        /// </summary>
        [SerializeField]
        [Tooltip("Parent object for pagination.")]
        private GameObject _paginationObject;

        /// <summary>
        /// Rank prefab.
        /// </summary>
        [SerializeField]
        [Tooltip("Rank prefab.")]
        private RankObject _rankPrefab;

        /// <summary>
        /// Current player rank object.
        /// </summary>
        [SerializeField]
        [Tooltip("Current player rank object.")]
        private RankObject _currentRankObject;

        /// <summary>
        /// Rank prefab.
        /// </summary>
        [SerializeField]
        [Tooltip("Rank prefab.")]
        private Text _currentPageText;

        /// <summary>
        /// Dropdown for selecting ID.
        /// </summary>
        [SerializeField]
        [Tooltip("Dropdown for selecting ID")]
        private Dropdown _dropdownId;

        /// <summary>
        /// InputField for selecting the ammount of random entries.
        /// </summary>
        [SerializeField]
        [Tooltip("InputField for selecting the ammount of random entries.")]
        private InputField _entriesInput;

        /// <summary>
        /// Rows scroll rect components
        /// </summary>
        [SerializeField]
        [Tooltip("Rows scroll rect components.")]
        private ScrollRect _scrollRect;

        /// <summary>
        /// Collection for rank game objects.
        /// </summary>
        private List<RankObject> _rankObjects;

        /// <summary>
        /// All ranks.
        /// </summary>
        private Rank[] _rows;

        /// <summary>
        /// Leaderboard with normal ranks.
        /// </summary>
        private Leaderboard<Rank> _leaderboard;

        /// <summary>
        /// File location for normal ranks.
        /// </summary>
        private string PATH_RANK;

        /// <summary>
        /// Activates the pagination if enabled and fills the rows with default data.
        /// </summary>
        private void Start()
        {
            _paginationObject.SetActive(_settings.UsePagination);

            PATH_RANK = Application.persistentDataPath + "/RowsData.json";

            _rankObjects = new List<RankObject>();
            _leaderboard = new Leaderboard<Rank>(_settings.MaxRows, new RankingSaverJSON<Rank>());

            Rank row1 = new Rank(1, "aaa", 100);
            Rank row2 = new Rank(2, "bbb", 30);
            Rank row3 = new Rank(3, "ccc", 200);
            Rank row4 = new Rank(4, "ddd", 20);
            Rank row5 = new Rank(5, "eee", 250);
            Rank row6 = new Rank(6, "fff", 50);
            Rank row7 = new Rank(7, "ggg", 220);
            Rank row8 = new Rank(8, "hhh", 300);
            Rank row9 = new Rank(9, "iii", 500);
            Rank row10 = new Rank(10, "jjj", 140);
            Rank row11 = new Rank(11, "kkk", 10);
            Rank row12 = new Rank(12, "lll", 17);
            Rank row13 = new Rank(13, "mmm", 15);
            Rank row14 = new Rank(14, "nnn", 13);
            Rank row15 = new Rank(15, "ooo", 19);

            _rows = new Rank[] { row15, row14, row13, row12, row11, row10, row9, row8, row7, row6, row5, row4, row3, row2, row1 };
            _leaderboard.AddRows(PATH_RANK, _rows);
        }

        /// <summary>
        /// Fills the leaderboard with ranks.
        /// </summary>
        public void RefreshTable()
        {
            ClearTable();
            _leaderboard.LoadData(PATH_RANK);

            _currentPageText.text = $"1 / {_leaderboard.TotalPages}";

            foreach (Rank rank in _leaderboard.Rankings)
            {
                RankObject rankObject = Instantiate(_rankPrefab, _rowContainer.transform, false);
                rankObject.SetData(rank.Id, rank.RankPosition, rank.Name, rank.Score);
                _rankObjects.Add(rankObject);
            }

            FillDropdown();
        }

        /// <summary>
        /// Sorts the ranks by name.
        /// </summary>
        public void SortByName()
        {
            ClearTable();
            List<Rank> sortedRankings = _leaderboard.GetCurrentPageRankingsSortedAscending(x => x.Name);

            foreach (Rank rank in sortedRankings)
            {
                RankObject rankObject = Instantiate(_rankPrefab, _rowContainer.transform, false);
                rankObject.SetData(rank.Id, rank.RankPosition, rank.Name, rank.Score);
                _rankObjects.Add(rankObject);
            }
        }

        /// <summary>
        /// Sorts the rank by score.
        /// </summary>
        public void SortByScore()
        {
            ClearTable();
            List<Rank> sortedRankings = _leaderboard.GetCurrentPageRankingsSortedDescending(x => x.Score);

            foreach (Rank rank in sortedRankings)
            {
                RankObject rankObject = Instantiate(_rankPrefab, _rowContainer.transform, false);
                rankObject.SetData(rank.Id, rank.RankPosition, rank.Name, rank.Score);
                _rankObjects.Add(rankObject);
            }
        }

        /// <summary>
        /// Displays the next page.
        /// </summary>
        public void NextPage()
        {
            _leaderboard.NextPage();
            ClearTable();

            foreach (Rank rank in _leaderboard.Rankings)
            {
                RankObject rankObject = Instantiate(_rankPrefab, _rowContainer.transform, false);
                rankObject.SetData(rank.Id, rank.RankPosition, rank.Name, rank.Score);
                _rankObjects.Add(rankObject);
            }

            _scrollRect.verticalNormalizedPosition = 1;
        }

        /// <summary>
        /// Displays the previous page.
        /// </summary>
        public void PreviousPage()
        {
            _leaderboard.PreviousPage();
            ClearTable();

            foreach (Rank rank in _leaderboard.Rankings)
            {
                RankObject rankObject = Instantiate(_rankPrefab, _rowContainer.transform, false);
                rankObject.SetData(rank.Id, rank.RankPosition, rank.Name, rank.Score);
                _rankObjects.Add(rankObject);
            }

            _scrollRect.verticalNormalizedPosition = 1;
        }

        /// <summary>
        /// Sets the current player rank information based on the chosen ID.
        /// </summary>
        public void ChooseID()
        {
            if (_dropdownId.options.Count <= 0)
                return;

            _currentRankObject.gameObject.SetActive(_settings.HighlightCurrentPlayer);
            Rank rank = _leaderboard.GetCurrentPlayerRank(Convert.ToInt32(_dropdownId.options[_dropdownId.value].text));
            _currentRankObject.SetData(rank.Id, rank.RankPosition, rank.Name, rank.Score);
        }

        /// <summary>
        /// Populates the leaderboard with random data.
        /// </summary>
        /// <param name="amount">Amount of ranks.</param>
        public void PopulateBoard()
        {
            if (string.IsNullOrEmpty(_entriesInput.text) || Convert.ToInt32(_entriesInput.text) < 0)
                return;

            List<Rank> newRanks = new List<Rank>();
            int amount = Convert.ToInt32(_entriesInput.text);

            for (int i = 0; i < amount; i++)
            {
                Rank rank = new Rank(i + 1, "user_" + i, Random.Range(0, 999));
                newRanks.Add(rank);
            }

            _leaderboard.AddRows(PATH_RANK, newRanks.ToArray());
            RefreshTable();
        }

        /// <summary>
        /// Destroys all rank game objects and clears the list. 
        /// </summary>
        private void ClearTable()
        {
            if (_leaderboard.TotalPages > 0)
                _currentPageText.text = $"{_leaderboard.CurrentPage} / {_leaderboard.TotalPages}";

            foreach (RankObject rank in _rankObjects)
                Destroy(rank.gameObject);

            _rankObjects.Clear();
        }

        /// <summary>
        /// Fills the dropdown with IDs
        /// </summary>
        private void FillDropdown()
        {
            _dropdownId.options.Clear();
            List<Rank> sortedById = _leaderboard.GetAllRankingsSortedAscending(x => x.Id);

            foreach (Rank rank in sortedById)
                _dropdownId.options.Add(new Dropdown.OptionData() { text = rank.Id.ToString() });

            _dropdownId.RefreshShownValue();
        }
    }
}
