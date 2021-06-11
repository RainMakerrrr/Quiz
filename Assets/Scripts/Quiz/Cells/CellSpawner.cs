using System;
using Data;
using Quiz.GameState;
using UnityEngine;

namespace Quiz.Cells
{
    public class CellSpawner : MonoBehaviour
    {
        public static event Action<QuizDatabase> OnCellsInitialized;

        [SerializeField] private Cell[] _cells;

        private QuizDatabase _quizDatabase;
        private Level _level;

        private void Start()
        {
            _level = GetComponentInParent<Level>();
            _cells = GetComponentsInChildren<Cell>();

            InitializeCells();

            Cell.OnLevelPassed += DisableAllCells;
        }

        private void DisableAllCells()
        {
            foreach (var cell in _cells)
            {
                cell.DisableCell();
            }
        }

        private void InitializeCells()
        {
            _quizDatabase = _level.CurrentQuizDatabase;

            foreach (var cell in _cells)
            {
                var randomObjectData = _quizDatabase.GetObjectData();
                cell.SetObjectData(randomObjectData, _level.LevelOrder);
            }

            OnCellsInitialized?.Invoke(_quizDatabase);
        }

        private void OnDisable()
        {
            Cell.OnLevelPassed -= DisableAllCells;
        }
    }
}