using System.Collections.Generic;
using TD_Punkverse.Core;
using UnityEngine;

namespace TD_Punkverse.Game.Enemies
{
	public sealed class EnemyFactoryService : Service
	{
		[SerializeField] private EnemyView _enemyPrefab;
		[SerializeField] private Transform _enemyContainer;

		private readonly List<EnemyView> _activeEnemies = new List<EnemyView>();

		public EnemyView SpawnEnemy(Enemy enemyData, Transform target, Vector3 spawnPosition)
		{
			EnemyView instance = Instantiate(
				_enemyPrefab,
				spawnPosition,
				Quaternion.identity,
				_enemyContainer
			);

			Enemy runtimeEnemy = new Enemy(
				enemyData.MaxHealth,
				enemyData.Damage,
				enemyData.Speed
			);

			instance.Initialize(runtimeEnemy, target);
			_activeEnemies.Add(instance);

			runtimeEnemy.OnDie += enemy =>
			{
				HandleEnemyDeath(instance);
			};

			return instance;
		}

		private void HandleEnemyDeath(EnemyView view)
		{
			if (_activeEnemies.Contains(view))
			{
				_activeEnemies.Remove(view);
			}

			if (view != null && view.gameObject != null)
			{
				Object.Destroy(view.gameObject);
			}
		}

		public IReadOnlyList<EnemyView> GetActiveEnemies()
		{
			return _activeEnemies;
		}
	}
}
