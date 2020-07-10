using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControllableEntity : MonoBehaviour
{
    private List<DirectionalControl> _directionalControls;
    
    private void Start()
    {
        FindControls();
    }

    private void FindControls()
    {
        _directionalControls = GetComponents<DirectionalControl>().ToList();
    }

    private void Update()
    {
        UpdateDirectionalControls();
        UpdateControls();
    }

    private void UpdateDirectionalControls()
    {
        Vector3 newDelta = new Vector3();
        for (int i = 0; i < _directionalControls.Count; i++)
        {
            newDelta += _directionalControls[i].GetMoveDelta();
        }

        transform.position += newDelta;
    }

    private void UpdateControls()
    {
        
    }
}
