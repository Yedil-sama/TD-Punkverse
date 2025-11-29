using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace TD_Punkverse.UI.Game
{
	public class PlayerHealthView : MonoBehaviour
	{
		[SerializeField] private Image _healthIcon;
		[SerializeField] private float _popDuration = 0.5f;
		[SerializeField] private float _popScale = 1.5f;

		private bool _isPopping = false;

		public void SetActive(bool active)
		{
			gameObject.SetActive(active);
		}

		public void Pop()
		{
			if (_healthIcon == null || _isPopping)
				return;

			_isPopping = true;
			_healthIcon.transform.localScale = Vector3.one;
			_healthIcon.DOKill();
			_healthIcon.transform
				.DOScale(_popScale, _popDuration / 2f)
				.SetEase(Ease.OutBack)
				.OnComplete(() =>
				{
					_healthIcon.transform
						.DOScale(0f, _popDuration / 2f)
						.SetEase(Ease.InBack)
						.OnComplete(() =>
						{
							_isPopping = false;
							SetActive(false);
						});
				});
		}
	}
}
