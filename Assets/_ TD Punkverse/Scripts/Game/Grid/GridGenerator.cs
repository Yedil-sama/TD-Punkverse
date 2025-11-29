using TD_Punkverse.Core;
using UnityEngine;

namespace TD_Punkverse.Game.Grid
{
	public sealed class GridGenerator : MonoBehaviour
	{
		[SerializeField] private int _columns;
		[SerializeField] private int _rows;
		[SerializeField] private float _cellSize;
		[SerializeField] private GridCellView _cellPrefab;
		[SerializeField] private Transform _container;

		private void Start() => Generate();

		public void Generate()
		{
			Clear();

			GridCellView[,] cells = new GridCellView[_columns, _rows];

			for (int x = 0; x < _columns; x++)
			{
				for (int y = 0; y < _rows; y++)
				{
					GridCellView cell = Instantiate(_cellPrefab, _container);
					cell.Initialize(x, y);

					cell.transform.localPosition = new Vector3(
						x * _cellSize,
						0f,
						y * _cellSize
					);

					cells[x, y] = cell;
				}
			}

			ServiceLocator.Instance.Get<GridService>().SetCells(cells);
		}

		private void Clear()
		{
			GridCellView[] cells = _container.GetComponentsInChildren<GridCellView>(true);
			foreach (GridCellView cell in cells)
			{
				DestroyImmediate(cell.gameObject);
			}
		}
	}
}
