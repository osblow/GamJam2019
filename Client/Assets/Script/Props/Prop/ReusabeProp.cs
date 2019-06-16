using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


/// <summary>
/// 相当于重写逻辑。在使用之后再次点击，将恢复原状态
/// </summary>
public class ReusabeProp : PropBase
{
    public RedGreenLightCtrl RedGreenLightCtrl; // 红绿灯控制器
    public bool IsWaterFull = false; // 是否水已经漫上来了

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
            // 主角动作
            ((MapHeroNode)MapNodeManager.Instance.GetHeroNode()).Operate();

            // 重置状态
            GetComponent<Image>().sprite = m_savedSprite;
            m_isOver = false;
            Inventory.Instance.RemoveProp(PropId);
            m_uiBtn.SetActive(false);

            // 执行动作
            PropAction action = PropData.GetData<PropAction>("reset_action");
            if (action.Action != null)
            {
                action.Action();
            }
            // 还原 旋转动画
            if (PropId == 205 || PropId == 207)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if (PropId == 205)
            {
                SetGreenLight();
            }

            // 道具重置完后的文字
            string tips = PropData.GetData<string>("reset_comment");
            if (tips != default(string))
            {
                UICommentory.Instance.SetTips(tips);
            }

            return;
        }

        base.OnPropBeginUsing();

        // 使用 旋转动画
        if (PropId == 205)
        {
            // 旋转一定角度开启
            float rotation = -45f;
            transform.DORotate(new Vector3(0, 0, rotation), 0.5f);
            SetGreenLight();
        }
        if (PropId == 207)
        {
            // 旋转一定角度开启
            float rotation = -71f;
            transform.DORotate(new Vector3(0, 0, rotation), 0.5f);
        }

    }

    public override void OnPropEndUsing()
    {
        base.OnPropEndUsing();
        m_isOver = true;

        if (PropId == 207)
        {
            // 当使用了路牌后，就得一直扶着
            InputManger.Instance.enabled = false;
        }
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


    public void SetGreenLight()
    {
        GreenLightState state = GreenLightState.GREEN;

        // 分为4种情况
        bool usedSwitch = Inventory.Instance.GetProp(205) != null;

        if (!IsWaterFull && usedSwitch)
        {
            // 1. 水没漫上来，使用了开关。此时显示正常红灯
            state = GreenLightState.RED;
        }
        else if (!IsWaterFull && !usedSwitch)
        {
            // 2. 水没漫上来，未使用开关。此时显示正常绿灯
            state = GreenLightState.GREEN;
        }
        else if (IsWaterFull && usedSwitch)
        {
            // 3. 水漫上来了，使用了开关。此时显示异常红灯
            state = GreenLightState.RED_WITH_CHAOS;
        }
        else
        {
            // 4. 水漫上来了，未使用开关。此时显示异常绿灯
            state = GreenLightState.GREEN_WITH_CHAOS;
        }

        RedGreenLightCtrl.SetState(state);
    }
}
