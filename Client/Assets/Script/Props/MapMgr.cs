using System;
using System.Collections.Generic;
using UnityEngine;

public class MapMgr
{
    private static Dictionary<int, Map> s_maps = new Dictionary<int, Map>();

    private static void Init(string[] prefabPaths)
    {
        s_maps.Clear();
        for (int i = 0; i < prefabPaths.Length; i++)
        {
            s_maps.Add(i, new Map(i, prefabPaths[i]));
        }
    }

    private static void Clear()
    {
        foreach(Map map in s_maps.Values)
        {
            map.Destroy();
        }

        s_maps.Clear();
    }
}


public class Map
{
    public PropMgr PropMgr;

    public int Id; // 从prefab配置中读取
    public GameObject GameObject;

    public Map(int id, string path)
    {
        Id = id;

        // 从prefab加载，并初始化PropMgr
        GameObject = GameObject.Instantiate<GameObject>(Resources.Load(path) as GameObject);

        PropMgr = new PropMgr(GameObject.transform);
    }

    public void Destroy()
    {
        GameObject.Destroy(GameObject);
    }
}
