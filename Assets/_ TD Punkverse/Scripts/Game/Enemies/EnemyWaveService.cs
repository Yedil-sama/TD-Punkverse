using System;
using System.Collections;
using System.Collections.Generic;
using TD_Punkverse.Core;
using TD_Punkverse.UI.Game;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TD_Punkverse.Game.Enemies
{
	public sealed class EnemyWaveService : Service
	{
		[Header("Config")]
		[SerializeField] private WaveSequence _waveSequence;
		[SerializeField] private float _waveInterval = 5f;

		[Header("Portals")]
		[SerializeField] private List<Portal> _portals;

		private EnemyFactoryService _factoryService;
		private PlayerService _playerService;
		private GameUICanvas _gameUICanvas;

		private int _currentWaveIndex;
		private float _timer;

		public event Action OnWin;

		public override void Initialize()
		{
			_factoryService = ServiceLocator.Instance.Get<EnemyFactoryService>();
			_playerService = ServiceLocator.Instance.Get<PlayerService>();
			_gameUICanvas = ServiceLocator.Instance.Get<UIService>().Get<GameUICanvas>();

			_currentWaveIndex = 0;
			StartCoroutine(WaveRoutine());
		}

		private IEnumerator WaveRoutine()
		{
			while (_currentWaveIndex < _waveSequence.Waves.Count)
			{
				SpawnWave(_waveSequence.Waves[_currentWaveIndex]);

				yield return new WaitUntil(() => _factoryService.GetActiveEnemies().Count == 0);

				_timer = _waveInterval;

				while (_timer > 0f)
				{
					_timer -= Time.deltaTime;
					_gameUICanvas.UpdateWaveTimer(Mathf.CeilToInt(_timer));
					yield return null;
				}

				_currentWaveIndex++;
			}

			OnWin?.Invoke();
		}

		private void SpawnWave(WaveDefinition wave) => StartCoroutine(SpawnRoutine(wave));

		private IEnumerator SpawnRoutine(WaveDefinition wave)
		{
			Transform target = _playerService.TownhallZone.transform;

			foreach (WaveEnemyEntry entry in wave.Enemies)
			{
				for (int i = 0; i < entry.Count; i++)
				{
					Portal portal = GetRandomPortal();
					Vector3 spawnPos = portal.GetRandomSpawnPoint();

					_factoryService.SpawnEnemy(entry.EnemyData, target, spawnPos);

					yield return new WaitForSeconds(entry.SpawnDelay);
				}
			}
		}

		private Portal GetRandomPortal()
		{
			if (_portals.Count == 1)
			{
				return _portals[0];
			}

			int index = Random.Range(0, _portals.Count);
			return _portals[index];
		}
	}
}
