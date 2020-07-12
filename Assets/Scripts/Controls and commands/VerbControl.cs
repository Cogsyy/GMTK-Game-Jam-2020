using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerbControl : Controls
{
    [SerializeField] private Verb _verb;

}

public enum Verb
{
    None = 0,
    Open, //Open door, will need to see if player is in trigger
    Grab, //grab object, will need to see if player is in trigger
    Eat, //eat object, will need to see if player is holding object that they can eat
    Drink, //drink object, will need to see if player is holding object that they can drink
    Cry, //Cry, always available
    Praise, //Praise the overlords, always available
}
