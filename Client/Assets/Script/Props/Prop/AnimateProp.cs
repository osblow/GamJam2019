using System;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;


public class AnimateProp : PropBase
{
    public override void OnPropEndUsing()
    {
        base.OnPropEndUsing();

        m_isOver = true;

        DoAction();   

        // 暂时当作：只要把消防栓打开，下面就会淹水，红绿灯会乱掉
        if(PropId == 206)
        {
            ReusabeProp prop = (ReusabeProp)PropMgr.Instance.GetProp(205);
            prop.IsWaterFull = true;
            prop.SetGreenLight();

            GameObject.Find("MainCanvas/Main/Panel1/Interact/ImgHydrant").GetComponent<MapHydrantNode>().EnableWaterAnim();
            GameObject.Find("MainCanvas/Main/Panel1/Front/UnderWaterMask").SetActive(true);
        }
    }

    private void DoAction()
    {
        PropAnimationType type = PropData.GetData<PropAnimationType>("anim_type");
        if (type == 0) return;

        Debug.Log("do action: " + type);

        switch (type)
        {
            case PropAnimationType.OFFSET:
                Vector3 offset = PropData.GetData<Vector3>("anim_offset");
                transform.DOMove(transform.position + offset, 0.5f);
                break;
            case PropAnimationType.ROTATION:
                float rotation = PropData.GetData<float>("anim_rot");
                transform.DORotate(new Vector3(0, 0, rotation), 0.5f);
                break;
            default:
                break;
        }
    }



    protected override void InitBtn()
    {
        UIHangerPropBtn hanger = gameObject.AddComponent<UIHangerPropBtn>();

        // align
        Transform followTrans = transform.Find("follow");
        hanger.Init(c_btnPrefabPath, followTrans ? followTrans.gameObject:gameObject, 100);

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
        hanger.SetIcon(iconPath);
        
        hanger.RegisterOnClick(OnPropBeginUsing);

        m_uiBtn = hanger.HangObject;
        m_uiBtn.SetActive(false); // 默认隐藏
    }
}
