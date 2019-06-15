using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropBase : MonoBehaviour
{
    public int PropId;

    public bool ShowByDefault = true; // 场景初始化时是否直接显示

    public PropData PropData; // 从id初始化

    protected GameObject m_uiBtn = null; // 浮在头顶的按钮
    protected bool m_isInTrigger = false; // 是否与主角发生碰撞
    protected bool m_isUsing = false; // 正在使用中，不显示使用的图标
    
    public void OnPropBeginUsing()
    {
        if (m_isUsing) return;

        Debug.Log("to use prop " + PropId);

        // 检查前置
        if (!PropData.CheckPrevProp())
        {
            Debug.Log("previous prop not exist");
            return;
        }

        OnPropUsing();
        Inventory.Instance.AddProp(PropData);

        m_isUsing = true;
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
        m_isUsing = false;
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_isInTrigger) return;

        // 显示操作按钮
        Debug.Log("enter prop" + PropId);

        if (!m_isUsing)
        {
            // 使用过程中不用显示按钮
            m_uiBtn.SetActive(true);
        }

        m_isInTrigger = true;
    }
    

    public void OnTriggerExit2D(Collider2D other)
    {
        if (!m_isInTrigger) return;

        // 隐藏操作按钮
        Debug.Log("left prop" + PropId);

        m_uiBtn.SetActive(false);

        m_isInTrigger = false;
    }



    private void Awake()
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


    private const string c_btnPrefabPath = "Prefab/UI/Hanger/HangerPropBtn";
    private void InitBtn()
    {
        UIHangerPropBtn hanger = gameObject.AddComponent<UIHangerPropBtn>();
        hanger.Init(c_btnPrefabPath, gameObject, 100);
        hanger.SetIcon(PropData.GetData<string>("icon"));
        hanger.RegisterOnClick(OnPropBeginUsing);

        m_uiBtn = hanger.HangObject;
        m_uiBtn.SetActive(false); // 默认隐藏
    }
}
