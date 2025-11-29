using DG.Tweening;
using System.Collections.Generic;
using TD_Punkverse.Core;
using UnityEngine;
using UnityEngine.UI;

namespace TD_Punkverse.UI.Game
{
	public class PlayerHealthView : MonoBehaviour
	{
		[SerializeField] private Image _healthIcon;
		[SerializeField] private float _popScale = 1.5f;
		[SerializeField] private float _popDuration = 0.25f;

		private Vector3 _originalScale;

		private void Awake()
		{
			_originalScale = _healthIcon.transform.localScale;
		}

		public void Pop()
		{
			PlayPopAnimation();
		}

		private void PlayPopAnimation()
		{
			_healthIcon.transform
				.DOScale(_popScale, _popDuration / 2f)
				.SetEase(Ease.OutBack)
				.OnComplete(() =>
				{
					_healthIcon.transform
						.DOScale(_originalScale, _popDuration / 2f)
						.SetEase(Ease.InBack);
				});
		}

		public void SetActive(bool active)
		{
			_healthIcon.enabled = active;
		}
	}

	public sealed class PlayerHealthService : Service
	{
		[SerializeField] private List<PlayerHealthView> _healthViews;
		private PlayerServiceObserver _observer;
		private int _currentHealth;

		public override void Initialize()
		{
			_observer = ServiceLocator.Instance.Get<PlayerService>().Observer;
			Subscribe();
		}

		private void OnDestroy()
		{
			Unsubscribe();
		}

		private void Subscribe() => _observer.OnHealthChange += HandleHealthChange;
		private void Unsubscribe() => _observer.OnHealthChange -= HandleHealthChange;

		private void HandleHealthChange(int value)
		{
			if (_healthViews == null || _healthViews.Count == 0)
				return;

			int clampedValue = Mathf.Clamp(value, 0, _healthViews.Count);

			for (int i = 0; i < _healthViews.Count; i++)
			{
				bool isActive = i < clampedValue;
				_healthViews[i].SetActive(isActive);

				if (isActive && i >= _currentHealth)
				{
					_healthViews[i].Pop();
				}
			}

			_currentHealth = clampedValue;
		}
	}
}
