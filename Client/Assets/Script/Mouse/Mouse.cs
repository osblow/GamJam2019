using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public Transform[] Points;
    public RedGreenLightCtrl RedGreenLightCtrl;
    public Transform Switch;

    public bool IsOver = false;

    private const float c_stepInterval1 = 4;
    private const float c_stepInterval2 = 0.5f;

    private float m_timer = 0;
    private int m_step = 0;

    public void Reset()
    {
        if (IsOver) return;
        transform.position = Points[0].position;
        transform.rotation = Points[0].rotation;

        // 初始是红灯
        RedGreenLightCtrl.SetState(GreenLightState.RED);
        Switch.rotation = Quaternion.Euler(0, 0, 0);

        m_step = 0;
        m_timer = 0;
    }

    public void End()
    {
        if (IsOver) return;

        RedGreenLightCtrl.SetState(GreenLightState.GREEN);
        Switch.rotation = Quaternion.Euler(0, 0, -45);

        IsOver = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOver) return;

        m_timer += Time.deltaTime;

        if(m_step == 0)
        {
            transform.position = Vector3.Lerp(Points[0].position, Points[1].position, m_timer / c_stepInterval1);

            if(Mathf.Abs(m_timer - c_stepInterval1) < 0.1f)
            {
                transform.rotation = Points[1].rotation;
                m_step = 1;
                m_timer = 0;
            }
        }
        else if(m_step == 1)
        {
            transform.position = Vector3.Lerp(Points[1].position, Points[2].position, m_timer / c_stepInterval2);
            transform.rotation = Quaternion.Lerp(Points[1].rotation, Points[2].rotation, m_timer / c_stepInterval2);

            if (Mathf.Abs(m_timer - c_stepInterval2) < 0.1f)
            {
                m_step = 2;
                Switch.transform.rotation = Quaternion.Euler(0, 0, -45);
                RedGreenLightCtrl.SetState(GreenLightState.GREEN);
            }
        }
    }
}
