using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene:SceneBase
{
    Transform m_transMainCanvas;

    GameObject m_scene;

    public override void Init()
    {
        m_transMainCanvas = GameObject.Find("MainCanvas/Main").transform;
        m_scene = GameObject.Instantiate(Resources.Load("Prefab/Scene/SceneTest"), m_transMainCanvas) as GameObject;

        MapNode m_hero = MapNodeManager.Instance.CreateHeroNode();
    }

    public override void Run()
    {

    }
}
