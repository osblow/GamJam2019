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
        {Stage.Start,10 },
    };

    Transform m_transMainCanvas;
    Camera m_sceneCamera;

    GameObject m_scene;
    //--------------------------------------------------------
    MapNode m_hero;
    Vector2 m_heroStartPos = new Vector2(-510,-166.4f);

    //--------------------------------------------------------
    GameObject m_car;
    Vector2 m_carStartOrgPos = new Vector2(-837, -164f);
    Vector2 m_carStartEndPos = new Vector2(393, -164f);
    //--------------------------------------------------------

    MapGirlNode m_girl;
    //--------------------------------------------------------
    bool m_screenEffectFlag = false;


    public override void Init()
    {
        m_transMainCanvas = GameObject.Find("MainCanvas/Main").transform;
        m_scene = GameObject.Find("MainCanvas/Main/Panel1");
        m_scene.SetActive(true);

        m_sceneCamera = GameObject.Find("MainCanvas/Main/SceneCamera").GetComponent<Camera>();

        m_hero = MapNodeManager.Instance.CreateHeroNode();
        m_hero.transform.SetParent(GameObject.Find("MainCanvas/Main/Panel1/NPC").transform);
        m_hero.transform.localPosition = m_heroStartPos;

        m_car = GameObject.Find("MainCanvas/Main/Panel1/Car1");
        m_car.transform.localPosition = m_carStartOrgPos;
        //m_carStartOrgPos = m_car.transform.position;
        //m_carStartEndPos = new Vector2(m_car.transform.position.x + 10, m_car.transform.position.y);

        m_girl = GameObject.Find("MainCanvas/Main/Panel1/NPC/Girl").GetComponent<MapGirlNode>();

        AudioManager.Instance.PlayBGM(AudioData.DATA["bg_1"]);
    }

    public override void Run()
    {
        PlayScene();
        timer += Time.deltaTime;

        if (timer > m_dicStageTime[m_curStage]-0.5f)
        {
            m_sceneCamera.transform.GetComponent<ScreenEffect>().enabled = true;
        }

        if (timer> m_dicStageTime[m_curStage])
        {
            ResetScene();
            m_screenEffectFlag = false;
            m_sceneCamera.transform.GetComponent<ScreenEffect>().enabled = false;
            timer = 0;
            return;
        }  
    }

    void ResetScene()
    {
        if (m_curStage == Stage.Start)
        {
            m_car.transform.localPosition = m_carStartOrgPos;
            m_girl.Idle();
        }
    }

    void PlayScene()
    {
        if (m_curStage == Stage.Start)
        {
            float distance = m_carStartEndPos.x - m_car.transform.localPosition.x;

            if (distance > 0.1f && timer >5)
            {
                m_car.transform.Translate(new Vector2(1, 0) * 10f * Time.deltaTime);
            }
            else
            {
                m_girl.Die();
            }
            
        }
    }
}
