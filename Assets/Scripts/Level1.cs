using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;
using TMPro;

public class Level1 : LevelBase
{
    //wake up
    //shower
    //praise overlords
    //eat food
    //drink coffee
    //leave house

    //optional:
    //get dressed
    //pet dog
    //cry

    [SerializeField] private ControllableEntity _player;
    [SerializeField] private TMP_Text _taskText;
    private int _currentObjectiveNameIndex = 0;

    [Header("Objectives")]
    [SerializeField] private string[] _objectiveTexts;
    [SerializeField] private WakeUpControl _wakeUpControl;
    [SerializeField] private Shower _shower;
    [SerializeField] private PraiseControl _praiseControl;

    private void Start()
    {
        _taskText.text = _objectiveTexts[_currentObjectiveNameIndex];

        //setup objectives
        objectives.Add(_wakeUpControl);
        objectives.Add(_shower);
        objectives.Add(_praiseControl);

        InitSpecialControls();
    }

    private void InitSpecialControls()
    {
        //only give wake up to start, give the rest later
        _player.InitControls(_wakeUpControl);
    }

    protected override void OnObjectiveCompleted(int objectivesComplete)
    {
        _currentObjectiveNameIndex++;
        _taskText.text = _objectiveTexts[_currentObjectiveNameIndex];
        base.OnObjectiveCompleted(objectivesComplete);
        if (objectivesComplete == 1)
        {
            //special case, give all controls now
            _player.GiveAllControls();
        }
    }
}
