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

    private float _wobbleValue = 0.5f;
    private float _wobbleDuration = 0.13f;
    private float _wobbleClampMin = 0.5f;
    private float _wobbleClampMax = 0.5f;
    private const int WOBBLE_ROT_MIN = -8;
    private const int WOBBLE_ROT_MAX = 8;
    private int _scaler = 1;

    private float _stretchSquashTimer = 0.5f;
    private float _stretchSquashDuration = 1.3f;
    private int _stretchSquashScaler = 1;
    private const float SQUASH_MIN = 0.95f;
    private const float SQUASH_MAX = 1.05f;

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
        UpdateStretchAndSquash();
        UpdateStretchSquashValue();
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

        bool moving = newDelta.magnitude > 0;

        if (!moving)
        {
            _wobbleValue = Mathf.Clamp(_wobbleValue, _wobbleClampMin, _wobbleClampMax);
        }

        Wobble();
        UpdateWobbleValue();

        transform.position += newDelta;
    }

    private void UpdateWobbleValue()
    {
        _wobbleValue += (Time.deltaTime / _wobbleDuration) * _scaler;

        if (_wobbleValue > 1)
        {
            _scaler = -1;
            _wobbleClampMin = 0.5f;
            _wobbleClampMax = _wobbleValue;
        }
        else if (_wobbleValue < 0)
        {
            _scaler = 1;
            _wobbleClampMin = _wobbleValue;
            _wobbleClampMax = 0.5f;
        }
    }

    private void UpdateStretchSquashValue()
    {
        _stretchSquashTimer += (Time.deltaTime / _stretchSquashDuration) * _stretchSquashScaler;

        if (_stretchSquashTimer > 1)
        {
            _stretchSquashScaler = -1;
        }
        else if (_stretchSquashTimer < 0)
        {
            _stretchSquashScaler = 1;
        }
    }

    private void Wobble()
    {
        float amount = Mathf.Lerp(WOBBLE_ROT_MIN, WOBBLE_ROT_MAX, Mathf.Sin(_wobbleValue));

        Vector3 targetEulerAngles = _humanSpriteRenderer.transform.localEulerAngles;
        targetEulerAngles.z = amount;
        _humanSpriteRenderer.transform.localEulerAngles = targetEulerAngles;
    }

    private void UpdateStretchAndSquash()
    {
        float amount = Mathf.Lerp(SQUASH_MIN, SQUASH_MAX, _stretchSquashTimer);

        Vector3 newScale = _humanSpriteRenderer.transform.localScale;
        newScale.y = amount;
        _humanSpriteRenderer.transform.localScale = newScale;
    }

    public Coroutine LoadForDuration(float duration)
    {
        return _loadingBar.StartLoading(duration);
    }
}
