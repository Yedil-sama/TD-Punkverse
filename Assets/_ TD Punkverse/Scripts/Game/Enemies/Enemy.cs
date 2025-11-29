using System;
using TD_Punkverse.Core;
using UnityEngine;

namespace TD_Punkverse.Game.Enemies
{
	[Serializable]
	public class Enemy
	{
		[SerializeField] private float _speed = 1f;
		[SerializeField] private int _damageToPlayer = 1;
		[SerializeField] private int _health = 5;

		public float Speed => _speed;
		public int Damage => _damageToPlayer;
		public int Health => _health;

		public event Action<Enemy> OnDie;

		public virtual void DealDamageToPlayer()
		{
			PlayerService playerService = ServiceLocator.Instance.Get<PlayerService>();
			playerService.TakeDamage(_damageToPlayer);

			Die();
		}

		public virtual int TakeDamage(int damage)
		{
			if (damage <= 0) return 0;

			int before = _health;

			_health -= damage;
			if (_health < 0) _health = 0;

			int dealtDamage = before - _health;

			if (_health == 0)
			{
				Die();
			}

			return dealtDamage;
		}

		private void Die()
		{
			OnDie?.Invoke(this);
		}
	}
}
