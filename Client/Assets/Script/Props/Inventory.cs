using System;
using System.Collections.Generic;
using UnityEngine;



public class Inventory
{
    public static Inventory Instance
    {
        get
        {
            if(s_ins == null)
            {
                s_ins = new Inventory();
            }
            return s_ins;
        }
    }
    private static Inventory s_ins;
    private Inventory()
    {
        InitUI();
    }



    private Dictionary<int, PropData> m_allProps = new Dictionary<int, PropData>();
    

    /// <summary>
    /// 只要是使用过的道具，都要进入背包
    /// 使用标记来区分是否显示在界面的背包中
    /// </summary>
    /// <param name="data"></param>
    public void AddProp(PropData data)
    {
        m_allProps.Add(data.Id, data);
    }

    public void RemoveProp(int id)
    {
        if (m_allProps.ContainsKey(id))
        {
            m_allProps.Remove(id);
        }
    }

    public PropData GetProp(int id)
    {
        if (m_allProps.ContainsKey(id))
        {
            return m_allProps[id];
        }

        return null;
    }



    private const string UIPREFAB_PATH = "";
    private void InitUI()
    {
        //Transform UICanvas = GameObject.Find("UICanvas/UI");

    }
}
