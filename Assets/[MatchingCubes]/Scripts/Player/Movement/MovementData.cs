using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Movement Data", menuName = "Movement Data")]
public class MovementData : ScriptableObject
{
    public float DefaultSpeed;
    public float FeverSpeed;    
}
