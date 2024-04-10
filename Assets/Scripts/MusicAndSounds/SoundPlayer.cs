using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public static SoundPlayer Instance {  get; private set; }

    [SerializeField] private SoundSourcer _sourcerPrefab;

    [SerializeField] private AudioClip _buttonClickSound;
    [SerializeField] private AudioClip _openMenuSound;
    [SerializeField] private AudioClip _upgradeSound;
    [SerializeField] private AudioClip _purchaseSound;
    [SerializeField] private AudioClip _openBoxSound;
    [SerializeField] private AudioClip _mergeSound;

    private static ObjectPool<SoundSourcer> _sourcersPool;

    private void Awake()
    {
        Instance = Singleton.Get<SoundPlayer>();

        if (_sourcersPool == null )
            _sourcersPool = new ObjectPool<SoundSourcer>(_sourcerPrefab, 8);
    }

    public void PlayButtoClickSound()
    {
        Play(_buttonClickSound); ;
    }

    public void PlayOpenMenuSound()
    {
        Play(_openMenuSound); ;
    }

    public void PlayUpgradeSound()
    {
        Play(_upgradeSound);
    }

    public void PlayPurchaseSound()
    {
        Play(_purchaseSound);
    }

    public void PlayOpenBoxSound()
    {
        Play(_openBoxSound);
    }

    public void PlayMergeSound()
    {
        Play(_mergeSound);
    }

    public void Play(AudioClip clip)
    {
        SoundSourcer sourcer = _sourcersPool.Get();
        sourcer.Init(clip, Sound.Instance.Volume, _sourcersPool);
    }
}
