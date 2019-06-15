using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene:SceneBase
{
    enum Stage
    {
        Start,
        Stage1,
        End,
    }

    Stage m_curStage = Stage.Start;
    float timer = 0;
    Dictionary<Stage, float> m_dicStageTime = new Dictionary<Stage, float>()
    {
        {Stage.Start,3 },
    };

    Transform m_transMainCanvas;

    GameObject m_scene;
    //--------------------------------------------------------
    MapNode m_hero;
    Vector2 m_heroStartPos = new Vector2(50,-166.4f);

    //--------------------------------------------------------
    GameObject m_car;
    Vector2 m_carStartOrgPos;
    Vector2 m_carStartEndPos;
    //--------------------------------------------------------

    //--------------------------------------------------------

    public override void Init()
    {
        m_transMainCanvas = GameObject.Find("MainCanvas/Main").transform;
        m_scene = GameObject.Find("MainCanvas/Main/Panel1");
        m_scene.SetActive(true);

        m_hero = MapNodeManager.Instance.CreateHeroNode();
        m_hero.transform.SetParent(GameObject.Find("MainCanvas/Main/Panel1/NPC").transform);
        m_hero.transform.localPosition = m_heroStartPos;

        m_car = GameObject.Find("MainCanvas/Main/Panel1/Car1");
        m_carStartOrgPos = m_car.transform.position;
        m_carStartEndPos = new Vector2(m_car.transform.position.x + 10, m_car.transform.position.y);

        AudioManager.Instance.PlayBGM(AudioData.DATA["bg_1"]);
    }

    public override void Run()
    {
        timer += Time.deltaTime;
        if(timer> m_dicStageTime[m_curStage])
        {
            ResetScene();
            timer = 0;
            return;
        }
        PlayScene();
    }

    void ResetScene()
    {
        if (m_curStage == Stage.Start)
        {
            m_car.transform.position = m_carStartOrgPos;
        }
    }

    void PlayScene()
    {
        if (m_curStage == Stage.Start)
        {
            float distance = m_carStartEndPos.x - m_car.transform.position.x;

            if (distance > 0.1f)
            {
                m_car.transform.Translate(new Vector2(1, 0) * 5f * Time.deltaTime);
            }
            
        }
    }
}
