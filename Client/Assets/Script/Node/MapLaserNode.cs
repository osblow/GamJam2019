using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapLaserNode : MapNode
{
    LineRenderer m_renderer;
    float m_duration = 999;
    //float timer = 0;
    void Start()
    {
    }


    void Update()
    {
        m_duration -= Time.deltaTime;
        if(m_duration <= 0)
        {
            GameObject.Destroy(gameObject);
        } 
    }

    public void Init(Vector3 from, Vector3 to,float duration = 1, float startWidth = 0.1f, float endWidth = 0.1f)
    {
        m_renderer = GetComponent<LineRenderer>();
        m_renderer.startWidth = startWidth;
        m_renderer.endWidth = endWidth;
        m_renderer.positionCount = 2;
        m_renderer.SetPosition(0, from);
        m_renderer.SetPosition(1, to);

        m_duration = duration;
    }
}
