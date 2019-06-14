using System;
using System.Collections.Generic;
using UnityEngine;

public class MapMgr
{
    private static Dictionary<int, Map> s_maps = new Dictionary<int, Map>();

    private static void Init(string[] prefabPaths)
    {
        s_maps.Clear();
        foreach(string path in prefabPaths)
        {
            Map newMap = new Map(path);
            s_maps.Add(newMap.Id, new Map(path));
        }
    }
}


public class Map
{
    public PropMgr PropMgr;

    public int Id; // 从prefab配置中读取


    public Map(string path)
    {
        // 从prefab加载，并初始化PropMgr
        GameObject mapObj = GameObject.Instantiate<GameObject>(Resources.Load(path) as GameObject);

        PropMgr = new PropMgr(mapObj.transform);
    }
}
