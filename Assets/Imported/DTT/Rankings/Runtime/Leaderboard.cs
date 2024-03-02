using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.ObjectModel;

namespace DTT.Rankings.Runtime
{
    /// <summary>
    /// Controls the leaderboard.
    /// </summary>
    /// <typeparam name="T">Class that derives from Rank.</typeparam>
    public class Leaderboard<T> where T : Rank
    {
        /// <summary>
        /// ID of the current player.
        /// </summary>
        private int _currentPlayerId;

        /// <summary>
        /// Current page number.
        /// </summary>
        private int _currentPage;

        /// <summary>
        /// The total amount of pages.
        /// </summary>
        private int _totalPages;

        /// <summary>
        /// The max amount of rankings in the list.
        /// </summary>
        private int _maxRows;

        /// <summary>
        /// Collection with all rankings on a page.
        /// </summary>
        private readonly List<T> _currentPageRankings;

        /// <summary>
        /// Collection with all rankings.
        /// </summary>
        private readonly List<T> _allRankings;

        /// <summary>
        /// Used to Save and Load data.
        /// </summary>
        private readonly IRankingSaver<T> _rankSaver;

        /// <summary>
        /// Event for loading data.
        /// </summary>
        public Action<T[]> callback;

        /// <summary>
        /// Collection with all rankings, unsorted.
        /// </summary>
        public ReadOnlyCollection<T> Rankings => _currentPageRankings.AsReadOnly();

        /// <summary>
        ///  Collection with all rankings, sorted.
        /// </summary>
        public ReadOnlyCollection<T> AllRankings => _allRankings.AsReadOnly();

        /// <summary>
        /// Current page number.
        /// </summary>
        public int CurrentPage => _currentPage + 1;

        /// <summary>
        /// The total amount of pages.
        /// </summary>
        public int TotalPages => _totalPages;

        /// <summary>
        /// Initializes the leaderboard.
        /// </summary>
        /// <param name="maxRows">The maximum amount of ranks on the board.</param>
        /// <param name="saver">Class for saving and loading ranks.</param>
        public Leaderboard(int maxRows, IRankingSaver<T> saver)
        {
            _maxRows = maxRows;
            _currentPage = 0;

            _currentPageRankings = new List<T>();
            _allRankings = new List<T>();

            _rankSaver = saver;

            callback += OnCallback;
        }

        /// <summary>
        /// Loads the ranking data from a file.
        /// </summary>
        /// <param name="path">The file location.</param>
        public void LoadData(string path) => _rankSaver.Load(path, callback);

        /// <summary>
        /// Adds data to a file.
        /// </summary>
        /// <param name="path">The file location.</param>
        /// <param name="rows">Collection with rows.</param>
        public void AddRows(string path, T[] rows) => _rankSaver.Save(path, rows);

        /// <summary>
        /// Sorts the collection descending by a property.
        /// </summary>
        /// <param name="getProp">Rank property.</param>
        /// <param name="toSort">Collection to be sorted</param>
        /// /// <returns>The sorted collection.</returns>
        public List<T> SortCollectionAscending(Func<T, IComparable> getProp, List<T> toSort)
        {
            List<T> sorted = new List<T>();
            sorted.AddRange(toSort.OrderBy(x => getProp(x)).ToList());

            return sorted;
        }

        /// <summary>
        /// Sorts the current page rankings ascending by a property.
        /// </summary>
        /// <param name="getProp">Rank property.</param>
        /// <returns>The sorted rankings.</returns>
        public List<T> GetCurrentPageRankingsSortedAscending(Func<T, IComparable> getProp)
        {
            List<T> sorted = new List<T>(_currentPageRankings.OrderBy(x => getProp(x)));

            return sorted;
        }

        /// <summary>
        /// Sorts all rankings ascending by a property.
        /// </summary>
        /// <param name="getProp">Rank property.</param>
        /// <returns>The sorted rankings.</returns>
        public List<T> GetAllRankingsSortedAscending(Func<T, IComparable> getProp)
        {
            List<T> sorted = new List<T>(_allRankings.OrderBy(x => getProp(x)));

            return sorted;
        }

        /// <summary>
        /// Sorts the collection descending by a property.
        /// </summary>
        /// <param name="getProp">Rank property.</param>
        /// <param name="toSort">Collection to be sorted</param>
        /// /// <returns>The sorted collection.</returns>
        public List<T> SortCollectionDescending(Func<T, IComparable> getProp, List<T> toSort)
        {
            List<T> sorted = new List<T>();
            sorted.AddRange(toSort.OrderByDescending(x => getProp(x)).ToList());

            return sorted;
        }

        /// <summary>
        /// Sorts the current page rankings descending by a property.
        /// </summary>
        /// <param name="getProp">Rank property.</param>
        /// <returns>The sorted rankings.</returns>
        public List<T> GetCurrentPageRankingsSortedDescending(Func<T, IComparable> getProp)
        {
            List<T> sorted = new List<T>(_currentPageRankings.OrderByDescending(x => getProp(x)));

            return sorted;
        }

        /// <summary>
        /// Sorts all rankings descending by a property.
        /// </summary>
        /// <param name="getProp">Rank property.</param>
        /// <returns>The sorted rankings.</returns>
        public List<T> GetAllRankingsSortedDescending(Func<T, IComparable> getProp)
        {
            List<T> sorted = new List<T>(_allRankings.OrderByDescending(x => getProp(x)));

            return sorted;
        }

        /// <summary>
        /// Returns the ranking of the current player.
        /// </summary>
        /// <param name="id">Player ID.</param>
        /// <returns>Null if not found.</returns>
        public T GetCurrentPlayerRank(int id, List<T> allRankings)
        {
            foreach (T rank in allRankings)
                if (rank._id == id)
                    return rank;

            return null;
        }

        /// <summary>
        /// Returns the ranking of the current player.
        /// </summary>
        /// <param name="id">Player ID.</param>
        /// <returns>Null if not found.</returns>
        public T GetCurrentPlayerRank(int id)
        {
            foreach (T rank in _allRankings)
                if (rank._id == id)
                    return rank;

            return null;
        }

        /// <summary>
        /// Listens to the callback event and fills the list with rankings.
        /// </summary>
        /// <param name="data">The data to receive.</param>
        private void OnCallback(T[] data)
        {
            if (data == null)
                return;

            _currentPage = 0;
            _allRankings.Clear();
            _allRankings.AddRange(data.ToList());

            AssignRankPositions(true, x => x.Score, _allRankings);

            _currentPageRankings.Clear();
            _totalPages = (_allRankings.Count + _maxRows - 1) / _maxRows;

            for (int i = 0; i < _maxRows; i++)
            {
                if (i >= _allRankings.Count)
                    return;

                _currentPageRankings.Add(_allRankings[i]);
            }
        }

        /// <summary>
        /// Increments the page counter.
        /// </summary>
        public void NextPage()
        {
            if (_currentPage + 1 >= _totalPages)
                return;

            _currentPage++;
            Paginate();
        }

        /// <summary>
        /// Decreases the page counter.
        /// </summary>
        public void PreviousPage()
        {
            if (_currentPage - 1 < 0)
                return;

            _currentPage--;
            Paginate();
        }

        /// <summary>
        /// Calculates a starting point and fills the rank list with reviews up to the max rows.
        /// </summary>
        private void Paginate()
        {
            _currentPageRankings.Clear();
            int startingPoint = _currentPage * _maxRows;

            for (int i = startingPoint; i < startingPoint + _maxRows; i++)
            {
                if (i >= _allRankings.Count)
                    break;

                _currentPageRankings.Add(_allRankings[i]);
            }
        }

        /// <summary>
        /// Assigns ranks based on a given property.
        /// </summary>
        /// <param name="descending">How should the list be sorted.</param>
        /// <param name="getProp">The property type.</param>
        /// <param name="toAssign">List to assign ranks to.</param>
        private void AssignRankPositions(bool descending, Func<T, IComparable> getProp, List<T> toAssign)
        {
            List<T> page = new List<T>();

            if (descending)
                page.AddRange(toAssign.OrderByDescending(x => getProp(x)).ToList());
            else
                page.AddRange(toAssign.OrderBy(x => getProp(x)).ToList());

            toAssign.Clear();
            toAssign.AddRange(page);

            for (int i = 0; i < toAssign.Count; i++)
                toAssign[i]._rankPosition = i + 1;
        }
    }
}

