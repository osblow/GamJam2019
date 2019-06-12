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
        m_transMainCanvas = GameObject.Find("MainCanvas/Main").transform;
    }

    public MapNode CreateHeroNode()
    {
        if(m_hero == null)
        {
            GameObject go = GameObject.Instantiate(Resources.Load("Prefab/Character/Hero"), m_transMainCanvas) as GameObject;
            m_hero = Util.GetOrAddComponent<MapHeroNode>(go);
        }

        return m_hero;
    }

    public MapNode GetHeroNode()
    {
        return m_hero;
    }

    public T CreateNode<T>(string path,Transform parent = null) where T:MapNode
    {
        GameObject go;
        if (parent)
        {
            go = GameObject.Instantiate(Resources.Load(path), parent) as GameObject;
        }
        else
        {
            go = GameObject.Instantiate(Resources.Load(path)) as GameObject;
        }

        T node = Util.GetOrAddComponent<T>(go);

        return node;
    }
}
