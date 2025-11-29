using System;
using TD_Punkverse.Core;
using UnityEngine;

namespace TD_Punkverse.Game.Enemies
{
	[Serializable]
	public sealed class Enemy
	{
		[SerializeField] private int _maxHealth = 5;
		[SerializeField] private int _damageToPlayer = 1;
		[SerializeField] private float _speed = 2f;

		private int _currentHealth;

		public int MaxHealth => _maxHealth;
		public int CurrentHealth => _currentHealth;
		public float Speed => _speed;
		public int Damage => _damageToPlayer;

		public event Action<Enemy> OnDie;

		public Enemy(int maxHealth, int damageToPlayer, float speed)
		{
			_maxHealth = maxHealth;
			_damageToPlayer = damageToPlayer;
			_speed = speed;

			_currentHealth = _maxHealth;
		}

		public void Initialize()
		{
			_currentHealth = _maxHealth;
		}

		public void DealDamageToPlayer()
		{
			PlayerService playerService = ServiceLocator.Instance.Get<PlayerService>();
			playerService.TakeDamage(_damageToPlayer);
			Die();
		}

		public int TakeDamage(int damage)
		{
			if (damage <= 0) return 0;

			int before = _currentHealth;
			_currentHealth -= damage;
			if (_currentHealth < 0) _currentHealth = 0;

			if (_currentHealth == 0)
				Die();

			return before - _currentHealth;
		}

		private void Die()
		{
			OnDie?.Invoke(this);
		}
	}
}
