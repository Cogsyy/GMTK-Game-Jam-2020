using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : Singleton<AudioController>
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _taskCompleteClip;

    public void PlayTaskComplete()
    {
        _audioSource.PlayOneShot(_taskCompleteClip);
    }
}
