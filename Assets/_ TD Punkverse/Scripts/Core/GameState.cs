namespace TD_Punkverse.Core
{
	public abstract class GameState
	{
		protected readonly GameStateMachine Machine;

		protected GameState(GameStateMachine machine)
		{
			Machine = machine;
		}

		public abstract void Enter();
		public abstract void Exit();
	}
}
