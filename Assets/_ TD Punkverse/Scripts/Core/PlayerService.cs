using UnityEngine;

namespace TD_Punkverse.Core
{
	public sealed class PlayerService : Service
	{
		[SerializeField] private int _health;
		[SerializeField] private int _money;

		private PlayerServiceObserver _observer;

		public int Health => _health;
		public int Money => _money;
		public PlayerServiceObserver Observer => _observer;

		public override void Initialize()
		{
			_observer = new PlayerServiceObserver();
		}

		public void AddMoney(int amount)
		{
			if (amount <= 0) return;

			_money += amount;
			_observer.InvokeMoneyChanged(_money);
		}

		public bool TrySpend(int amount)
		{
			if (amount <= 0) return false;
			if (_money < amount) return false;

			_money -= amount;
			_observer.InvokeMoneyChanged(_money);

			return true;
		}

		public void TakeDamage(int damage)
		{
			if (damage <= 0) return;

			_health -= damage;
			_observer.InvokeHealthChanged(_health);

			if (_health <= 0)
			{
				_health = 0;
				_observer.InvokeHealthChanged(_health);

				Lose();
			}
		}

		public void Lose()
		{
			_observer.InvokeLose();
		}
	}
}
