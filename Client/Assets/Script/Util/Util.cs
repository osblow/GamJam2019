using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    public static T GetOrAddComponent<T>(GameObject go) where T:Component
    {
        T cmp = go.GetComponent<T>();
        if(cmp == null)
        {
            cmp = go.AddComponent<T>();
        }
        return cmp;
    }
}
