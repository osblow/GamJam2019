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
        m_scene = GameObject.Find("MainCanvas/Main/Panel1");
        m_scene.SetActive(true);

        MapNode m_hero = MapNodeManager.Instance.CreateHeroNode();

        AudioManager.Instance.PlayBGM(AudioData.DATA["bg_1"]);
    }

    public override void Run()
    {

    }
}
