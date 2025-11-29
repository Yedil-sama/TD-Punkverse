using System.Collections;
using System.Collections.Generic;
using TD_Punkverse.Game.Enemies;
using TD_Punkverse.Game.Projectiles;
using UnityEngine;

namespace TD_Punkverse.Game.Towers
{
	public class ShootingTowerView : TowerView
	{
		[SerializeField] private ShootingTower _tower;
		[SerializeField] private Transform _firePoint;
		[SerializeField] private SphereCollider _rangeTrigger;
		[SerializeField] private GameObject _rangeIndicator;

		private readonly List<EnemyView> _enemies = new List<EnemyView>();
		private Coroutine _shootRoutine;

		private void Awake()
		{
			_rangeTrigger.radius = _tower.AttackRange;
		}

		public void SetPlacementPreview(bool active)
		{
			if (_rangeIndicator != null)
				_rangeIndicator.SetActive(active);

			UpdateRangeIndicatorScale();
		}

		private void UpdateRangeIndicatorScale()
		{
			if (_rangeIndicator != null)
			{
				float diameter = _tower.AttackRange * 2f;
				Vector3 scale = _rangeIndicator.transform.localScale;
				_rangeIndicator.transform.localScale = new Vector3(diameter, diameter, scale.z);
			}
		}

		public new void FinalizePlacement()
		{
			base.FinalizePlacement();
			SetPlacementPreview(false);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (!IsPlaced)
				return;

			if (other.TryGetComponent(out EnemyView enemy))
			{
				_enemies.Add(enemy);

				if (_shootRoutine == null)
					_shootRoutine = StartCoroutine(ShootRoutine());
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (!IsPlaced)
				return;

			if (other.TryGetComponent(out EnemyView enemy))
				_enemies.Remove(enemy);

			if (_enemies.Count == 0 && _shootRoutine != null)
			{
				StopCoroutine(_shootRoutine);
				_shootRoutine = null;
			}
		}

		private IEnumerator ShootRoutine()
		{
			while (_enemies.Count > 0)
			{
				_enemies.RemoveAll(e => e == null || e.Enemy.CurrentHealth <= 0);

				if (_enemies.Count == 0)
					break;

				EnemyView target = _enemies[0];

				yield return RotateAndWait(target);

				FireProjectile(target);

				yield return new WaitForSeconds(_tower.WorkSpeed);
			}

			_shootRoutine = null;
		}

		private IEnumerator RotateAndWait(EnemyView target)
		{
			if (target == null)
				yield break;

			while (true)
			{
				Vector3 direction = target.Position - _firePoint.position;
				direction.y = 0f;

				if (direction.sqrMagnitude < 0.001f)
					yield break;

				Quaternion targetRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0f, 90f, 0f);
				transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _tower.TurnSpeed * Time.deltaTime);

				if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
					break;

				yield return null;
			}
		}

		private void FireProjectile(EnemyView enemyView)
		{
			TargetProjectile projectile = new TargetProjectile(
				_tower.Damage,
				_tower.ProjectileSpeed,
				enemyView.Enemy
			);

			ProjectileView instance = Instantiate(
				_tower.ProjectilePrefab,
				_firePoint.position,
				Quaternion.identity
			);

			instance.Initialize(projectile, enemyView);
		}
	}
}
