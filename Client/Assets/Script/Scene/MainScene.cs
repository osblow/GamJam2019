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
    MapHeroNode m_hero;
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
    Mouse m_mouse;

    bool m_audioCarHitBody = false;
    bool m_audioCarHitCar = false;
    bool m_audioTimeStretch = false;

    public override void Init()
    {
        m_transMainCanvas = GameObject.Find("MainCanvas/Main").transform;
        m_scene = GameObject.Find("MainCanvas/Main/Panel1");
        m_scene.SetActive(true);

        m_sceneCamera = GameObject.Find("MainCanvas/Main/SceneCamera").GetComponent<Camera>();

        m_hero = MapNodeManager.Instance.CreateHeroNode() as MapHeroNode;
        m_hero.transform.SetParent(GameObject.Find("MainCanvas/Main/Panel1/NPC").transform);
        m_hero.transform.localPosition = m_heroStartPos;

        m_car = GameObject.Find("MainCanvas/Main/Panel1/Car1");
        m_car.transform.localPosition = m_carStartOrgPos;

        m_car2 = GameObject.Find("MainCanvas/Main/Panel1/Car2");
        m_car2.transform.localPosition = m_car2StartOrgPos;

        m_steel = GameObject.Find("MainCanvas/Main/Panel1/Steel");
        m_steel.transform.localPosition = m_steelStartOrgPos;

        m_girl = GameObject.Find("MainCanvas/Main/Panel1/NPC/Girl").GetComponent<MapGirlNode>();

        m_mouse = GameObject.Find("MainCanvas/Main/Panel1/Interact/mouse").GetComponent<Mouse>();
        //GameObject.Find("MainCanvas/Main/Panel1/Interact/ImgHydrant").GetComponent<MapHydrantNode>().EnableWaterAnim();

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


    private static bool s_firstGrilDie = true;
    private static List<Stage> s_showedCommentStages = new List<Stage>();// 已经展示过一次提示的情景
    void AddCommentory()
    {
        if (m_curStage == Stage.Start && !s_showedCommentStages.Contains(m_curStage))
        {
            UICommentory.Instance.SetTips("咦？刚才发生了什么，怎么脑子里多出了一段记忆...");
            UICommentory.Instance.SetTips("我回到了小美被撞前的10秒钟，但时间不够我跑过去");
            UICommentory.Instance.SetTips("不！我一定要救她！");
            UICommentory.Instance.SetTips("先看看周围有什么可以利用的东西");
            s_showedCommentStages.Add(m_curStage);
        }

        if (m_curStage == Stage.Stage1 && !s_showedCommentStages.Contains(m_curStage))
        {
            UICommentory.Instance.SetTips("不！！！！！！！！！！");
            UICommentory.Instance.SetTips("还是得让小美过到马路对面去");
            s_showedCommentStages.Add(m_curStage);
        }

        if (m_curStage == Stage.Stage2 && !s_showedCommentStages.Contains(m_curStage))
        {
            UICommentory.Instance.SetTips("小美得救了。但是他们却...我好像害了更多的人");
            s_showedCommentStages.Add(m_curStage);
        }
    }


    void ResetScene()
    {
        // 恢复主角行动, 并把路牌还原
        //InputManger.Instance.enabled = true;

        //if (Inventory.Instance.GetProp(207) != null)
        //{
        //    PropMgr.Instance.GetProp(207).OnPropBeginUsing(); // 相当于再点击一次
        //}

        //
        AddCommentory();
        

        if (TargetStage != 0 && m_curStage != TargetStage)
        {
            if(TargetStage == Stage.BadEnd || TargetStage == Stage.BadEnd2 || TargetStage == Stage.NormalEnd || TargetStage == Stage.GoodEnd)
            {
                //如果是结局，保持当前状态重置一次
            }
            else
            {
                m_curStage = TargetStage;
            }
        }

        if (m_curStage == Stage.Start)
        {
            m_car.transform.localPosition = m_carStartOrgPos;
            m_steel.transform.localPosition = m_steelStartOrgPos;
            m_girl.Idle();
            m_audioCarHitBody = false;
            m_mouse.Reset();
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
        //如果是结局，重置后再切状态
        if (TargetStage != 0 && m_curStage != TargetStage)
        {
            if (TargetStage == Stage.BadEnd || TargetStage == Stage.BadEnd2 || TargetStage == Stage.NormalEnd || TargetStage == Stage.GoodEnd)
            {
                m_curStage = TargetStage;
            }
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
                    AudioManager.Instance.PlaySoundByGO(AudioData.DATA["car_drive"], m_car.gameObject);
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

                // 上方文字提示
                if (s_firstGrilDie)
                {
                    UICommentory.Instance.SetTips("小美！！！天哪怎么会这样！");
                    s_firstGrilDie = false;
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
                m_hero.Die(); //男主死亡
                if (!m_audioCarHitBody)
                {
                    AudioManager.Instance.PlaySoundByGO(AudioData.DATA["car_hit_body"], m_car.gameObject);
                    m_audioCarHitBody = true;
                }


                ResultView.Instance.gameObject.SetActive(true);
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
                prop.gameObject.SetActive(!prop.IsOver);

                // 上面的扳手表现消失
                GameObject.Find("MainCanvas/Main/Panel1/Steel/wrentch").SetActive(false);
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

            // 显示女主头上的离开按钮
            PropMgr.Instance.GetProp(209).gameObject.SetActive(true);

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

            // 显示女主头上的离开按钮
            PropMgr.Instance.GetProp(209).gameObject.SetActive(true);

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
            else
            {
                //todo 女主死亡
                m_girl.Die();
            }
            
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
                    m_hero.Die(); //男主死亡
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
                //todo 女主死亡
                m_girl.Die();
            }
            
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
            else
            {
                if (!m_audioCarHitCar)
                {
                    AudioManager.Instance.PlaySoundByGO(AudioData.DATA["car_hit_car"], m_car.gameObject);
                    m_audioCarHitCar = true;
                    m_hero.Die(); //男主死亡
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
