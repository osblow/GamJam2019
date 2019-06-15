using System;
using System.Collections.Generic;
using UnityEngine;

class EndPointProp : PropBase
{
    public override void OnPropEndUsing()
    {
        base.OnPropEndUsing();

        Debug.Log("Game Over");
    }
}
