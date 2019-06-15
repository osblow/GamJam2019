using System;
using System.Collections.Generic;
using UnityEngine;

public class PropMgr : MonoBehaviour
{
    public static PropMgr Instance;
    private void Awake()
    {
        Instance = this;
    }



    public int SceneId; // 每个地图都维护一个道具管理器

    private Dictionary<int, PropBase> m_allPropsInScene = new Dictionary<int, PropBase>();


    /// <summary>
    /// 从配置好的map预设初始化整个图的道具
    /// </summary>
    /// <param name="sceneRoot"></param>
    public void Init(Transform sceneRoot)
    {
        // 遍历整个子树，将所有挂载道具的保存起来
        WalkTransform(sceneRoot, GetPropCom);
    }

    public PropBase GetProp(int id)
    {
        if (!m_allPropsInScene.ContainsKey(id))
        {
            return null;
        }

        return m_allPropsInScene[id];
    }

    public void Clear()
    {
        // 
        m_allPropsInScene.Clear();
    }



    private void GetPropCom(Transform transform)
    {
        PropBase com = transform.GetComponent<PropBase>();
        if(com == null)
        {
            return;
        }

        m_allPropsInScene.Add(com.PropId, com);
    }


    private static void WalkTransform(Transform trans, Action<Transform> handleFunc)
    {
        handleFunc(trans);

        if (trans.childCount <= 0) return;

        for (int i = 0; i < trans.childCount; i++)
        {
            WalkTransform(trans.GetChild(i), handleFunc);
        }
    }
}
