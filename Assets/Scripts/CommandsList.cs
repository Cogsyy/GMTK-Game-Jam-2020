using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CommandsList : MonoBehaviour
{
    [SerializeField] private Transform _commandsArea;
    [SerializeField] private TMP_Text _commandTemplate;

    private List<Controls> _knownControls;

    private void Start()
    {
        _commandTemplate.gameObject.SetActive(false);
    }

    public void InitCommandsList(List<Controls> allControls)
    {
        _knownControls = allControls;

        for (int i = 0; i < allControls.Count; i++)
        {
            TMP_Text newCommand = Instantiate(_commandTemplate, _commandsArea);
            newCommand.gameObject.SetActive(true);
            newCommand.text = allControls[i].GetName();
        }
    }

    public bool CommandExists(string name)
    {
        return _knownControls.Find(command => command.GetName().ToLower() == name.ToLower()) != null;
    }
}
