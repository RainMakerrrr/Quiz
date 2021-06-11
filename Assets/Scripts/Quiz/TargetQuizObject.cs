using System;
using Data;
using DG.Tweening;
using Quiz.Cells;
using Quiz.GameState;
using TMPro;
using UnityEngine;

namespace Quiz
{
    public class TargetQuizObject : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _targetText;

        private QuizObjectData _targetObject;

        private void OnEnable()
        {
            CellSpawner.OnCellsInitialized += UpdateTargetObject;
            GameStateHandler.OnLevelsCompleted += ClearTargetText;
        }

        private void UpdateTargetObject(QuizDatabase quizDatabase)
        {
            
            _targetObject = quizDatabase.GetTargetObjectData();
            _targetText.DOFade(1f, 1f);
            _targetText.text = $"Find {_targetObject.Name}";
        }

        private void ClearTargetText() => _targetText.text = string.Empty;

        public bool CheckQuizObject(QuizObjectData quizObjectData) => _targetObject == quizObjectData;

        private void OnDisable()
        {
            CellSpawner.OnCellsInitialized -= UpdateTargetObject;
            GameStateHandler.OnLevelsCompleted -= ClearTargetText;
        }
    }
}