using TD_Punkverse.Core;

namespace TD_Punkverse.UI.Game
{
	public sealed class WinUICanvas : UICanvas
	{
		protected override void Initialize()
		{
			base.Initialize();

			Subscribe();
		}

		private void OnDestroy()
		{
			Unsubscribe();
		}

		private void Subscribe() => GameEventBus.OnWin += Open;
		private void Unsubscribe() => GameEventBus.OnWin -= Open;
	}
}
