using System.Collections.Generic;
using TD_Punkverse.Core;
using UnityEngine;

namespace TD_Punkverse.UI.Game
{
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

				if (!isActive)
				{
					_healthViews[i].Pop();
				}
				else
				{
					_healthViews[i].SetActive(true);
				}
			}

			_currentHealth = clampedValue;
		}
	}
}
