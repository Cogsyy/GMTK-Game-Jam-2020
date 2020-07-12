using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FreeWill
{
    private CommandsList _commandList;

    private float _timeWithoutFreeWill = 0;

    private float _timeToTakeBackControl;

    //consts
    private const float TAKE_CONTROL_MIN = 5;
    private const float TAKE_CONTROL_MAX = 15;

    private const int MAX_TAKEOVER_CONTROLS = 4;

    private int _controlsTaken = 0;

    public FreeWill(CommandsList commandList)
    {
        _commandList = commandList;
        DetermineTakeControlTime();
    }

    private void DetermineTakeControlTime()
    {
        _timeWithoutFreeWill = 0;
        _timeToTakeBackControl = UnityEngine.Random.Range(TAKE_CONTROL_MIN, TAKE_CONTROL_MAX);
    }

    public void Update()
    {
        if (_controlsTaken >= MAX_TAKEOVER_CONTROLS)
        {
            return;
        }

        _timeWithoutFreeWill += Time.deltaTime;

        if (_timeWithoutFreeWill > _timeToTakeBackControl)
        {
            TakeControl();
            DetermineTakeControlTime();
        }
    }

    private void TakeControl()
    {
        _controlsTaken++;
        _commandList.DeactivateRandomAllowedCommand();
    }

    public void RegainControl(Controls command)
    {
        _controlsTaken--;
    }
}
