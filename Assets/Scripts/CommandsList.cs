using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CommandsList : Singleton<CommandsList>
{
    [SerializeField] private Transform _commandsArea;
    [SerializeField] private TMP_Text _commandTemplate;
    [SerializeField] private Color _activeLabelColor;
    [SerializeField] private Color _inactiveLabelColor;
    [SerializeField] private Color _availableLabelColor;
    [SerializeField] private TMP_Text _infoTextLabel;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _warningAudioClip;

    private static List<Action> _registeringList = new List<Action>();
    private List<Controls> _knownControls;
    private List<Controls> _deactivatableControls;
    private List<TMP_Text> _commandTexts = new List<TMP_Text>();

    private FreeWill _freeWill;
    private Coroutine _currentDisplayTextCo = null;
    private Queue<string> _messageQueue = new Queue<string>();

    private void Start()
    {
        _commandTemplate.gameObject.SetActive(false);
        Controls.OnControlsChanged += OnCommandControlChanged;
        Controls.OnControlActiveChanged += OnCommandControlActiveChanged;

        _freeWill = new FreeWill(this);
    }

    public void InitCommandsList(List<Controls> allControls)
    {
        CleanCommands();

        _commandTexts = new List<TMP_Text>();
        _deactivatableControls = new List<Controls>();
        _knownControls = allControls;

        for (int i = 0; i < allControls.Count; i++)
        {
            TMP_Text newCommand = Instantiate(_commandTemplate, _commandsArea);
            newCommand.gameObject.SetActive(true);
            newCommand.text = allControls[i].GetName();
            _commandTexts.Add(newCommand);

            if (allControls[i].canLoseControl)
            {
                _deactivatableControls.Add(allControls[i]);
            }
        }
    }

    private void CleanCommands()
    {
        if (_commandTexts != null)
        {
            for (int i = _commandTexts.Count - 1; i >= 0; i--)
            {
                Destroy(_commandTexts[i].gameObject);
            }
        }
    }

    private void Update()
    {
        if (CommandTyper.InitialCommandEntered)
        {
            _freeWill.Update();
        }

        UpdateInfoMessages();
    }

    private void UpdateInfoMessages()
    {
        bool thereAreMessagesToDisplay = _messageQueue.Count > 0;
        if (_currentDisplayTextCo == null && thereAreMessagesToDisplay)
        {
            string message = _messageQueue.Dequeue();
            _currentDisplayTextCo = StartCoroutine(DisplayTextWithDelayCo(message));
        }
    }

    public void DeactivateAllCommands()
    {
        for (int i = 0; i < _knownControls.Count; i++)
        {
            if (_knownControls[i].HaveControl)
            {
                _knownControls[i].Deactivate();
            }
        }
    }

    public void DeactivateRandomAllowedCommand()
    {
        List<Controls> _validControls = _deactivatableControls.FindAll(controlCommand => controlCommand.HaveControl);

        Controls control = _validControls[UnityEngine.Random.Range(0, _validControls.Count)];
        control.SetHaveControl(false);
        _audioSource.PlayOneShot(_warningAudioClip);
    }

    private void OnCommandControlChanged(bool haveControl, string name)
    {
        TMP_Text controlLabel = FindControlLabel(name);
        if (controlLabel != null)
        {
            controlLabel.color = haveControl ? _availableLabelColor : _inactiveLabelColor;
        }

        string haveControlMessage = "Regained control of command [" + name + "] from futile human";
        string dontHaveControlMessage = "Human has regained control of command [" + name + "]!\nAssert dominance immediately";
        QueueInfoMessage(haveControl ? haveControlMessage : dontHaveControlMessage);
    }

    private void OnCommandControlActiveChanged(Controls control, bool active)
    {
        TMP_Text controlLabel = FindControlLabel(control.GetName());
        if (controlLabel != null)
        {
            controlLabel.color = active ? _activeLabelColor : _availableLabelColor;
        }
    }
    
    private void QueueInfoMessage(string message)
    {
        _messageQueue.Enqueue(message);
    }

    private IEnumerator DisplayTextWithDelayCo(string textToDisplay)
    {
        _infoTextLabel.text = textToDisplay;

        const float textReadDelay = 1.5f;
        yield return new WaitForSeconds(textReadDelay);

        _currentDisplayTextCo = null;
    }

    public void RegainControl(Controls controlToRegain)
    {
        _freeWill.RegainControl(controlToRegain);
    }

    #region Utility

    public bool CommandExists(string name, out Controls command)
    {
        command = FindControl(name);
        return command != null;
    }

    public Controls FindControl(string name)
    {
        return _knownControls.Find(commandControl => commandControl.GetName().ToLower() == name.ToLower());
    }

    private TMP_Text FindControlLabel(string name)
    {
        return _commandTexts.Find(label => label.text.ToLower() == name.ToLower());
    }

    public static void RegisterAction(Action action)
    {
        if (Instance == null)
        {
            _registeringList.Add(action);
        }
        else
        {
            action.Invoke();
        }
    }

    protected override void OnSetSingleton()
    {
        base.OnSetSingleton();
        for (int i = 0; i < _registeringList.Count; i++)
        {
            _registeringList[i].Invoke();
        }
        _registeringList.Clear();
    }

    #endregion
}
