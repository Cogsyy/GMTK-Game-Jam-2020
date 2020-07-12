using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class Level5 : LevelBase
{
    [SerializeField] private TMP_Text _taskText;
    private int _currentObjectiveNameIndex = 0;

    [Header("Objectives")]
    [SerializeField] private string[] _objectiveTexts;
    [SerializeField] private SimpleLoadObject _whiskey;
    [SerializeField] private SimpleLoadObject _tv;
    [SerializeField] private SimpleLoadObject _dog;
    [SerializeField] private PraiseControl _praiseControl;
    [SerializeField] private Bed _bed;

    private void Start()
    {
        _taskText.text = _objectiveTexts[_currentObjectiveNameIndex];

        //setup objectives
        objectives.Add(_whiskey);
        objectives.Add(_tv);
        objectives.Add(_dog);
        objectives.Add(_praiseControl);
        objectives.Add(_bed);
    }

    protected override void OnObjectiveCompleted(int objectivesComplete)
    {
        _currentObjectiveNameIndex++;
        if (_currentObjectiveNameIndex < _objectiveTexts.Count())
        {
            _taskText.text = _objectiveTexts[_currentObjectiveNameIndex];
        }

        base.OnObjectiveCompleted(objectivesComplete);
    }
}
