using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyboardAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _audioClips;
    [SerializeField] private TMP_InputField _inputField;


    // Update is called once per frame
    void Update()
    {
        if (_inputField.isFocused)
        {
            if (Input.anyKeyDown)
            {
                _audioSource.PlayOneShot(_audioClips[Random.Range(0, _audioClips.Length)]);
            }
        }
    }
}
