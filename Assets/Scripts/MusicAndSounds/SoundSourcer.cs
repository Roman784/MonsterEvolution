using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundSourcer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private ObjectPool<SoundSourcer> _pool;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        //AudioSource _audioSource = GetComponent<AudioSource>();
    }

    public void Init(AudioClip clip, float volume, ObjectPool<SoundSourcer> pool)
    {
        _pool = pool;

        _audioSource.volume = volume;
        _audioSource.PlayOneShot(clip);

        Invoke("Destroy", clip.length);
    }

    private void Destroy()
    {
        _pool.Release(this);
    }
}
