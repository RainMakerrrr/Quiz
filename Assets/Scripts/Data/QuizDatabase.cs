using System;
using System.Collections.Generic;
using System.Linq;
using Quiz.Cells;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Data
{
    [CreateAssetMenu(menuName = "Data / Quiz Database")]
    public class QuizDatabase : ScriptableObject
    {
        [SerializeField] private List<QuizObjectData> _quizObjectsPool = new List<QuizObjectData>();

        private readonly List<QuizObjectData> _quizObjectsData = new List<QuizObjectData>();
        private readonly List<QuizObjectData> _usedObjectsData = new List<QuizObjectData>();

        private void OnEnable()
        {
            _quizObjectsData.Clear();
            _quizObjectsData.AddRange(_quizObjectsPool);
            _usedObjectsData.Clear();

            Cell.OnLevelPassed += AddQuizObjectsData;
        }

        private void AddQuizObjectsData()
        {
            _quizObjectsData.AddRange(_usedObjectsData);
            _usedObjectsData.Clear();
        }

        public QuizObjectData GetObjectData()
        {
            var objectData = _quizObjectsData[Random.Range(0, _quizObjectsData.Count)];
            RemoveQuizObject(objectData);

            return objectData;
        }

        public QuizObjectData GetTargetObjectData()
        {
            var unusedObjectsData = _usedObjectsData.Where(data => !data.IsTarget).ToList();
            var objectData = unusedObjectsData[Random.Range(0, unusedObjectsData.Count)];
            objectData.IsTarget = true;

            return objectData;
        }

        private void RemoveQuizObject(QuizObjectData quizObjectData)
        {
            _quizObjectsData.Remove(quizObjectData);
            _usedObjectsData.Add(quizObjectData);
        }

        private void OnDisable() => Cell.OnLevelPassed -= AddQuizObjectsData;
    }
}