using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [SerializeField] private CommandsList _commandList;

    private float _timeWithoutFreeWill = 0;

    private float _timeToTakeBackControl;

    //consts
    private const float TAKE_CONTROL_MIN = 5;
    private const float TAKE_CONTROL_MAX = 15;

    private const int MAX_TAKEOVER_CONTROLS = 4;

    private int _controlsTaken = 0;

    private void Start()
    {
        //init game
        DetermineTakeControlTime();
    }

    private void DetermineTakeControlTime()
    {
        _timeWithoutFreeWill = 0;
        _timeToTakeBackControl = Random.Range(TAKE_CONTROL_MIN, TAKE_CONTROL_MAX);
    }

    private void Update()
    {
        UpdateFreeWill();
    }

    private void UpdateFreeWill()
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

    private void RegainControl(Controls command)
    {
        _controlsTaken--;
    }
}
