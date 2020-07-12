using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;

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

    [Header("Objectives")]
    [SerializeField] private WakeUpControl _wakeUpControl;
    [SerializeField] private Shower _shower;

    private void Start()
    {
        //setup objectives
        objectives.Add(_wakeUpControl);
        objectives.Add(_shower);

        InitSpecialControls();
    }

    private void InitSpecialControls()
    {
        //only give wake up to start, give the rest later
        _player.InitControls(_wakeUpControl);
    }

    protected override void OnObjectiveCompleted(int objectivesComplete)
    {
        base.OnObjectiveCompleted(objectivesComplete);
        if (objectivesComplete == 1)
        {
            //special case, give all controls now
            _player.GiveAllControls();
        }
    }
}
