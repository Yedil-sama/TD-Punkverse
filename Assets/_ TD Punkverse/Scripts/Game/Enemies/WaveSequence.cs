using System.Collections.Generic;
using UnityEngine;

namespace TD_Punkverse.Game.Enemies
{
	[CreateAssetMenu(fileName = "WaveSequence", menuName = "Scriptable Objects/TD Punkverse/Wave Sequence")]
	public sealed class WaveSequence : ScriptableObject
	{
		public List<WaveDefinition> Waves;
	}
}
