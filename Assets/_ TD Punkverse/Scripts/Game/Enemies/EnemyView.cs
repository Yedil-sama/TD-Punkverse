using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace TD_Punkverse.Game.Enemies
{
	[RequireComponent(typeof(NavMeshAgent))]
	public sealed class EnemyView : MonoBehaviour
	{
		[SerializeField] private Enemy _enemy;

		private NavMeshAgent _agent;
		private Transform _target;

		public Enemy Enemy => _enemy;
		public Vector3 Position => transform.position;

		public void Initialize(Enemy enemy, Transform target)
		{
			_enemy = enemy;
			_target = target;

			_enemy.Initialize();

			_agent = GetComponent<NavMeshAgent>();
			_agent.speed = _enemy.Speed;
			_agent.updateRotation = true;
			_agent.updatePosition = true;

			if (_agent.isActiveAndEnabled && _agent.isOnNavMesh)
			{
				_agent.SetDestination(_target.position);
			}
			else
			{
				StartCoroutine(SetDestinationNextFrame());
			}
		}

		private IEnumerator SetDestinationNextFrame()
		{
			yield return new WaitUntil(() => _agent.isActiveAndEnabled && _agent.isOnNavMesh);

			_agent.SetDestination(_target.position);
		}


		private void Update()
		{
			if (_target == null)
				return;

			if (_agent.destination != _target.position)
				_agent.SetDestination(_target.position);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent(out TownhallZone zone))
			{
				_enemy.DealDamageToPlayer();
			}
		}
	}
}
