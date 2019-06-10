using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DotweenTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOMove(new Vector3(0, 100, 0), 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
