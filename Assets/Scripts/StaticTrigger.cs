using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTrigger : Singleton<StaticTrigger>
{
    [SerializeField] private GameObject _staticObject;
    [SerializeField] private AudioSource _audioSource;

    void Start()
    {
        _staticObject.SetActive(false);
    }

    public void TriggerStaticTransition()
    {
        StartCoroutine(TriggerTransition());
    }

    private IEnumerator TriggerTransition()
    {
        _staticObject.SetActive(true);
        _audioSource.volume = 1;
        yield return new WaitForSeconds(0.3f);
        _staticObject.SetActive(false);
        _audioSource.volume = 0;
    }
    
    public void TriggerStaticGameClose()
    {
        _staticObject.SetActive(true);
        _audioSource.volume = 1;
    }
}
