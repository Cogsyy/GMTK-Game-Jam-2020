using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_BackandForth : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private RectTransform _rectTrans;
    [SerializeField] float _duration;
    [SerializeField] bool _isRepeating = false;
    [SerializeField] Vector3 _RotA;
    [SerializeField] Vector3 _RotB;
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private Light _light;
    [SerializeField] private float _intensityHighPoint;
    [SerializeField] private float _intensityLowPoint;

    private void OnEnable()
    {
        StartCoroutine(RotationToPosB());
    }

    private IEnumerator RotationToPosA()
    {
        float t = 0;

        Quaternion from = Quaternion.Euler(_RotA);
        Quaternion to = Quaternion.Euler(_RotB);

        while (t < 1)
        {
            t += Time.deltaTime / _duration;
            SetRotation(Quaternion.Lerp(from, to, _curve.Evaluate(t)));
            _light.intensity = Mathf.Lerp(_intensityHighPoint, _intensityLowPoint, _curve.Evaluate(t));
            yield return null;
        }

        if (_isRepeating)
            StartCoroutine(RotationToPosB());
    }

    private IEnumerator RotationToPosB()
    {
        float t = 0;

        Quaternion from = Quaternion.Euler(_RotB);
        Quaternion to = Quaternion.Euler(_RotA);

        while (t < 1)
        {
            t += Time.deltaTime / _duration;
            SetRotation(Quaternion.Lerp(from, to, _curve.Evaluate(t)));
            _light.intensity = Mathf.Lerp(_intensityLowPoint, _intensityHighPoint, _curve.Evaluate(t));

            yield return null;
        }

        if (_isRepeating)
            StartCoroutine(RotationToPosA());
    }

    private void SetRotation(Quaternion rotation)
    {
        if(_rectTrans != null)
            _rectTrans.rotation = rotation;

        if (_transform != null)
            _transform.rotation = rotation;
    }
}
