using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CommandTyper : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;

    private string _currentTextValue = "";

    public void Event_OnValueChanged()//fired from the command's tmp input field
    {
        if (_inputField.text.Length < _currentTextValue.Length && PreviousIsNewLine(_currentTextValue))
        {
            //illegal backspace
            Debug.Log("Illegal backspace");
            _inputField.text = _currentTextValue;
            _inputField.caretPosition = _inputField.caretPosition + 1;
        }
        else
        {
            _currentTextValue = _inputField.text;
        }
    }

    private bool PreviousIsNewLine(string value)
    {
        return value.Length >= 2 ? value.LastIndexOf("\n") == _inputField.caretPosition : false;
    }

    public void Event_OnSelect()
    {
        
    }

    private void Update()
    {
        //lock caret position
        _inputField.caretPosition = _inputField.text.Length;
    }
}
