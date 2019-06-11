using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNodeManager : MonoBehaviour
{
    public static MapNodeManager Instance;

    Transform m_transMainCanvas;    //挂载点

    MapNode m_hero;

    void Awake()
    {
        Instance = this;
        m_transMainCanvas = GameObject.Find("Canvas/Main").transform;
    }

    public MapNode GetHeroNode()
    {
        if(m_hero == null)
        {
            GameObject go = GameObject.Instantiate(Resources.Load("Prefab/Character/Hero"), m_transMainCanvas) as GameObject;
            m_hero = Util.GetOrAddComponent<MapHeroNode>(go);
        }

        return m_hero;
    }
}
