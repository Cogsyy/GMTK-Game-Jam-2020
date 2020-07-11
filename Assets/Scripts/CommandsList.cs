using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;

public class CommandsList : MonoBehaviour
{
    [SerializeField] private Transform _commandsArea;
    [SerializeField] private TMP_Text _commandTemplate;
    [SerializeField] private Color _activeLabelColor;
    [SerializeField] private Color _inactiveLabelColor;

    private List<Controls> _knownControls;
    private List<TMP_Text> _commandTexts;

    private void Start()
    {
        _commandTemplate.gameObject.SetActive(false);
        Controls.OnControlsChanged += OnCommandControlChanged;
    }

    public void InitCommandsList(List<Controls> allControls)
    {
        _commandTexts = new List<TMP_Text>();
        _knownControls = allControls;

        for (int i = 0; i < allControls.Count; i++)
        {
            TMP_Text newCommand = Instantiate(_commandTemplate, _commandsArea);
            newCommand.gameObject.SetActive(true);
            newCommand.text = allControls[i].GetName();
            _commandTexts.Add(newCommand);
        }
    }

    public bool CommandExists(string name, out Controls command)
    {
        command = FindControl(name);
        return command != null;
    }

    private Controls FindControl(string name)
    {
        return _knownControls.Find(commandControl => commandControl.GetName().ToLower() == name.ToLower());
    }

    private TMP_Text FindControlLabel(string name)
    {
        return _commandTexts.Find(label => label.text.ToLower() == name.ToLower());
    }

    public void DeactivateAllCommands()
    {
        for (int i = 0; i < _knownControls.Count; i++)
        {
            _knownControls[i].Deactivate();
        }
    }

    public void DeactivateRandomAllowedCommand()
    {
        Controls control = _knownControls[UnityEngine.Random.Range(0, _knownControls.Count)];
        control.SetHaveControl(false);
    }

    private void OnCommandControlChanged(bool haveControl, string name)
    {
        TMP_Text controlLabel = FindControlLabel(name);
        controlLabel.color = haveControl ? _activeLabelColor : _inactiveLabelColor;
    }
}
