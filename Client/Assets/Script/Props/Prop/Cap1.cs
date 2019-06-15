using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 第一个井盖，交互之后与撬棍组合
/// 车经过时打开
/// </summary>
public class Cap1 : PropBase
{
    GameObject m_car;


    protected override void Awake()
    {
        base.Awake();
        m_car = GameObject.Find("MainCanvas/Main/Panel1/Car1");
    }


    public override void OnPropBeginUsing()
    {
        if (m_isUsing) return;

        Debug.Log("to use prop " + PropId);

        // 检查前置
        if (!PropData.CheckPrevProp())
        {
            Debug.Log("previous prop not exist");
            return;
        }

        m_uiBtn.SetActive(false);
        OnPropUsing();
        //Inventory.Instance.AddProp(PropData);

        m_isUsing = true;
    }

    private void Update()
    {
        if (!m_isUsing) return;

        if (m_isOver) return;

        if(Mathf.Abs(m_car.transform.position.x - transform.position.x) <= 4)
        {
            Inventory.Instance.AddProp(PropData);
            m_isOver = true;
        }
    }
}
