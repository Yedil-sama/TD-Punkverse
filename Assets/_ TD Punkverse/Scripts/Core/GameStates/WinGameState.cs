using UnityEngine;

namespace TD_Punkverse.Core
{
	public sealed class WinGameState : GameState
	{
		public WinGameState(GameStateMachine machine) : base(machine)
		{
		}

		public override void Enter()
		{
			Time.timeScale = 0f;
			// TODO: trigger win UI
		}

		public override void Exit()
		{
			Time.timeScale = 1f;
		}
	}
}
