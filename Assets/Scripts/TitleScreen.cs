using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private GameObject _titleScreenObject;
    void Start()
    {
        StartCoroutine(TitleScreenTransition());
    }

    private IEnumerator TitleScreenTransition()
    {
        yield return new WaitForSeconds(3);
        StaticTrigger.Instance.TriggerStaticTransition();
        _titleScreenObject.SetActive(false);
    }
}
