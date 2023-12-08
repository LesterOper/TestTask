using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.LevelDialog
{
    public class LevelPartController : MonoBehaviour
    {
        [SerializeField] private List<Transform> lvlParents;
        [SerializeField]private Level _lvlPrefab;
        private List<Level> _levels = new List<Level>(25);

        public int Setup(int lvl, int reward, Action<int> passedCallback)
        {
            int lvlNum = lvl;
            for (int i = 0; i < lvlParents.Count; i++)
            {
                lvlNum += 1;
                Level level = Instantiate(_lvlPrefab, lvlParents[i]);
                _levels.Add(level);
                level.Setup(lvlNum, reward, passedCallback);
            }

            return lvlNum;
        }

    }
}