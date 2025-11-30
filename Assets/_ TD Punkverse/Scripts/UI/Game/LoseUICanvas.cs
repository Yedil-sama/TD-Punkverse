using TD_Punkverse.Core;

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
            _sceneLoaderService.LoadScene("Game Scene");
        }

        public void OnMenuButtonPress()
        {
            _sceneLoaderService.LoadScene("Menu Scene");
        }
    }
}
