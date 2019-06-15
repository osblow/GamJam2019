using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 相当于重写逻辑。在使用之后再次点击，将恢复原状态
/// </summary>
public class ReusabeProp : PropBase
{
    Sprite m_savedSprite = null;


    protected override void Awake()
    {
        base.Awake();

        // 保存自己的icon，用于还原状态
        m_savedSprite = GetComponent<Image>().sprite;
    }


    public override void OnPropBeginUsing()
    {
        if (m_isOver)
        {
            // 重置状态
            GetComponent<Image>().sprite = m_savedSprite;
            m_isOver = false;
            Inventory.Instance.RemoveProp(PropId);
            m_uiBtn.SetActive(false);
            return;
        }

        base.OnPropBeginUsing();
    }

    public override void OnPropEndUsing()
    {
        base.OnPropEndUsing();
        m_isOver = true;
    }


    public override void OnTriggerEnter2D(Collider2D collision)
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


    public override void OnTriggerExit2D(Collider2D other)
    {
        if (!m_isInTrigger) return;

        // 隐藏操作按钮
        Debug.Log("left prop" + PropId);

        m_uiBtn.SetActive(false);

        m_isInTrigger = false;
    }
}
