using System;

namespace TD_Punkverse.Core
{
	public static class GameEventBus
	{
		public static event Action OnLose;
		public static event Action OnWin;

		public static void RaiseLose() => OnLose?.Invoke();
		public static void RaiseWin() => OnWin?.Invoke();
	}
}
