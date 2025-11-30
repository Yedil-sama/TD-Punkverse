using UnityEngine;

namespace TD_Punkverse.Game.Towers
{
	public class TowerView : MonoBehaviour
	{
		[SerializeField] private Tower _tower; // assign this in the prefab

		public Tower Tower => _tower;

		private bool _isPlaced;
		public bool IsPlaced => _isPlaced;

		public void FinalizePlacement()
		{
			_isPlaced = true;
		}

		public void SetTower(Tower towerData)
		{
			_tower = new Tower(towerData.Name, towerData.Cost, towerData.WorkSpeed);
		}
	}
}
