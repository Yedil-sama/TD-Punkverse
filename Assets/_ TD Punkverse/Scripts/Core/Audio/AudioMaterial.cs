using System;
using UnityEngine;

namespace TD_Punkverse.Core.Audio
{
	[Serializable]
	public class AudioMaterial
	{
		[Range(0, 1f)] public float Volume;
		public AudioClip AudioClip;
	}
}
