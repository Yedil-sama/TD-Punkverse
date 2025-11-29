using TD_Punkverse.Game.Enemies;
using UnityEngine;

namespace TD_Punkverse.Game
{
	[RequireComponent(typeof(Collider))]
	public class TownhallZone : MonoBehaviour
	{
		private Collider _collider;

		private void Awake()
		{
			_collider = GetComponent<Collider>();
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent(out EnemyView enemyView))
			{
				enemyView.Enemy.DealDamageToPlayer();
			}
		}
	}
}