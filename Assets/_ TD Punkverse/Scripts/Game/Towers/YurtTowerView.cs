using System.Collections.Generic;
using TD_Punkverse.Core;
using UnityEngine;

namespace TD_Punkverse.Game.Towers
{
	public sealed class YurtTowerView : TowerView
	{
		[SerializeField] private YurtTower _tower;
		[SerializeField] private GameObject _rangeIndicator;
		[SerializeField] private SphereCollider _rangeTrigger;

		private PlayerService _player;
		private readonly HashSet<ShootingTowerView> _buffedTowers = new HashSet<ShootingTowerView>();

		private void Awake()
		{
			_rangeTrigger.radius = _tower.Range;
			UpdateRangeIndicatorScale();
		}

		private void Start()
		{
			_player = ServiceLocator.Instance.Get<PlayerService>();
		}

		public void SetPlacementPreview(bool active)
		{
			if (_rangeIndicator != null)
				_rangeIndicator.SetActive(active);

			UpdateRangeIndicatorScale();
		}

		private void UpdateRangeIndicatorScale()
		{
			if (_rangeIndicator == null)
				return;

			float diameter = _tower.Range * 2f;
			Vector3 scale = _rangeIndicator.transform.localScale;
			_rangeIndicator.transform.localScale = new Vector3(diameter, diameter, scale.z);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (!IsPlaced)
				return;

			ShootingTowerView shootingTower = other.GetComponent<ShootingTowerView>();
			if (shootingTower == null || _buffedTowers.Contains(shootingTower))
				return;

			_buffedTowers.Add(shootingTower);
			shootingTower.ApplyWorkSpeedBuff(_tower.WorkSpeedBuff);
		}

		private void OnTriggerExit(Collider other)
		{
			if (!IsPlaced)
				return;

			ShootingTowerView shootingTower = other.GetComponent<ShootingTowerView>();
			if (shootingTower == null || !_buffedTowers.Contains(shootingTower))
				return;

			_buffedTowers.Remove(shootingTower);
			shootingTower.RemoveWorkSpeedBuff(_tower.WorkSpeedBuff);
		}

		public new void FinalizePlacement()
		{
			base.FinalizePlacement();
		}
	}
}
