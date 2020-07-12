using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class ControllableEntity : MonoBehaviour
{
    [SerializeField] private LoadingBar _loadingBar;
    [SerializeField] private SpriteRenderer _humanSpriteRenderer;

    private List<DirectionalControl> _directionalControls;

    [NonSerialized] public bool interacting;
    
    private void Awake()
    {
        FindControls();
    }

    private void FindControls()
    {
        GiveAllControls();//by default
    }

    public void GiveAllControls()
    {
        _directionalControls = GetComponents<DirectionalControl>().ToList();
        List<Controls> allControls = GetComponents<Controls>().ToList();

        CommandsList.RegisterAction(() => CommandsList.Instance.InitCommandsList(allControls));
    }

    public void InitControls(params Controls[] allowedControls)
    {
        List<Controls> allowedControlsList = allowedControls.ToList();

        _directionalControls = new List<DirectionalControl>();
        for (int i = 0; i < allowedControlsList.Count; i++)
        {
            if (allowedControls[i] is DirectionalControl)
                _directionalControls.Add(allowedControls[i] as DirectionalControl);
        }

        CommandsList.Instance.InitCommandsList(allowedControlsList);
    }

    private void Update()
    {
        UpdateDirectionalControls();
    }

    private void UpdateDirectionalControls()
    {
        Vector3 newDelta = new Vector3();
        for (int i = 0; i < _directionalControls.Count; i++)
        {
            newDelta += _directionalControls[i].GetMoveDelta();
        }

        if(newDelta.x > 0)
            _humanSpriteRenderer.flipX = false;
        if (newDelta.x < 0)
            _humanSpriteRenderer.flipX = true;

        transform.position += newDelta;
    }

    public Coroutine LoadForDuration(float duration)
    {
        return _loadingBar.StartLoading(duration);
    }
}
