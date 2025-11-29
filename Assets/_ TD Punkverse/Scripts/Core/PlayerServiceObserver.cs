using System;

namespace TD_Punkverse.Core
{
	public sealed class PlayerServiceObserver
	{
		public event Action<int> OnHealthChange;
		public event Action<int> OnMoneyChange;
		public event Action OnLose;

		public void NotifyHealthChange(int value) => OnHealthChange?.Invoke(value);

		public void NotifyMoneyChange(int value) => OnMoneyChange?.Invoke(value);

		public void NotifyLose() => OnLose?.Invoke();
	}
}
