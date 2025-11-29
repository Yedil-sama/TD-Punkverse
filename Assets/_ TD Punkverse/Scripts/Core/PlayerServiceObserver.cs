using System;

namespace TD_Punkverse.Core
{
	public sealed class PlayerServiceObserver
	{
		public event Action<int> OnHealthChanged;
		public event Action<int> OnMoneyChanged;
		public event Action OnLose;

		public void InvokeHealthChanged(int value) => OnHealthChanged?.Invoke(value);

		public void InvokeMoneyChanged(int value) => OnMoneyChanged?.Invoke(value);

		public void InvokeLose() => OnLose?.Invoke();
	}
}
