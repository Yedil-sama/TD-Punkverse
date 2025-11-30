#if UNITY_EDITOR
using UnityEditor;
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

		public void Generate()
		{
			ClearEditor();

			for (int x = 0; x < _columns; x++)
			{
				for (int y = 0; y < _rows; y++)
				{
					GridCellView cell = (GridCellView)PrefabUtility.InstantiatePrefab(_cellPrefab, _container);
					cell.Initialize(x, y);

					cell.transform.localPosition = new Vector3(
						x * _cellSize,
						0f,
						y * _cellSize
					);
				}
			}
		}

		public void ClearEditor()
		{
			for (int i = _container.childCount - 1; i >= 0; i--)
			{
				DestroyImmediate(_container.GetChild(i).gameObject);
			}
		}
	}
}
#endif