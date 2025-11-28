using TD_Punkverse.Core;
using TD_Punkverse.Game.Towers;
using UnityEngine;

namespace TD_Punkverse.Game.Grid
{
	public sealed class GridCellView : MonoBehaviour
	{
		[SerializeField] private int _x;
		[SerializeField] private int _y;

		private bool _isEmpty = true;
		private GridService _grid;

		public int GridX => _x;
		public int GridY => _y;
		public bool IsEmpty => _isEmpty;

		private void Awake()
		{
			_grid = ServiceLocator.Instance.Get<GridService>();
		}

		public void Assign(TowerView tower)
		{
			tower.transform.parent = transform;
			tower.transform.localPosition = new Vector3(0, 0.585f, 0);

			_isEmpty = false;
		}

		public void TryPlace(TowerView tower)
		{
			if (_isEmpty)
			{
				_grid.PlaceTower(tower, _x, _y);
			}
		}
	}
}
