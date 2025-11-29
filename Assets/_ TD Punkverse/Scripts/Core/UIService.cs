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
			RegisterCanvases();
		}

		private void RegisterCanvases()
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

		public T Get<T>() where T : UICanvas
		{
			Type key = typeof(T);

			if (_canvases.TryGetValue(key, out UICanvas canvas))
			{
				return (T)canvas;
			}

			Debug.LogError($"UIService: Canvas of type {key.Name} not registered.");
			return null;
		}

		public void Open<T>() where T : UICanvas
		{
			T canvas = Get<T>();
			if (canvas == null)
			{
				return;
			}

			if (_current != null)
			{
				_current.Close();
			}

			_current = canvas;
			_current.Open();
		}

		public void CloseCurrent()
		{
			if (_current == null)
			{
				return;
			}

			_current.Close();
			_current = null;
		}
	}
}
