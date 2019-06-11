using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatePlay : StateBase
{
    public override void Start()
    {
        SceneManager.Instance.ChangeScene<MainScene>();
    }

    public override void Run()
    {

    }

    public override void End()
    {
        
    }
}
