using System;
using TD_Punkverse.Game.Projectiles;
using UnityEngine;

namespace TD_Punkverse.Game.Towers
{
	[Serializable]
	public sealed class ShootingTower : Tower
	{
		[SerializeField] private ProjectileView _projectilePrefab;
		[SerializeField] private float _projectileSpeed = 10f;
		[SerializeField] private int _damage = 1;
		[SerializeField] private float _attackRange = 7f;
		[SerializeField] private float _turnSpeed = 180f;

		public ProjectileView ProjectilePrefab => _projectilePrefab;
		public float ProjectileSpeed => _projectileSpeed;
		public int Damage => _damage;
		public float AttackRange => _attackRange;
		public float TurnSpeed => _turnSpeed;

		public ShootingTower(string name, int cost, float workSpeed) : base(name, cost, workSpeed) { }
	}
}
