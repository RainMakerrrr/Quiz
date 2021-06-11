using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Quiz.Cells;
using UnityEngine;

namespace Quiz.GameState
{
    public class GameStateHandler : MonoBehaviour
    {
        public static event Action OnLevelsCompleted;

        [SerializeField] private List<Level> _levels = new List<Level>();
        private Level _currentLevel;

        private void OnEnable()
        {
            _levels = Utilities.QuickSortAscending(_levels).ToList();

            _currentLevel = _levels.FirstOrDefault();

            if (_currentLevel != null)
                _currentLevel.gameObject.SetActive(true);

            foreach (var level in _levels.Except(new[] {_currentLevel}))
            {
                level.gameObject.SetActive(false);
            }

            Cell.OnLevelPassed += LoadNextLevel;
        }

        private void LoadNextLevel() => StartCoroutine(LoadNextLevelWithDelay());

        private IEnumerator LoadNextLevelWithDelay()
        {
            yield return new WaitForSeconds(0.6f);

            var index = _levels.IndexOf(_currentLevel);
            _currentLevel.gameObject.SetActive(false);

            if (index >= _levels.Count - 1)
            {
                OnLevelsCompleted?.Invoke();
                yield break;
            }

            _currentLevel = _levels[index + 1];
            _currentLevel.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            Cell.OnLevelPassed -= LoadNextLevel;
        }
    }
}