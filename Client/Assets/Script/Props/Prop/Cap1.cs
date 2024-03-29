﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


/// <summary>
/// 第一个井盖，交互之后与撬棍组合
/// 车经过时打开
/// </summary>
public class Cap1 : PropBase
{
    GameObject m_car;


    protected override void Awake()
    {
        base.Awake();
        m_car = GameObject.Find("MainCanvas/Main/Panel1/Car1");
    }


    public override void OnPropBeginUsing()
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
            AudioManager.Instance.PlaySoundByGO(AudioData.DATA["error"], gameObject);
            Debug.Log("previous prop not exist");
            return;
        }

        // 消费掉前置物品
        int prevId = PropData.GetData<int>("prev_prop");
        if (prevId != 0)
        {
            Inventory.Instance.RemoveProp(prevId);
        }

        m_uiBtn.SetActive(false);
        OnPropUsing();
        //Inventory.Instance.AddProp(PropData);

        m_isUsing = true;
    }

    private void Update()
    {
        if (!m_isUsing) return;

        if (m_isOver) return;

        if(Mathf.Abs(m_car.transform.position.x - transform.position.x) <= 4)
        {
            // 特殊处理，换回井盖原图
            //Texture2D tex = Resources.Load("Sprite/Prop/lib_without_bar") as Texture2D;
            //Sprite spr = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            //GetComponent<Image>().sprite = spr;

            // 旋转一定角度开启
            float rotation = -160f;
            transform.DORotate(new Vector3(0, 0, rotation), 0.5f).SetDelay(0.5f);

            Inventory.Instance.AddProp(PropData);
            m_isOver = true;
        }
    }
}
