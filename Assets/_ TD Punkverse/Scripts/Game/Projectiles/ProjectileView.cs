using System.Collections;
using TD_Punkverse.Game.Enemies;
using UnityEngine;

namespace TD_Punkverse.Game.Projectiles
{
	[RequireComponent(typeof(Collider))]
	public sealed class ProjectileView : MonoBehaviour
	{
		private TargetProjectile _projectile;
		private EnemyView _enemyView;

		private bool _hit;
		private Vector3 _lastTargetPosition;

		public void Initialize(TargetProjectile projectile, EnemyView enemyView)
		{
			_projectile = projectile;
			_enemyView = enemyView;

			if (_enemyView != null)
				_lastTargetPosition = _enemyView.Position;

			StartCoroutine(MoveRoutine());
		}

		private IEnumerator MoveRoutine()
		{
			while (!_hit)
			{
				if (_enemyView != null && _enemyView.Enemy != null && _enemyView.Enemy.CurrentHealth > 0)
				{
					_lastTargetPosition = _enemyView.Position;
				}

				transform.position = Vector3.MoveTowards(transform.position, _lastTargetPosition, _projectile.Speed * Time.deltaTime);

				if (Vector3.Distance(transform.position, _lastTargetPosition) < 0.01f)
					break;

				yield return null;
			}

			if (!_hit)
				Destroy(gameObject);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (_hit)
				return;

			if (other.TryGetComponent(out EnemyView enemy))
			{
				if (enemy == _enemyView)
				{
					_hit = true;
					enemy.Enemy.TakeDamage(_projectile.Damage);
					Destroy(gameObject);
				}
			}
		}
	}
}
