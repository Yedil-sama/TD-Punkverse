using UnityEngine;

namespace TD_Punkverse.Core
{
	public sealed class LoseGameState : GameState
	{
		public LoseGameState(GameStateMachine machine) : base(machine)
		{
		}

		public override void Enter()
		{
			Time.timeScale = 0f;
			// TODO: trigger lose UI
		}

		public override void Exit()
		{
			Time.timeScale = 1f;
		}
	}
}
