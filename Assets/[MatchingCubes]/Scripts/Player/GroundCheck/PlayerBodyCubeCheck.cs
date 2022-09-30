using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyCubeCheck : GroundCheckBase
{
    [SerializeField] private Transform _originBody;
    public override Transform OriginBody { get => _originBody; protected set => _originBody = value; }
}
