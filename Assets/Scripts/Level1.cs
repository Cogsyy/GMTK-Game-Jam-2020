using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

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
    [SerializeField] private CoffeeMachine _coffee;
    [SerializeField] private LeaveTransitionAsObjective _leaveObj;

    private void Start()
    {
        _taskText.text = _objectiveTexts[_currentObjectiveNameIndex];

        //setup objectives
        objectives.Add(_wakeUpControl);
        objectives.Add(_shower);
        objectives.Add(_praiseControl);
        objectives.Add(_coffee);
        objectives.Add(_leaveObj);

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
        if (_currentObjectiveNameIndex < _objectiveTexts.Count())
        {
            _taskText.text = _objectiveTexts[_currentObjectiveNameIndex];
        }
        
        base.OnObjectiveCompleted(objectivesComplete);
        if (objectivesComplete == 1)
        {
            //special case, give all controls now
            _player.GiveAllControls();
        }
    }
}
