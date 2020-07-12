using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBase : MonoBehaviour
{
    protected List<IObjective> objectives = new List<IObjective>();
    private int _currentObjective = 0;

    private void Update()
    {
        int triggerCount = 0;
        for (int i = 0; i < objectives.Count; i++)
        {
            if (objectives[i].Triggered())
            {
                triggerCount++;
                if (_currentObjective < triggerCount)
                {
                    _currentObjective = triggerCount;
                    OnObjectiveCompleted(_currentObjective);
                }
            }
        }
        
        if (triggerCount >= objectives.Count)
        {
            OnCompleteLevel();
        }
    }

    protected virtual void OnObjectiveCompleted(int objectivesComplete)
    {
        AudioController.Instance.PlayTaskComplete();
    }

    protected virtual void OnCompleteLevel()
    {
        Debug.Log("You win");
    }
}
