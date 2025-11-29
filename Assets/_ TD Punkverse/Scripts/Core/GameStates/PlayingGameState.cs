using UnityEngine;

namespace TD_Punkverse.Core.GameStates
{
	public sealed class PlayingGameState : GameState
	{
		public PlayingGameState(GameStateMachine machine) : base(machine)
		{
		}

		public override void Enter()
		{
			Time.timeScale = 1f;
		}

		public override void Exit()
		{
		}
	}
}
