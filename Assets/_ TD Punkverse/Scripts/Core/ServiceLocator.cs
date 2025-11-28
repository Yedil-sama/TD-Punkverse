using System;
using System.Collections.Generic;
using UnityEngine;

namespace TD_Punkverse.Core
{
	public sealed class ServiceLocator : MonoBehaviour
	{
		public static ServiceLocator Instance { get; private set; }

		private readonly Dictionary<Type, IService> _services = new();

		public void Initialize()
		{
			if (Instance != null && Instance != this)
			{
				Destroy(gameObject);
				return;
			}

			Instance = this;
		}

		public void Register(IService service)
		{
			Type type = service.GetType();

			if (_services.ContainsKey(type))
			{
				Debug.LogWarning($"Service {type} is already registered.");
				return;
			}

			service.Initialize();
			_services[type] = service;
		}

		public T Get<T>() where T : IService
		{
			Type type = typeof(T);

			if (_services.TryGetValue(type, out IService service))
			{
				return (T)service;
			}

			throw new Exception($"Service {type} not registered!");
		}
	}
}
