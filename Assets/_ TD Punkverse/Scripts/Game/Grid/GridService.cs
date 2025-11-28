using TD_Punkverse.Core;
using TD_Punkverse.Game.Towers;
using UnityEngine;

namespace TD_Punkverse.Game.Grid
{
	public sealed class GridService : Service
	{
		[SerializeField] private int _columns;
		[SerializeField] private int _rows;
		[SerializeField] private float _cellSize;
		[SerializeField] private GridCellView _cellPrefab;
		[SerializeField] private Transform _cellContainer;
		private GridCellView[,] _cells;

		public override void Initialize()
		{
			_cells = new GridCellView[_columns, _rows];

			foreach (GridCellView cell in _cellContainer.GetComponentsInChildren<GridCellView>())
			{
				int x = cell.GridX;
				int y = cell.GridY;

				if (x >= 0 && x < _columns && y >= 0 && y < _rows)
				{
					_cells[x, y] = cell;
				}
				else
				{
					Debug.LogWarning($"Cell at ({x},{y}) is out of bounds.");
				}
			}
		}

		public bool IsFree(int x, int y)
		{
			return _cells[x, y].IsEmpty;
		}

		public void PlaceTower(TowerView tower, int x, int y)
		{
			GridCellView cell = _cells[x, y];
			cell.Assign(tower);
		}
	}
}
