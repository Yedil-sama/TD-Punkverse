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

		private void Die()
		{
			OnDie?.Invoke(this);
		}
	}
}
