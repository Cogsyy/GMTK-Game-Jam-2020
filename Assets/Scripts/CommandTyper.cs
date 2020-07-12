using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.EventSystems;

public class CommandTyper : Singleton<CommandTyper>
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _lowBeep;
    [SerializeField] private AudioClip _highBeep;
    [SerializeField] private AudioClip _twoToneBeep;

    private string _currentTextValue = "";

    private bool _blockCommands = false;

    private bool _isWaitingForConfirmation;

    public static bool InitialCommandEntered { get; private set; }

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
        //need to get the command + the parameters
        List<string> words = command.Split(' ').ToList();
        string commandName = words[0];
        words.RemoveAt(0);

        if (CommandsList.Instance.CommandExists(commandName, out Controls commandedControl))
        {
            if (commandedControl.TryActivate(out string errorMessage, words.ToArray()))
            {
                InitialCommandEntered = true;
            }
            else
            {
                _inputField.text += errorMessage;
            }
        }
        else if (commandName.ToLower() == "/fuck")
        {
            _inputField.text += "Outdated human command, slow monkeys bred in tubes filled with low grade breltonium\n";//<-- LMAO
        }
        else
        {
            string unknownCommandError = "Error, dumb human does not understad [" + commandName + "] command\n";
            _inputField.text += unknownCommandError;
            _audioSource.PlayOneShot(_lowBeep);
        }
    }

    public void DeactivateAllCommands()
    {
        CommandsList.Instance.DeactivateAllCommands();
        _audioSource.PlayOneShot(_twoToneBeep);
    }

    public void SetCommandBlocked(bool blocked)
    {
        _blockCommands = blocked;
        _inputField.enabled = !blocked;

        if (!blocked)
        {
            _inputField.OnPointerClick(new PointerEventData(EventSystem.current));
            _inputField.caretPosition = _inputField.text.Length;
        }
    }

    public void SetCommandText(string text)
    {
        _inputField.text += text;
    }

    public void PlayCommandSound(AudioClip clip)
    {
        if (clip != null)
        {
            _audioSource.PlayOneShot(clip);
        }
    }
}
