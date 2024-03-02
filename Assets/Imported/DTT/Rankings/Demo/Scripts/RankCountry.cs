using System.Collections;
using System.Collections.Generic;
using System;
using DTT.Rankings.Runtime;

namespace DTT.Rankings.Demo
{
    /// <summary>
    /// Custom ranking with a country field.
    /// </summary>
    [Serializable]
    public class RankCountry : Rank
    {
        /// <summary>
        /// Player country.
        /// </summary>
        public string _country;

        /// <summary>
        /// Player country.
        /// </summary>
        public string Country => _country;

        /// <summary>
        /// Initializes the rank with the given data.
        /// </summary>
        /// <param name="id">Rank ID.</param>
        /// <param name="name">Player name.</param>
        /// <param name="score">Player score.</param>
        /// <param name="country">Player country.</param>
        public RankCountry(int id, string name, int score, string country) : base(id, name, score) => _country = country;

        /// <summary>
        /// Returns a string with all rank details.
        /// </summary>
        public override string ToString() => $"ID: {Id} Rank: {RankPosition} Name: {Name} Score: {Score} Country: {_country}";
    }
}
