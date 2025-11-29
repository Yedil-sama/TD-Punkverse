using TD_Punkverse.Core;
using TD_Punkverse.Game.Enemies;

namespace TD_Punkverse.UI.Game
{
	public sealed class WinUICanvas : UICanvas
	{
		private void Start()
		{
			EnemyWaveService waveService = ServiceLocator.Instance.Get<EnemyWaveService>();
			waveService.OnWin += Open;
		}
	}
}
