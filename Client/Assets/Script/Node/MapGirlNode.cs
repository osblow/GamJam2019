using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MapGirlNode : MapLifeNode
{
    public float m_moveSpeed = 2f;

    AnimationPlayer m_animPlayer;
    MotionState m_motionState = MotionState.Idle;
    Vector2 m_startSize = new Vector3(0.8f, 0.8f, 0.8f);
    Color m_startColor = new Color(0.7f, 0.7f, 0.7f);

    void Start()
    {
        m_animPlayer = Util.GetOrAddComponent<AnimationPlayer>(gameObject);
        transform.localScale = m_startSize;
        GetComponent<Image>().color = m_startColor;
    }


    void Update()
    {
        CheckMove();
        UpdateAnimation();
    }

    void CheckMove()
    {
        if (m_motionState == MotionState.Die)
        {
            return;
        }

        if (m_motionState == MotionState.Leave)
        {
            Vector2 direction = new Vector2(-1, 0);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.Translate(direction * m_moveSpeed * Time.deltaTime);
            return;
        }
        if(m_motionState == MotionState.Cross)
        {
            transform.DOScale(new Vector3(1f, 1f, 1f), 2);
            Tweener twn = GetComponent< Image>().DOColor(new Color(1, 1, 1), 2);
            twn.SetEase(Ease.Linear);
            twn.OnComplete(OnCrossComplete);
        }
    }

    void OnCrossComplete()
    {
        Idle();
    }
    //void CheckDistance()
    //{
    //    MapNode hero = MapNodeManager.Instance.GetHeroNode();
    //    Vector3 direction = hero.transform.position - transform.position;
    //    //m_rigidbody.MovePosition(m_rigidbody.position + new Vector2(direction.x * m_moveSpeed, 0) * Time.deltaTime);

    //    if (Mathf.Abs(direction.x) > 5)
    //    {
    //        m_animPlayer.Play(AnimationData.DATA["hero_run"]);
    //        if (Mathf.Sign(direction.x) < 0)
    //        {
    //            transform.rotation = Quaternion.Euler(0, 180, 0);
    //        }
    //        else
    //        {
    //            transform.rotation = Quaternion.Euler(0, 0, 0);
    //        }
    //        transform.Translate(new Vector3(Mathf.Sign(direction.x) * direction.normalized.x, 0, 0) * m_moveSpeed * Time.deltaTime);
    //    }
    //    else
    //    {
    //        m_animPlayer.Play(AnimationData.DATA["hero_idle"]);
    //    }

    //}

    void UpdateAnimation()
    {
        if (m_motionState == MotionState.Idle)
        {
            m_animPlayer.Play(AnimationData.DATA["cat_idle_m"]);
        }
        else if (m_motionState == MotionState.Run)
        {
            m_animPlayer.Play(AnimationData.DATA["cat_run_m"]);
        }
        else if (m_motionState == MotionState.Die)
        {
            m_animPlayer.Play(AnimationData.DATA["cat_die_m"]);
        }
        else if (m_motionState == MotionState.Leave)
        {
            m_animPlayer.Play(AnimationData.DATA["cat_run_m"]);
        }
        else if (m_motionState == MotionState.Cross)
        {
            m_animPlayer.Play(AnimationData.DATA["cat_run_m"]);
        }
    }

    public void Die()
    {
        m_motionState = MotionState.Die;
    }

    public void Idle(bool needReset = false)
    {
        m_motionState = MotionState.Idle;
        if (needReset)
        {
            Reset();
        }
    }

    public void Leave()
    {
        m_motionState = MotionState.Leave;
    }

    public void Cross()
    {
        m_motionState = MotionState.Cross;
        
    }
    public void Reset()
    {
        transform.localScale = m_startSize;
        GetComponent<Image>().color = m_startColor;
    }
}

