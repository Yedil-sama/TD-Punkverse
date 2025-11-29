using System;
using System.Collections.Generic;
using UnityEngine;

namespace TD_Punkverse.Game.Enemies
{
	[Serializable]
	public sealed class WaveEnemyEntry
	{
		public Enemy EnemyData;
		public int Count = 1;
		public float SpawnDelay = 0.5f;
	}

	[CreateAssetMenu(fileName = "WaveDefinition", menuName = "Scriptable Objects/TD Punkverse/Wave Definition")]
	public sealed class WaveDefinition : ScriptableObject
	{
		public List<WaveEnemyEntry> Enemies;
	}
}
