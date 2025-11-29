using UnityEngine;

namespace TD_Punkverse.Game.Towers
{
	public class TowerView : MonoBehaviour
	{
		private Tower _tower;
		public Tower Tower => _tower;

		private bool _isPlaced;
		public bool IsPlaced => _isPlaced;

		public void FinalizePlacement()
		{
			_isPlaced = true;
		}
	}
}
