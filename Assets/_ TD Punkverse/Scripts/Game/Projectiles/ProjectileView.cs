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

		public void Initialize(TargetProjectile projectile, EnemyView enemyView)
		{
			_projectile = projectile;
			_enemyView = enemyView;
			StartCoroutine(MoveRoutine());
		}

		private IEnumerator MoveRoutine()
		{
			while (_enemyView != null && _enemyView.Enemy.CurrentHealth > 0 && !_hit)
			{
				Vector3 targetPos = _enemyView.Position;
				transform.position = Vector3.MoveTowards(transform.position, targetPos, _projectile.Speed * Time.deltaTime);

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
