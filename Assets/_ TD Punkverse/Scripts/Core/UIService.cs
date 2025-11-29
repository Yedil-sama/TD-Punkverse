using System;
using System.Collections.Generic;
using TD_Punkverse.UI;
using UnityEngine;

namespace TD_Punkverse.Core
{
	public sealed class UIService : Service
	{
		[SerializeField] private List<UICanvas> _uiCanvases;
		private readonly Dictionary<Type, UICanvas> _canvases = new Dictionary<Type, UICanvas>();

		private UICanvas _current;

		public override void Initialize()
		{
			RegisterUICanvases();
		}

		private void RegisterUICanvases()
		{
			foreach (UICanvas canvas in _uiCanvases)
			{
				Type type = canvas.GetType();
				if (_canvases.ContainsKey(type) == false)
				{
					_canvases.Add(type, canvas);
				}
			}
		}

		private void CloseAll()
		{
			foreach (KeyValuePair<Type, UICanvas> kvp in _canvases)
			{
				kvp.Value.Close();
			}
		}

		public void Open<T>() where T : UICanvas
		{
			Type key = typeof(T);

			if (_canvases.TryGetValue(key, out UICanvas canvas))
			{
				if (_current != null)
				{
					_current.Close();
				}

				_current = canvas;
				_current.Open();
			}
			else
			{
				Debug.LogError($"UIService: Canvas of type {key.Name} not registered.");
			}
		}

		public void CloseCurrent()
		{
			if (_current != null)
			{
				_current.Close();
				_current = null;
			}
		}
	}
}
