using UnityEngine;
using UnityEngine.AI;

namespace TD_Punkverse.Game.Enemies
{
	public class EnemyView : MonoBehaviour
	{
		[Header("Enemy")]
		[SerializeField] private Enemy _enemy;

		[Header("Unity Dependencies")]
		[SerializeField] private Collider _collider;
		[SerializeField] private NavMeshAgent _agent;

		public Enemy Enemy => _enemy;
	}
}