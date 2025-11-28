using Core;
using System.Collections.Generic;
using UnityEngine;

namespace TD_Punkverse.Core
{
	[DefaultExecutionOrder(-999)]
	public sealed class EntryPoint : MonoBehaviour
	{
		[SerializeField] private ServiceAllocator _serviceAllocator;
		[SerializeField] private List<Installer> _installers;
		[SerializeField] private List<Service> _services;

		private void Awake()
		{
			InitializeServiceAllocator();
			InitializeServices();
			InitializeInstallers();
		}

		private void InitializeServiceAllocator()
		{
			_serviceAllocator.Initialize();
		}

		private void InitializeInstallers()
		{
			foreach (Installer installer in _installers)
			{
				installer.Install();
			}
		}

		private void InitializeServices()
		{
			foreach (Service service in _services)
			{
				service.Initialize();
			}
		}
	}
}
