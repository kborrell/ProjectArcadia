using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    [SerializeField]
    private bool _enabled;

    [SerializeField]
    private AudioClip _backgroundAudio;
    /*
    [SerializeField]
    private List<AudioClip> _levelSfxs;
    */
    [SerializeField]
    private AudioSource _audioSource;
    /*
    [SerializeField]
    private AudioSource _sfxSource;
    */
    void Start()
    {
        if (_enabled && !_audioSource.isPlaying)
            EnablebackgroundAudio();
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
    /*
    public void PlaySFX(string name)
    {
        if (_sfxSource.isPlaying)
            return;

        AudioClip file = _levelSfxs.Where(x => x.name == name).SingleOrDefault();

        if (file != null)
        {
            _sfxSource.clip = file;
            _sfxSource.Play();
        }
    }
    */
}