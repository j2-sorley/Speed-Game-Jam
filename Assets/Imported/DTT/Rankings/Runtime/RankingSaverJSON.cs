using UnityEngine;
using System;
using System.IO;

namespace DTT.Rankings.Runtime
{
    /// <summary>
    /// Loads and saves rank data in JSON.
    /// </summary>
    /// <typeparam name="T">Class that derives from Rank.</typeparam>
    public class RankingSaverJSON<T> : IRankingSaver<T> where T : Rank
    {
        /// <summary>
        /// Loads the data and calls the callback event.
        /// </summary>
        /// <param name="path">File location.</param>
        /// <param name="callback">Event to pass the data to.</param>
        public void Load(string path, Action<T[]> callback)
        {
            if (!File.Exists(path))
            {
                Debug.LogError("File doesn't exist.");
                return;
            }

                string content = File.ReadAllText(path);
                Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(content);
                T[] rows = wrapper.Items;

                callback?.Invoke(rows);
        }

        /// <summary>
        /// Saves the data.
        /// </summary>
        /// <param name="path">File location.</param>
        /// <param name="rows">Collection with rows.</param>
        public void Save(string path, T[] rows)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = rows;
            string json = JsonUtility.ToJson(wrapper, true);

            File.WriteAllText(path, json);
        }

        /// <summary>
        /// Wrapper allows arrays to be serialized.
        /// </summary>
        /// <typeparam name="TObject">the type of the object to be serialised</typeparam>
        [Serializable]
        private class Wrapper<TObject>
        {
            public TObject[] Items;
        }
    }
}
