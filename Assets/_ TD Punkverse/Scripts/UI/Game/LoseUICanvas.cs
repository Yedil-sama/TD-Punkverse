using DG.Tweening;
using TD_Punkverse.Core;
using UnityEngine;

namespace TD_Punkverse.UI.Game
{
	public sealed class LoseUICanvas : UICanvas
	{
		private SceneLoaderService _sceneLoaderService;
		protected override void Initialize()
		{
			base.Initialize();

			_sceneLoaderService = ServiceLocator.Instance.Get<SceneLoaderService>();

			Subscribe();
		}

		private void OnDestroy()
		{
			Unsubscribe();
		}

		private void Subscribe() => GameEventBus.OnLose += Open;
		private void Unsubscribe() => GameEventBus.OnLose -= Open;

		public void OnRetryButtonPress()
		{
			Time.timeScale = 1f;
			DOTween.KillAll();

			ServiceLocator.Instance.Get<SceneLoaderService>().LoadScene("Game Scene");
		}

		public void OnMenuButtonPress()
		{
			_sceneLoaderService.LoadScene("Menu Scene");
		}
	}
}
