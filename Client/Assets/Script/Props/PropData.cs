using System;
using System.Collections.Generic;
using UnityEngine;

public class PropData
{
    public int Id;
    public GameObject GameObject = null;

    protected Dictionary<string, object> m_datas;

    public PropData(int id)
    {
        Id = id;

        if (!PropConfig.S_CONFIGS.ContainsKey(id))
        {
            Debug.LogError("道具配置不存在,id= " + id);
            m_datas = new Dictionary<string, object>();
            return;
        }

        // init from data table
        m_datas = PropConfig.S_CONFIGS[id];
    }

    public T GetData<T>(string key)
    {
        if (!m_datas.ContainsKey(key))
        {
            return default(T);
        }

        return (T)m_datas[key];
    }

    /// <summary>
    /// 检查前置道具是否已获取，前置道具可以有多个
    /// </summary>
    /// <returns></returns>
    public bool CheckPrevProp()
    {
        int prevProp = GetData<int>("prev_prop");
        if (prevProp == 0) return true;

        return Inventory.Instance.GetProp(prevProp) != null;
    }
}
