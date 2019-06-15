using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapStairsNode : MapNode
{
    UIHanger m_UIHanger;
    Button m_btnClimb;
    GameObject m_upPoint;
    GameObject m_downPoint;

    void Awake()
    {
        m_UIHanger = Util.GetOrAddComponent<UIHanger>(gameObject);
        m_btnClimb = transform.Find("BtnStairs").GetComponent<Button>();
        m_btnClimb.onClick.AddListener(OnClimbClick);
        m_upPoint = transform.Find("PointUp").gameObject;
        m_downPoint = transform.Find("PointDown").gameObject;
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void SetButtonAble(bool flag)
    {
        m_btnClimb.gameObject.SetActive(flag);
    }


    void OnClimbClick()
    {
        MapHeroNode hero = MapNodeManager.Instance.GetHeroNode() as MapHeroNode;
        
        if (hero.transform.position.y > transform.position.y)    //在上面
        {
            hero.Climb(m_upPoint.transform.position,m_downPoint.transform.position);
        }
        else
        {
            hero.Climb(m_downPoint.transform.position, m_upPoint.transform.position);
        }
    }
}
