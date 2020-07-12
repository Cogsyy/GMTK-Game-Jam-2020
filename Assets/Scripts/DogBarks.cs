using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBarks : MonoBehaviour
{
    [SerializeField] private List<GameObject> _level1Sequence;
    [SerializeField] private List<GameObject> _level5Sequence;

    [SerializeField] private GameObject _barkbark;
    [SerializeField] private GameObject _bark;
    [SerializeField] private GameObject _removeChip;

    private void DisableAll()
    {
        _barkbark.SetActive(false);
        _bark.SetActive(false);
        _removeChip.SetActive(false);
        gameObject.SetActive(false);
    }

    public void PlayLevel1Sequence()
    {
        DisableAll();
        gameObject.SetActive(true);
        StartCoroutine(SequenceCo(_level1Sequence));
    }

    public void PlayLevel5Sequence()
    {
        DisableAll();
        gameObject.SetActive(true);
        StartCoroutine(SequenceCo(_level5Sequence));
    }

    private IEnumerator SequenceCo(List<GameObject> sequence)
    {
        for (int i = 0; i < sequence.Count; i++)
        {
            sequence[i].SetActive(true);
            yield return new WaitForSeconds(2);
            sequence[i].SetActive(false);
        }

        DisableAll();
    }
}
