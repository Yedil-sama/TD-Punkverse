using TD_Punkverse.Core;
using TMPro;
using UnityEngine;

namespace TD_Punkverse.UI.Game
{
	public sealed class GameUICanvas : UICanvas
	{

		private UIService _uiService;
		[SerializeField] private TMP_Text _waveTimerText;
		[SerializeField] private TMP_Text _moneyText;

		private PlayerService playerService;

		protected override void Initialize()
		{
			base.Initialize();

			playerService = ServiceLocator.Instance.Get<PlayerService>();
			_uiService = ServiceLocator.Instance.Get<UIService>();

			UpdateMoney(playerService.Money);
			Subscribe();
		}

		private void OnDestroy() => Unsubscribe();

		private void Subscribe()
		{
			playerService.Observer.OnMoneyChange += UpdateMoney;
		}

		private void Unsubscribe()
		{
			playerService.Observer.OnMoneyChange -= UpdateMoney;

		}

		public void UpdateWaveTimer(int time)
		{
			_waveTimerText.text = $"Next Wave: {time}s";
		}

		public void UpdateMoney(int money)
		{
			_moneyText.text = money.ToString();
		}

		public void OnSettingsButtonPress()
		{
			_uiService.Get<SettingsUICanvas>().Open();
		}
	}
}
