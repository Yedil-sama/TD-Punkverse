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
	}
}