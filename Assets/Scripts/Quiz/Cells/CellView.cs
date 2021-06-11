using Data;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

namespace Quiz.Cells
{
    public class CellView : MonoBehaviour
    {
        [SerializeField] private Color[] _colors;

        private Image _cellIcon;
        private Image _cellBackground;
        private UIParticleSystem _particle;

        private Vector3 _startIconScale;

        private void Awake()
        {
            _cellBackground = GetComponent<Image>();
            _cellIcon = transform.GetChild(0).GetComponent<Image>();
            _particle = GetComponentInChildren<UIParticleSystem>();

            _cellBackground.SetAlpha(0f);
            _cellIcon.SetAlpha(0f);
            _startIconScale = _cellIcon.transform.localScale;

            _cellBackground.color = _colors[Random.Range(0, _colors.Length)];
            _cellBackground.transform.localScale = Vector3.zero;
        }

        private void BounceCell()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(_cellBackground.DOFade(1f, 0.2f));
            sequence.Join(_cellIcon.DOFade(1f, 0.2f));
            sequence.Append(_cellBackground.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce));
        }

        private void EnableCell()
        {
            _cellBackground.SetAlpha(1f);
            _cellIcon.SetAlpha(1f);
            _cellBackground.transform.localScale = Vector3.one;
        }

        public void WrongAnswerHandler()
        {
            _cellIcon.transform.DOShakePosition(0.5f, 30f)
                .SetEase(Ease.InBounce);
        }

        public void RightAnswerHandler()
        {
            _cellIcon.transform.localScale = Vector3.zero;
            _cellIcon.transform.DOScale(_startIconScale, 0.5f).SetEase(Ease.OutBounce);
            _particle.StartParticleEmission();
        }

        public void UpdateCellView(QuizObjectData objectData, int levelOrder)
        {
            _cellIcon.sprite = objectData.QuizObjectSprite;

            if (levelOrder > 1)
                EnableCell();
            else
                BounceCell();
        }
    }
}