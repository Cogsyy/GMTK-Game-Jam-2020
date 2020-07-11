using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using System.Text.RegularExpressions;

public class CommandTyper : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;

    private string _currentTextValue = "";

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
                Debug.Log("Command: " + GetCommand());
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
            string final = _inputField.text.Substring(secondLastHackN + 1, lastHackN - (secondLastHackN + 1));//+1 removes \n, it counts as 1 aparently, not 2
            return final;
        }
        else
        {
            return _inputField.text.Substring(0, lastHackN);
        }
    }

    public string Between(string STR, string FirstString, string LastString)
    {
        string FinalString;
        int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
        int Pos2 = STR.IndexOf(LastString);
        FinalString = STR.Substring(Pos1, Pos2 - Pos1);
        return FinalString;
    }

    private void Update()
    {
        //lock caret position
        _inputField.caretPosition = _inputField.text.Length;
    }
}
