using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControllableEntity : MonoBehaviour
{
    [SerializeField] private CommandsList _commandsList;

    private List<DirectionalControl> _directionalControls;
    
    private void Start()
    {
        FindControls();
    }

    private void FindControls()
    {
        _directionalControls = GetComponents<DirectionalControl>().ToList();
        _commandsList.InitCommandsList(_directionalControls.Cast<Controls>().ToList());
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
