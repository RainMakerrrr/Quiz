using System;
using Data;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Quiz.Cells
{
    public class Cell : MonoBehaviour, IPointerClickHandler
    {
        public static event Action OnLevelPassed;

        private QuizObjectData _objectData;
        private TargetQuizObject _targetQuizObject;
        private CellView _cellView;
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _cellView = GetComponent<CellView>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _targetQuizObject = FindObjectOfType<TargetQuizObject>();
        }

        public void SetObjectData(QuizObjectData objectData, int levelOrder)
        {
            _objectData = objectData;
            _cellView.UpdateCellView(_objectData, levelOrder);
        }

        public void DisableCell() => _canvasGroup.blocksRaycasts = false;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_targetQuizObject.CheckQuizObject(_objectData))
            {
                _cellView.RightAnswerHandler();
                OnLevelPassed?.Invoke();
            }
            else
            {
                _cellView.WrongAnswerHandler();
            }
        }
    }
}