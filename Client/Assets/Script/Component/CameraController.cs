using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        MapNode hero = MapNodeManager.Instance.GetHeroNode();
        if(hero != null)
        {
            transform.position = new Vector3(hero.transform.position.x, transform.position.y, transform.position.z);
        }
        
    }
}
