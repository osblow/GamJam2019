using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene:SceneBase
{
    enum Stage
    {
        Start,//女主被出撞死的状态
        Stage1,//阻止老鼠拨成红灯后，女主被砸死的状态
        Stage2,//用消防栓拦住汽车，女主成功过马路后，两车相撞的状态
        GoodEnd,//主角阻止辆车相撞牺牲，结束循环
    }

    Stage m_curStage = Stage.Start;
    float timer = 0;
    Dictionary<Stage, float> m_dicStageTime = new Dictionary<Stage, float>()
    {
        {Stage.Start,10 },
        {Stage.Stage1,10 },
        {Stage.Stage2,10 },
        {Stage.GoodEnd,100000 },
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
    Vector2 m_carStage1EndPos = new Vector2(827, -164f);
    Vector2 m_carStage2EndPos = new Vector2(-173, -164f);
    Vector2 m_carGoodEndPos = new Vector2(-173, -164f);
    //--------------------------------------------------------
    GameObject m_car2;
    Vector2 m_car2StartOrgPos = new Vector2(817, -164f);
    Vector2 m_car2Stage2EndPos = new Vector2(52, -164f);
    Vector2 m_car2GoodEndPos = new Vector2(304, -164f);
    //--------------------------------------------------------
    GameObject m_steel;
    Vector2 m_steelStartOrgPos = new Vector2(556, 158f);
    Vector2 m_steelStage1EndPos = new Vector2(556, -115f);
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

        m_car2 = GameObject.Find("MainCanvas/Main/Panel1/Car2");
        m_car2.transform.localPosition = m_car2StartOrgPos;

        m_steel = GameObject.Find("MainCanvas/Main/Panel1/Steel");
        m_steel.transform.localPosition = m_steelStartOrgPos;

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

        if (m_curStage == Stage.Stage1)
        {
            m_car.transform.localPosition = m_carStartOrgPos;
            m_steel.transform.localPosition = m_steelStartOrgPos;
            m_girl.Idle();
        }

        if (m_curStage == Stage.Stage2)
        {
            m_car.transform.localPosition = m_carStartOrgPos;
            m_car2.transform.localPosition = m_car2StartOrgPos;
            m_steel.transform.localPosition = m_steelStartOrgPos;
            m_girl.Idle();
        }

        if (m_curStage == Stage.GoodEnd)
        {
            //不重置
        }
    }

    void PlayScene()
    {
        if (m_curStage == Stage.Start)
        {
            float distance = m_carStartEndPos.x - m_car.transform.localPosition.x;

            if (distance > 0.1f)
            {
                if (timer > 4)
                {
                    m_car.transform.Translate(new Vector2(1, 0) * 10f * Time.deltaTime);
                }   
            }
            else
            {
                m_girl.Die();
            }
            
        }

        if (m_curStage == Stage.Stage1)
        {
            float carDistance = m_carStage1EndPos.x - m_car.transform.localPosition.x;
            if (carDistance > 0.1f)
            {
                if (timer > 4)
                {
                    m_car.transform.Translate(new Vector2(1, 0) * 10f * Time.deltaTime);
                }
            }

            float steelDistance = m_steelStage1EndPos.y - m_steel.transform.localPosition.y;
            if (steelDistance < -0.1f)
            {
                if (timer > 8)
                {
                    m_steel.transform.Translate(new Vector2(0, -1) * 10f * Time.deltaTime);
                }
            }
            else
            {
                m_girl.Die();
            }

        }

        if (m_curStage == Stage.Stage2)
        {
            float carDistance = m_carStage2EndPos.x - m_car.transform.localPosition.x;
            if (carDistance > 0.1f)
            {
                if (timer > 4)
                {
                    m_car.transform.Translate(new Vector2(1, 0) * 10f * Time.deltaTime);
                }
            }

            float car2Distance = m_car2Stage2EndPos.x - m_car2.transform.localPosition.x;
            if (car2Distance < -0.1f)
            {
                if (timer > 6)
                {
                    m_car2.transform.Translate(new Vector2(-1, 0) * 10f * Time.deltaTime);
                }
            }

            float steelDistance = m_steelStage1EndPos.y - m_steel.transform.localPosition.y;
            if (steelDistance < -0.1f)
            {
                if (timer > 8)
                {
                    m_steel.transform.Translate(new Vector2(0, -1) * 10f * Time.deltaTime);
                }
            }
        }

        if (m_curStage == Stage.GoodEnd)
        {
            float carDistance = m_carGoodEndPos.x - m_car.transform.localPosition.x;
            if (carDistance > 0.1f)
            {
                if (timer > 4)
                {
                    m_car.transform.Translate(new Vector2(1, 0) * 10f * Time.deltaTime);
                }
            }

            float car2Distance = m_car2GoodEndPos.x - m_car2.transform.localPosition.x;
            if (car2Distance < -0.1f)
            {
                if (timer > 6)
                {
                    m_car2.transform.Translate(new Vector2(-1, 0) * 10f * Time.deltaTime);
                }
            }

            float steelDistance = m_steelStage1EndPos.y - m_steel.transform.localPosition.y;
            if (steelDistance < -0.1f)
            {
                if (timer > 8)
                {
                    m_steel.transform.Translate(new Vector2(0, -1) * 10f * Time.deltaTime);
                }
            }
        }
    }
}
