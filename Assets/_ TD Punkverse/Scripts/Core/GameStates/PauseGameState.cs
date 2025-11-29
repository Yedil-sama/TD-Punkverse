using UnityEngine;

namespace TD_Punkverse.Core.GameStates
{
	public sealed class PauseGameState : GameState
	{
		public PauseGameState(GameStateMachine machine) : base(machine)
		{
		}

		public override void Enter()
		{
			Time.timeScale = 0f;
		}

		public override void Exit()
		{
			Time.timeScale = 1f;
		}
	}
}
