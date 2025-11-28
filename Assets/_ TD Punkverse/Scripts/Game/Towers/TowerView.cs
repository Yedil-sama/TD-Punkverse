using UnityEngine;

namespace TD_Punkverse.Game.Towers
{
	public class TowerView : MonoBehaviour
	{
		[SerializeField] private Tower _tower;
		public Tower Tower => _tower;
	}
}