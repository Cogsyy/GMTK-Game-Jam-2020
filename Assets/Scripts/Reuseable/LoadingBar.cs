using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    [SerializeField] private Image _fillImage;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public Coroutine StartLoading(float duration)
    {
        gameObject.SetActive(true);
        CommandTyper.Instance.SetCommandBlocked(true);
        return StartCoroutine(LoadCo(duration));
    }

    private IEnumerator LoadCo(float duration)
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / duration;
            _fillImage.fillAmount = t;
            yield return null;
        }

        gameObject.SetActive(false);
        CommandTyper.Instance.SetCommandBlocked(false);
    }
}
