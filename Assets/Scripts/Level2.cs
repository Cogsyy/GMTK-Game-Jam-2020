using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class Level2 : LevelBase
{
    /*Objectives
    - Drink Coffee
    - Talk to other human
    - Work
    - Talk to boss
    - Work again
    - Praise Overlords
    - Leave
    */
    

    [SerializeField] private ControllableEntity _player;
    [SerializeField] private TMP_Text _taskText;
    private int _currentObjectiveNameIndex = 0;

    [Header("Objectives")]
    [SerializeField] private string[] _objectiveTexts;
    [SerializeField] private CoffeeMachine _coffee;
    [SerializeField] private HumanObject _humanObject;
    [SerializeField] private BossObject _bossObj;
    [SerializeField] private WorkDeskObject _workObj;
    [SerializeField] private PraiseControl _praiseControl;
    [SerializeField] private LeaveTransitionAsObjective _leaveObj;
    private void Start()
    {
        _taskText.text = _objectiveTexts[_currentObjectiveNameIndex];

        //setup objectives

        objectives.Add(_coffee);
        objectives.Add(_humanObject);
        objectives.Add(_bossObj);
        objectives.Add(_workObj);
        objectives.Add(_praiseControl);
        objectives.Add(_leaveObj);

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
