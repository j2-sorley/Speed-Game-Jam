using System.Collections;
using System.Collections.Generic;
using System;

namespace DTT.Rankings.Runtime
{
    /// <summary>
    /// Interface for saving and loading ranks.
    /// </summary>
    /// <typeparam name="T">Rank or any class that derives from it.</typeparam>
    public interface IRankingSaver<T> where T : Rank
    {
        /// <summary>
        /// Loads the data and calls the callback event.
        /// </summary>
        /// <param name="path">File location.</param>
        /// <param name="callback">Event to pass the data to.</param>
        void Load(string path, Action<T[]> callback);

        /// <summary>
        /// Saves the data.
        /// </summary>
        /// <param name="path">File location.</param>
        /// <param name="rows">Collection with rows.</param>
        void Save(string path, T[] rows);
    }
}
