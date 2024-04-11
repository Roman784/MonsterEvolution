using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance { get; private set; }

    [SerializeField] private float _volume;
    [SerializeField] private List<AudioClip> _clips = new List<AudioClip>();

    private AudioSource _audioSource;

    private void Awake()
    {
        Instance = Singleton.Get<MusicPlayer>();

        _audioSource = GetComponent<AudioSource>();
    }

    public void Init(float volume)
    {
        Sound.Instance.OnVolumeChanged.AddListener(ChangeVolume);
        _audioSource.volume = _volume * volume;

        Play();
    }

    private void Play()
    {
        AudioClip clip = _clips[Random.Range(0, _clips.Count)];

        _audioSource.clip = clip;
        _audioSource.Play();

        _audioSource.playOnAwake = true;

        Invoke("Play", clip.length);
    }

    private void ChangeVolume()
    {
        _audioSource.volume = _volume * Sound.Instance.Volume;
    }

    public void ContinuePlay()
    {
        /*if (_audioSource != null && _audioSource.clip != null && !_audioSource.isPlaying) 
            _audioSource.Play();*/
    }
}
