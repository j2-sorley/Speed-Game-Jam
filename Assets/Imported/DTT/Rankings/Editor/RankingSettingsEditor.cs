using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DTT.PublishingTools;
using UnityEditor;
using DTT.Rankings.Runtime;

namespace DTT.Rankings.Editor
{
    /// <summary>
    /// Places a DTT header in the inspector and keeps the default body.
    /// </summary>
    [DTTHeader("dtt.rankings")]
    [CustomEditor(typeof(RankingSettings))]
    public class RankingSettingsEditor : DTTInspector
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            DrawDefaultInspector();
        }
    }
}