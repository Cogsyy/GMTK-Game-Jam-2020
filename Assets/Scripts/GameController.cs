﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    protected override void OnSetSingleton()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {

    }

    private void Update()
    {

    }
}
