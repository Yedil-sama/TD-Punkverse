using System;
using TD_Punkverse.Core;
using UnityEngine;

namespace TD_Punkverse.Game.Enemies
{
	[Serializable]
	public class Enemy
	{
		[SerializeField] private int _townhallDamage = 1;

		public virtual void DealDamageToPlayer()
		{
			PlayerService playerService = ServiceLocator.Instance.Get<PlayerService>();

			playerService.TakeDamage(_townhallDamage);
		}
	}
}
