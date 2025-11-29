using UnityEngine;

namespace TD_Punkverse.Game.Enemies
{
	public sealed class Portal : MonoBehaviour
	{
		[SerializeField] private float _maxSpawnRadius = 3f;

		public float MaxSpawnRadius => _maxSpawnRadius;

		public Vector3 GetRandomSpawnPoint()
		{
			Vector2 randomCircle = Random.insideUnitCircle * _maxSpawnRadius;

			Vector3 spawnPoint = new Vector3(
				transform.position.x + randomCircle.x,
				transform.position.y,
				transform.position.z + randomCircle.y
			);

			return spawnPoint;
		}
	}
}
