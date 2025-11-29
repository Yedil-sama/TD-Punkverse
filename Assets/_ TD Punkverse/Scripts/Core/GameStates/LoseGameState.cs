using DG.Tweening;
using UnityEngine;

namespace TD_Punkverse.Core
{
	public sealed class LoseGameState : GameState
	{
		private const float PauseDuration = 2f;

		public LoseGameState(GameStateMachine machine) : base(machine)
		{
		}

		public override void Enter()
		{
			DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 0f, PauseDuration)
				   .SetEase(Ease.Linear)
				   .SetUpdate(true)
				   .OnComplete(() =>
				   {
					   GameEventBus.RaiseLose();
				   });
		}

		public override void Exit()
		{
			Time.timeScale = 1f;
		}
	}
}
