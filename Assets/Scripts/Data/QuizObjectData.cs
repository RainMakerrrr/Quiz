using Quiz.GameState;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Data / Quiz Object Data")]
    public class QuizObjectData : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _quizObjectSprite;

        public string Name => _name;
        public Sprite QuizObjectSprite => _quizObjectSprite;

        public bool IsTarget { get; set; }

        private void OnEnable()
        {
            ResetTargetFlag();
            GameStateHandler.OnLevelsCompleted += ResetTargetFlag;
        }

        private void ResetTargetFlag() => IsTarget = false;

        private void OnDisable() => GameStateHandler.OnLevelsCompleted -= ResetTargetFlag;
    }
}