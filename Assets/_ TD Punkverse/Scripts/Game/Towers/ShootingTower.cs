using System;
using TD_Punkverse.Game.Projectiles;
using UnityEngine;

namespace TD_Punkverse.Game.Towers
{
	[Serializable]
	public class ShootingTower : Tower
	{
		[SerializeField] private ProjectileView projectilePrefab;

		public ShootingTower(string name, int cost, float workSpeed) : base(name, cost, workSpeed)
		{
		}


	}
}