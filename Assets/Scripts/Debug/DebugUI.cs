using System;
using System.Transactions;
using RushingMachine.Common;
using UnityEngine;

namespace RushingMachine.Debug
{
    public class DebugUI : SingletonMonoBehaviour<DebugUI>
    {
        public int EnemiesCount { get; set; }
        public int PoolCount { get; set; }
        private GUIStyle _style = new GUIStyle();

        private void Start()
        {
            _style.fontSize = Mathf.RoundToInt(Screen.width * 0.04f);
            _style.normal.textColor = Color.yellow;
        }

        //For Debug
        void OnGUI()
        {
            //GUI.Label(new Rect(10, Screen.height - 40, 100, 20), "enemies count: " + EnemiesCount, _style);
            GUI.Label(new Rect(10, (float) (Screen.height - _style.fontSize * 1.5), 100, 20), "enemies count: " + EnemiesCount, _style);
        }
    }
}