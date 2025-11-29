using TD_Punkverse.Core;

namespace TD_Punkverse.UI.Game
{
	public sealed class LoseUICanvas : UICanvas
	{
		private void Start()
		{
			PlayerService player = ServiceLocator.Instance.Get<PlayerService>();
			player.Observer.OnLose += Open;
		}
	}
}
