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
			EnemyView instance = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity, _enemyContainer);
			instance.Initialize(enemyData, target);

			_activeEnemies.Add(instance);
			enemyData.OnDie += e => _activeEnemies.Remove(instance);

			return instance;
		}

		public IReadOnlyList<EnemyView> GetActiveEnemies() => _activeEnemies;
	}
}
