using System.Collections.Generic;
using TD_Punkverse.Core;
using TD_Punkverse.Game.Grid;
using TD_Punkverse.Game.Towers;
using UnityEngine;

namespace TD_Punkverse.Game
{
	public sealed class TowerPlacerService : Service
	{
		[SerializeField] private LayerMask _gridLayerMask;
		[SerializeField] private Transform _dragRoot;
		[SerializeField] private Material _invalidPlacementMaterial;
		[SerializeField] private Material _validPlacementMaterial;

		private TowerView _draggedInstance;
		private TowerView _ghostInstance;
		private GridService _grid;
		private Camera _worldCamera;
		private PlayerService _player;
		private readonly List<Renderer> _ghostRenderers = new List<Renderer>();

		public override void Initialize()
		{
			_worldCamera = Camera.main;
			_grid = ServiceLocator.Instance.Get<GridService>();
			_player = ServiceLocator.Instance.Get<PlayerService>();
		}

		public void StartDrag(TowerView prefab)
		{
			if (!CanAfford(prefab)) return;

			_draggedInstance = prefab;

			_ghostInstance = Object.Instantiate(prefab, _dragRoot);
			_ghostInstance.SetTower(prefab.Tower); // ensure the ghost has the tower data

			SetGhostPreview(_ghostInstance, true);
			PrepareGhost(_ghostInstance);
			UpdateDrag();
		}

		public void UpdateDrag()
		{
			if (_ghostInstance == null) return;

			Ray ray = _worldCamera.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, _gridLayerMask))
			{
				GridCellView cell = hitInfo.collider.GetComponent<GridCellView>();
				if (cell != null)
				{
					_ghostInstance.transform.position = cell.transform.position + new Vector3(0, 2.35f, 0);
					SetGhostMaterial(cell.IsEmpty && CanAfford(_draggedInstance));
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
			if (_ghostInstance == null) return;

			Ray ray = _worldCamera.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, _gridLayerMask))
			{
				GridCellView cell = hitInfo.collider.GetComponent<GridCellView>();
				if (cell != null && cell.IsEmpty && CanAfford(_draggedInstance))
				{
					TowerView placed = Object.Instantiate(_draggedInstance);

					// Spend money
					if (_draggedInstance.Tower != null)
						_player.TrySpend(_draggedInstance.Tower.Cost);

					_grid.PlaceTower(placed, cell.GridX, cell.GridY);
					placed.FinalizePlacement();
					SetGhostPreview(placed, false);

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

		private bool CanAfford(TowerView towerView)
		{
			return towerView != null && towerView.Tower != null && _player.Money >= towerView.Tower.Cost;
		}

		private void PrepareGhost(TowerView ghost)
		{
			_ghostRenderers.Clear();
			foreach (Renderer r in ghost.GetComponentsInChildren<Renderer>(true))
			{
				if (r.gameObject.layer == LayerMask.NameToLayer("Attack Range")) continue;
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

		private void SetGhostPreview(TowerView ghost, bool active)
		{
			switch (ghost)
			{
				case ShootingTowerView shootingTower:
					shootingTower.SetPlacementPreview(active);
					break;
				case YurtTowerView yurtTower:
					yurtTower.SetPlacementPreview(active);
					break;
			}
		}
	}
}
