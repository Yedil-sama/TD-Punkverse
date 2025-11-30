using TD_Punkverse.Core;
using UnityEngine;
using UnityEngine.UI;

namespace TD_Punkverse.UI
{
    public sealed class SettingsUICanvas : UICanvas
    {
        private SceneLoaderService _sceneLoaderService;

        [Header("Master Volume")]
        [SerializeField] private Image _masterIcon;
        [SerializeField] private Slider _masterSlider;
        [SerializeField] private Sprite _masterOn;
        [SerializeField] private Sprite _masterOff;

        [Header("Music Volume")]
        [SerializeField] private Image _musicIcon;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Sprite _musicOn;
        [SerializeField] private Sprite _musicOff;

        [Header("SFX Volume")]
        [SerializeField] private Image _sfxIcon;
        [SerializeField] private Slider _sfxSlider;
        [SerializeField] private Sprite _sfxOn;
        [SerializeField] private Sprite _sfxOff;

        protected override void Initialize()
        {
            base.Initialize();

            _sceneLoaderService = ServiceLocator.Instance.Get<SceneLoaderService>();
        }

        public void OnRetryButtonPress()
        {
            _sceneLoaderService.LoadScene("Game Scene");
        }

    }
}