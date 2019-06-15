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
        hanger.Init(c_btnPrefabPath, followTrans.gameObject, 100);
        hanger.SetIcon(PropData.GetData<string>("icon"));
        hanger.RegisterOnClick(OnPropBeginUsing);

        m_uiBtn = hanger.HangObject;
        m_uiBtn.SetActive(false); // 默认隐藏
    }
}
