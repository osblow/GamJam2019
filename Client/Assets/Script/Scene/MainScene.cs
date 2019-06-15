using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene:SceneBase
{
    public enum Stage
    {
        Start = 1,//女主被出撞死的状态
        BadEnd,//在开消防栓之前，男主举牌，被左车撞死亡
        Stage1,//阻止老鼠拨成红灯后，女主被砸死的状态
        Stage2,//用消防栓拦住汽车，两车相撞的状态(女主过马路,取决于红绿灯)
        Stage3,//用消防栓拦住汽车，两车相撞的状态，主角切成绿灯，女主成功过马路
        Stage4,//用消防栓拦住汽车，两车相撞的状态，主角切成红灯灯，女主被砸死的状态
        BadEnd2,//在开消防栓之后，男主举牌，被右车撞死亡
        NormalEnd,//Stage3 跟女主离开，普通结局
        GoodEnd,//Stage3 主角阻止两车相撞牺牲，结束循环
    }

    public Stage TargetStage = 0;
    Stage m_curStage = Stage.Start;

    float timer = 0;
    Dictionary<Stage, float> m_dicStageTime = new Dictionary<Stage, float>()
    {
        {Stage.Start,10 },
        {Stage.BadEnd,100000},
        {Stage.Stage1,10 },
        {Stage.Stage2,10 },
        {Stage.Stage3,10},
        {Stage.Stage4,10},
        {Stage.BadEnd2,100000},
        {Stage.NormalEnd,100000},
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
    Vector2 m_carBadEndPos = new Vector2(41, -164f);
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

    //--------------------------------------------------------
    bool m_audioCarHitBody = false;
    bool m_audioCarHitCar = false;
    bool m_audioTimeStretch = false;

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

        // 遍历场景内所有道具
        PropMgr.Instance.Init(m_scene.transform);
    }

    public override void Run()
    {
        PlayScene();
        timer += Time.deltaTime;

        if (timer > m_dicStageTime[m_curStage]-0.5f)
        {
            m_sceneCamera.transform.GetComponent<ScreenEffect>().enabled = true;
            if (!m_audioTimeStretch)
            {
                AudioManager.Instance.PlaySoundByGO(AudioData.DATA["time_stretch"], m_car2.gameObject);
                m_audioTimeStretch = true;
            }     
        }

        if (timer> m_dicStageTime[m_curStage])
        {
            ResetScene();
            m_audioTimeStretch = false;
            m_screenEffectFlag = false;
            m_sceneCamera.transform.GetComponent<ScreenEffect>().enabled = false;
            timer = 0;
            return;
        }  
    }

    void ResetScene()
    {
        if(TargetStage != 0 && m_curStage != TargetStage)
        {
            m_curStage = TargetStage;
        }
        if (m_curStage == Stage.Start)
        {
            m_car.transform.localPosition = m_carStartOrgPos;
            m_girl.Idle();
            m_audioCarHitBody = false;
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
            m_audioCarHitCar = false;
        }

        if (m_curStage == Stage.Stage3)
        {
            m_car.transform.localPosition = m_carStartOrgPos;
            m_car2.transform.localPosition = m_car2StartOrgPos;
            m_steel.transform.localPosition = m_steelStartOrgPos;
            m_girl.Idle(); 
            m_audioCarHitCar = false;
        }

        if (m_curStage == Stage.Stage4)
        {
            m_car.transform.localPosition = m_carStartOrgPos;
            m_car2.transform.localPosition = m_car2StartOrgPos;
            m_steel.transform.localPosition = m_steelStartOrgPos;
            m_girl.Idle();
            m_audioCarHitCar = false;
        }

        if (m_curStage == Stage.BadEnd)
        {
            //不重置
        }

        if (m_curStage == Stage.BadEnd2)
        {
            //不重置
        }

        if (m_curStage == Stage.NormalEnd)
        {
            //不重置
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
                if (!m_audioCarHitBody)
                {
                    AudioManager.Instance.PlaySoundByGO(AudioData.DATA["car_hit_body"], m_car.gameObject);
                    m_audioCarHitBody = true;
                }
            }
            
        }

        if (m_curStage == Stage.BadEnd)
        {
            float distance = m_carBadEndPos.x - m_car.transform.localPosition.x;

            if (distance > 0.1f)
            {
                if (timer > 4)
                {
                    m_car.transform.Translate(new Vector2(1, 0) * 10f * Time.deltaTime);
                }
            }
            else
            {
                //m_girl.Die(); //男主死亡
                if (!m_audioCarHitBody)
                {
                    AudioManager.Instance.PlaySoundByGO(AudioData.DATA["car_hit_body"], m_car.gameObject);
                    m_audioCarHitBody = true;
                }
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

                // 地上的扳手显示出来
                PropBase prop = PropMgr.Instance.GetProp(208);
                prop.gameObject.SetActive(true);
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
            else
            {
                if (!m_audioCarHitCar)
                {
                    AudioManager.Instance.PlaySoundByGO(AudioData.DATA["car_hit_car"], m_car.gameObject);
                    m_audioCarHitCar = true;
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

        if (m_curStage == Stage.Stage3)
        {
            float carDistance = m_carGoodEndPos.x - m_car.transform.localPosition.x;
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
            else
            {
                if (!m_audioCarHitCar)
                {
                    AudioManager.Instance.PlaySoundByGO(AudioData.DATA["car_hit_car"], m_car.gameObject);
                    m_audioCarHitCar = true;
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
            //todo 女主生还
        }

        if (m_curStage == Stage.Stage4)
        {
            float carDistance = m_carGoodEndPos.x - m_car.transform.localPosition.x;
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
            else
            {
                if (!m_audioCarHitCar)
                {
                    AudioManager.Instance.PlaySoundByGO(AudioData.DATA["car_hit_car"], m_car.gameObject);
                    m_audioCarHitCar = true;
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
            //todo 女主死亡
        }

        if (m_curStage == Stage.BadEnd2)
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
            else
            {
                if (!m_audioCarHitCar)
                {
                    AudioManager.Instance.PlaySoundByGO(AudioData.DATA["car_hit_car"], m_car.gameObject);
                    m_audioCarHitCar = true;
                    //todo 男主死亡
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
            //todo 女主死亡
        }

        if (m_curStage == Stage.NormalEnd)
        {
            float carDistance = m_carGoodEndPos.x - m_car.transform.localPosition.x;
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
            else
            {
                if (!m_audioCarHitCar)
                {
                    AudioManager.Instance.PlaySoundByGO(AudioData.DATA["car_hit_car"], m_car.gameObject);
                    m_audioCarHitCar = true;
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
            //todo 和女主离开动画
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
