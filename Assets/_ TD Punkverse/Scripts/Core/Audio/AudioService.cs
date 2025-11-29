using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD_Punkverse.Core.Audio
{
	public sealed class AudioService : Service
	{
		[Header("Scene Dependencies")]
		[SerializeField] private bool _startBackgroundMusic = true;
		[SerializeField] private List<AudioMaterial> _audioMaterials;

		[Header("Prefab Dependencies")]
		[SerializeField] private AudioSource _musicSourcePrefab;
		[SerializeField] private AudioSource _sfxSourcePrefab;

		private readonly Dictionary<AudioClip, AudioMaterial> _materialsMap = new Dictionary<AudioClip, AudioMaterial>();
		private AudioSource _musicSource;
		private AudioSource _sfxSource;
		private Transform _root;

		private Coroutine _musicCoroutine;

		public override void Initialize()
		{
			_root = transform;

			_musicSource = Instantiate(_musicSourcePrefab, _root);
			_sfxSource = Instantiate(_sfxSourcePrefab, _root);

			foreach (AudioMaterial material in _audioMaterials)
			{
				if (material.AudioClip != null && !_materialsMap.ContainsKey(material.AudioClip))
				{
					_materialsMap.Add(material.AudioClip, material);
				}
			}

			if (_startBackgroundMusic && _audioMaterials.Count > 0)
			{
				StartPlaylist();
			}
		}

		public void StartPlaylist()
		{
			if (_musicCoroutine != null)
			{
				StopCoroutine(_musicCoroutine);
			}
			_musicCoroutine = StartCoroutine(PlayMusicPlaylist());
		}

		public void StopPlaylist()
		{
			if (_musicCoroutine != null)
			{
				StopCoroutine(_musicCoroutine);
				_musicCoroutine = null;
			}
			StopMusic();
		}

		private IEnumerator PlayMusicPlaylist()
		{
			List<AudioMaterial> musicMaterials = _audioMaterials.FindAll(m => m.AudioClip != null);

			if (musicMaterials.Count == 0)
				yield break;

			int index = 0;
			while (true)
			{
				AudioMaterial current = musicMaterials[index];
				_musicSource.clip = current.AudioClip;
				_musicSource.volume = current.Volume;
				_musicSource.loop = false;
				_musicSource.Play();

				yield return new WaitForSeconds(current.AudioClip.length);

				index = (index + 1) % musicMaterials.Count;
			}
		}

		public void PlaySFX(AudioClip clip)
		{
			if (clip == null || !_materialsMap.ContainsKey(clip))
				return;

			AudioMaterial material = _materialsMap[clip];
			_sfxSource.PlayOneShot(clip, material.Volume);
		}

		public void StopMusic()
		{
			_musicSource.Stop();
			_musicSource.clip = null;
		}

		public void StopAll()
		{
			StopMusic();
			_sfxSource.Stop();
		}
	}
}
