using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 为一次性使用道具。使用后立即消失
/// </summary>
class InstantProp : PropBase
{
    public override void OnPropEndUsing()
    {
        base.OnPropEndUsing();

        gameObject.SetActive(false);
        m_uiBtn.SetActive(false);
        m_isOver = true;

        if(PropId == 209)
        {
            //和女主离开动画
            ((MapHeroNode)MapNodeManager.Instance.GetHeroNode()).Leave();
            GameObject.Find("MainCanvas/Main/Panel1/NPC/Girl").GetComponent<MapGirlNode>().Leave();

            // 召唤结束界面
            TimeMgr.Instance.TimerOnce(2f, delegate () {
                ResultView.Instance.gameObject.SetActive(true);
                ResultView.Instance.SetTips(ResultType.NORMAL);
            });
        }
    }
}