using System.Collections.Generic;
using UnityEngine;

namespace TD_Punkverse.Core.Audio
{
	public sealed class AudioService : Service
	{
		[SerializeField] private List<AudioMaterial> _audioMaterials;

		public override void Initialize()
		{

		}
	}
}