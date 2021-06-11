using Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Quiz.GameState
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private QuizDatabase[] _quizDatabases;
        [SerializeField] private int _levelOrder;

        public QuizDatabase CurrentQuizDatabase { get; private set; }

        public int LevelOrder => _levelOrder;

        private void Awake() => CurrentQuizDatabase = _quizDatabases[Random.Range(0, _quizDatabases.Length)];
    }
}