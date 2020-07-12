using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingBar : MonoBehaviour
{
    [SerializeField] private Image _fillImage;
    [SerializeField] private TMP_Text _loadingText;
    [SerializeField] private string[] _humanInsults;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _progressClip;
    [SerializeField] private AudioClip _completeClip;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public Coroutine StartLoading(float duration)
    {
        _loadingText.text = _humanInsults[Random.Range(0, _humanInsults.Length)] + " completing task";
        gameObject.SetActive(true);
        CommandTyper.Instance.SetCommandBlocked(true);
        return StartCoroutine(LoadCo(duration));
    }

    private IEnumerator LoadCo(float duration)
    {
        _audioSource.clip = _progressClip;
        _audioSource.loop = true;
        _audioSource.Play();

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / duration;
            _fillImage.fillAmount = t;
            yield return null;
        }

        _audioSource.loop = false;
        _audioSource.Stop();
        _audioSource.PlayOneShot(_completeClip);
        _loadingText.text = _humanInsults[Random.Range(0, _humanInsults.Length)] + " completed task";
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
        CommandTyper.Instance.SetCommandBlocked(false);
    }
}
