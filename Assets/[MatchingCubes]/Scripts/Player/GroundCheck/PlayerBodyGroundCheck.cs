using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyGroundCheck : GroundCheckBase
{
    [SerializeField] private Transform _originBody;
    public override Transform OriginBody { get => _originBody; protected set => _originBody = value; }
}
