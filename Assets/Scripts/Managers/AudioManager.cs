using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _backgroundAudio;

    [SerializeField]
    private List<AudioClip> _levelSfxs;

    void Start()
    {
        if (!_audioSource.isPlaying)
        {
            EnablebackgroundAudio();
        }
    }

    public void EnablebackgroundAudio()
    {
        if (_backgroundAudio != null)
        {
            _audioSource.Stop();
            _audioSource.clip = _backgroundAudio;
            _audioSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        StartCoroutine(PlaySfxRoutine(name));
    }

    public IEnumerator PlaySfxRoutine(string sfx)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = _levelSfxs.Where(x => x.name == sfx).SingleOrDefault();

        if (audioSource.clip != null)
        {
            audioSource.Play();

            while (audioSource.isPlaying)
            {
                yield return null;
            }
        }
        Destroy(audioSource);
    }

    public void StopSFX(string name)
    {
		AudioSource[] sources = GetComponents<AudioSource>();

		for (int i = 0; i < sources.Length; i++)
		{
            if (sources[i].clip.name == name)
                sources[i].mute = true;
		}
    }

    public bool IsSFXPlaying(string name)
    {
        AudioSource[] sources = GetComponents<AudioSource>();

        for (int i = 0; i < sources.Length; i++)
        {
            if (sources[i].clip.name == name)
                return true;
        }

        return false;
    }
}