using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Quiz.GameState
{
    public class GameStateView : MonoBehaviour
    {
        private Image _loadScreen;
        private Button _restartButton;

        private void OnEnable()
        {
            _loadScreen = GetComponent<Image>();
            _restartButton = GetComponentInChildren<Button>(true);

            GameStateHandler.OnLevelsCompleted += EnableRestartButton;

            _restartButton.onClick.AddListener(RestartGame);
        }

        private void EnableRestartButton() => _restartButton.gameObject.SetActive(true);

        private void RestartGame()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(_loadScreen.DOFade(1f, 0.7f));
            sequence.Append(_loadScreen.DOFade(0.2f, 0.7f));
            sequence.OnComplete(() => SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex));
        }

        private void OnDisable()
        {
            GameStateHandler.OnLevelsCompleted -= EnableRestartButton;
            _restartButton.onClick.RemoveListener(RestartGame);
        }
    }
}