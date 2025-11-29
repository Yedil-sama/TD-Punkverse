using TD_Punkverse.Core;

namespace TD_Punkverse.UI.Game
{
	public sealed class LoseUICanvas : UICanvas
	{
		protected override void Initialize()
		{
			Subscribe();
		}

		private void OnDestroy()
		{
			Unsubscribe();
		}

		private void Subscribe() => GameEventBus.OnLose += Open;
		private void Unsubscribe() => GameEventBus.OnLose -= Open;
	}
}
