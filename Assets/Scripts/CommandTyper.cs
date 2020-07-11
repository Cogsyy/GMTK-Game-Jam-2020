﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEditor.PackageManager;

public class CommandTyper : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private CommandsList _commandsList;

    private string _currentTextValue = "";

    private bool _blockCommands = false;

    public void Event_OnValueChanged()//fired from the command's tmp input field
    {
        if (IsIllegalEdit())
        {
            _inputField.text = _currentTextValue;
            _inputField.caretPosition = _inputField.caretPosition + 1;
        }
        else
        {
            if (EnteredCommand())
            {
                ProcessCommand(GetCommand());
            }

            _currentTextValue = _inputField.text;
        }
    }

    private bool EnteredCommand()
    {
        return _inputField.text.LastIndexOf("\n") == _inputField.caretPosition;
    }

    private bool IsIllegalEdit()
    {
        return _inputField.text.Length < _currentTextValue.Length && PreviousIsNewLine(_currentTextValue);
    }

    private bool PreviousIsNewLine(string value)
    {
        return value.Length >= 2 ? value.LastIndexOf("\n") == _inputField.caretPosition : false;
    }

    private string GetCommand()
    {
        int hackNCount = Regex.Matches(_inputField.text, "\n").Count;
        bool multipleCommandsExist = hackNCount >= 2;

        int lastHackN = _inputField.text.LastIndexOf("\n");

        if (multipleCommandsExist)
        {
            string substringWithoutLastHackN = _inputField.text.Substring(0, lastHackN);
            int secondLastHackN = substringWithoutLastHackN.LastIndexOf("\n");
            return _inputField.text.Substring(secondLastHackN + 1, lastHackN - (secondLastHackN + 1));//+1 removes \n, it counts as 1 aparently, not 2
        }
        else
        {
            return _inputField.text.Substring(0, lastHackN);
        }
    }

    private void Update()
    {
        //lock caret position
        _inputField.caretPosition = _inputField.text.Length;
    }

    private void ProcessCommand(string command)
    {
        if (_blockCommands)
            return;

        if (_commandsList.CommandExists(command, out Controls commandedControl))
        {
            _commandsList.DeactivateAllCommands();
            commandedControl.TryActivate();
        }
        else if (command.ToLower() == "/stop")
        {
            _commandsList.DeactivateAllCommands();
        }
        else if (command.ToLower() == "/reboot")//very temp
        {
            RebootCommand();
        }
        else
        {
            string unknownCommandError = "Error, unknown command " + command + "\n";
            _inputField.text += unknownCommandError;
        }
    }

    private void RebootCommand()
    {
        StartCoroutine(RebootCo());
    }

    private IEnumerator RebootCo()
    {
        _blockCommands = true;

        string rebootingText = "Started reboot routines...";
        _inputField.text += rebootingText;

        float t = 0;
        float iterations = 4;

        for (int i = 0; i < iterations; i++)
        {    
            yield return new WaitForSeconds(1);
            _inputField.text += "\n" + ((i / iterations) * 100).ToString() + "% complete";
        }

        _inputField.text += "\nReboot successful\n";

        _blockCommands = false;
    }
}