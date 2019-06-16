using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropBase : MonoBehaviour
{
    public int PropId;

    public bool ShowByDefault = true; // 场景初始化时是否直接显示

    public GameObject AssociatedObj;

    public PropData PropData; // 从id初始化

    protected GameObject m_uiBtn = null; // 浮在头顶的按钮
    protected bool m_isInTrigger = false; // 是否与主角发生碰撞
    protected bool m_isUsing = false; // 正在使用中，不显示使用的图标
    protected bool m_isOver = false; // 已经使用结束，无交互
    
    public virtual void OnPropBeginUsing()
    {
        if (m_isUsing) return;

        Debug.Log("to use prop " + PropId);

        // 主角动作
        ((MapHeroNode)MapNodeManager.Instance.GetHeroNode()).Operate();

        // 检查前置
        if (!PropData.CheckPrevProp())
        {
            // 道具无法使用时的文字
            string tips = PropData.GetData<string>("prev_comment");
            if (tips != default(string))
            {
                UICommentory.Instance.SetTips(tips);
            }

            Debug.Log("previous prop not exist");
            return;
        }

        // 消费掉前置物品
        int prevId = PropData.GetData<int>("prev_prop");
        if (prevId != 0)
        {
            Inventory.Instance.RemoveProp(prevId);
        }


        m_isUsing = true;


        if (PropId != 209) m_uiBtn.SetActive(false);
        OnPropUsing();
        Inventory.Instance.AddProp(PropData);
    }

    /// <summary>
    /// 一般在这里播个动画啥的
    /// </summary>
    public virtual void OnPropUsing()
    {
        OnPropEndUsing();
    }

    public virtual void OnPropEndUsing()
    {
        // 使用完后图标的替换
        string usedIconPath = PropData.GetData<string>("used_icon");
        if(usedIconPath != default(string))
        {
            Texture2D tex = Resources.Load(usedIconPath) as Texture2D;
            Sprite spr = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            GetComponent<Image>().sprite = spr;
        }

        // 使用完后所关联物体图标的替换
        string associateIconPath = PropData.GetData<string>("associated_obj_used_icon");
        if (AssociatedObj && associateIconPath != default(string))
        {
            Texture2D tex = Resources.Load(associateIconPath) as Texture2D;
            Sprite spr = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            AssociatedObj.GetComponent<Image>().sprite = spr;
        }

        // 执行动作
        PropAction action = PropData.GetData<PropAction>("used_action");
        if(action.Action != null)
        {
            action.Action();
        }

        // 道具使用完后的文字
        string tips = PropData.GetData<string>("used_comment");
        if(tips != default(string))
        {
            UICommentory.Instance.SetTips(tips);
        }

        m_isUsing = false;
    }


    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_isInTrigger || m_isOver) return;

        // 显示操作按钮
        Debug.Log("enter prop" + PropId);

        if (!m_isUsing)
        {
            // 使用过程中不用显示按钮
            m_uiBtn.SetActive(true);
        }

        m_isInTrigger = true;
    }
    

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (!m_isInTrigger || m_isOver) return;

        // 隐藏操作按钮
        Debug.Log("left prop" + PropId);

        if (PropId != 209) m_uiBtn.SetActive(false);

        m_isInTrigger = false;
    }



    protected virtual void Awake()
    {
        // 初始化数据
        PropData = new PropData(PropId);

        // 初始化显示状态
        gameObject.SetActive(ShowByDefault);
        InitBtn();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public const string c_btnPrefabPath = "Prefab/UI/Hanger/HangerPropBtn";
    protected virtual void InitBtn()
    {
        UIHangerPropBtn hanger = gameObject.GetComponent<UIHangerPropBtn>();
        if(hanger == null)
        {
            hanger = gameObject.AddComponent<UIHangerPropBtn>();
            hanger.Init(c_btnPrefabPath, gameObject, 100);
            hanger.RegisterOnClick(OnPropBeginUsing);
        }

        if(PropId == 204)
        {
            Debug.Log("");
        }

        // 检查是否有依赖的道具，如果有并且依赖的道具有图标，则显示为该图标
        string iconPath = "Sprite/Prop/use_normal";
        int prevId = PropData.GetData<int>("prev_prop");
        if (prevId != 0)
        {
            PropData theData = new PropData(prevId);
            string thePath = theData.GetData<string>("icon");
            if (thePath != default(string))
            {
                iconPath = thePath;
            }
        }

        // 特殊处理一下，如果是最后的心心，使用心的图标
        if (PropId == 209) iconPath = "Sprite/Prop/use_love";
        hanger.SetIcon(iconPath);

        m_uiBtn = hanger.HangObject;
        if (PropId != 209) m_uiBtn.SetActive(false); // 默认隐藏
    }
}
