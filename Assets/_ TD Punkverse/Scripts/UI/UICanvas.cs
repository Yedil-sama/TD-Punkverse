using UnityEngine;

namespace TD_Punkverse.UI
{
	[RequireComponent(typeof(Canvas))]
	public abstract class UICanvas : MonoBehaviour
	{
		private Canvas _canvas;

		private void Awake()
		{
			_canvas = GetComponent<Canvas>();
		}

		private void Start()
		{
			Initialize();
		}

		protected virtual void Initialize() { }

		public virtual void Open()
		{
			gameObject.SetActive(true);
		}

		public virtual void Close()
		{
			gameObject.SetActive(false);
		}
	}
}
