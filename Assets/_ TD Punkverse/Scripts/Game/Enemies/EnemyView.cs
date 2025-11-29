using UnityEngine;
using UnityEngine.AI;

namespace TD_Punkverse.Game.Enemies
{
	[RequireComponent(typeof(NavMeshAgent))]
	public class EnemyView : MonoBehaviour
	{
		[Header("Data")]
		[SerializeField] private Enemy _enemy;

		private NavMeshAgent _agent;

		private Transform _target;

		public Enemy Enemy => _enemy;

		public void Initialize(Enemy enemy, Transform target)
		{
			_agent = GetComponent<NavMeshAgent>();

			_enemy = enemy;
			_target = target;

			_agent.speed = enemy.Speed;

			Subscribe();
		}

		private void Subscribe()
		{
			_enemy.OnDie += HandleDie;
		}

		private void Unsubscribe()
		{
			_enemy.OnDie -= HandleDie;
		}

		private void OnDisable() => Unsubscribe();

		private void Update()
		{
			if (_enemy == null || _target == null) return;

			_agent.SetDestination(_target.position);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (_enemy == null) return;

			if (other.TryGetComponent(out TownhallZone townhallZone))
			{
				_enemy.DealDamageToPlayer();
			}
		}

		private void HandleDie(Enemy enemy)
		{
			Destroy(gameObject);
		}
	}
}
