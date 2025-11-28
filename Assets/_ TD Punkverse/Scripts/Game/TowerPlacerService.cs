using System.Collections.Generic;
using TD_Punkverse.Core;
using TD_Punkverse.Game.Grid;
using TD_Punkverse.Game.Towers;
using UnityEngine;

namespace TD_Punkverse.Game
{
	public sealed class TowerPlacerService : Service
	{
		[SerializeField] private Camera _worldCamera;
		[SerializeField] private LayerMask _gridLayerMask;
		[SerializeField] private Transform _dragRoot;
		[SerializeField] private Material _invalidPlacementMaterial;
		[SerializeField] private Material _validPlacementMaterial;

		private TowerView _draggedInstance;
		private TowerView _ghostInstance;
		private GridService _grid;
		private readonly List<Renderer> _ghostRenderers = new List<Renderer>();

		public override void Initialize()
		{
			if (_worldCamera == null)
			{
				_worldCamera = Camera.main;
			}

			_grid = ServiceLocator.Instance.Get<GridService>();
		}

		public void StartDrag(TowerView prefab)
		{
			_draggedInstance = prefab;
			_ghostInstance = Instantiate(prefab, _dragRoot);
			PrepareGhost(_ghostInstance);
			UpdateDrag();
		}

		public void UpdateDrag()
		{
			if (_ghostInstance == null)
			{
				return;
			}

			Ray ray = _worldCamera.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, _gridLayerMask))
			{
				GridCellView cell = hitInfo.collider.GetComponent<GridCellView>();
				if (cell != null)
				{
					_ghostInstance.transform.position = cell.transform.position + new Vector3(0, 2.35f, 0);
					if (cell.IsEmpty)
					{
						SetGhostMaterial(true);
					}
					else
					{
						SetGhostMaterial(false);
					}

					return;
				}

				_ghostInstance.transform.position = hitInfo.point;
				SetGhostMaterial(false);
				return;
			}

			SetGhostMaterial(false);
		}

		public void EndDrag()
		{
			if (_ghostInstance == null)
			{
				return;
			}

			Ray ray = _worldCamera.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, _gridLayerMask))
			{
				GridCellView cell = hitInfo.collider.GetComponent<GridCellView>();
				if (cell != null && cell.IsEmpty)
				{
					TowerView placed = Instantiate(_draggedInstance);
					_grid.PlaceTower(placed, cell.GridX, cell.GridY);
					Destroy(_ghostInstance.gameObject);
					_ghostInstance = null;
					_draggedInstance = null;
					return;
				}
			}

			Destroy(_ghostInstance.gameObject);
			_ghostInstance = null;
			_draggedInstance = null;
		}

		private void PrepareGhost(TowerView ghost)
		{
			Renderer[] renderers = ghost.GetComponentsInChildren<Renderer>(true);
			_ghostRenderers.Clear();
			foreach (Renderer r in renderers)
			{
				_ghostRenderers.Add(r);
			}

			ghost.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
			SetGhostMaterial(false);
		}

		private void SetGhostMaterial(bool valid)
		{
			Material material = valid ? _validPlacementMaterial : _invalidPlacementMaterial;
			foreach (Renderer renderer in _ghostRenderers)
			{
				renderer.sharedMaterial = material;
			}
		}
	}
}
