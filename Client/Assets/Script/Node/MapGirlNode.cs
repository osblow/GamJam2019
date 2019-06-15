﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapGirlNode : MapLifeNode
{
    public float m_moveSpeed = 2f;

    AnimationPlayer m_animPlayer;
    MotionState m_motionState = MotionState.Idle;

    void Start()
    {
        m_animPlayer = Util.GetOrAddComponent<AnimationPlayer>(gameObject);

    }


    void Update()
    {
        UpdateAnimation();
    }

    void CheckDistance()
    {
        MapNode hero = MapNodeManager.Instance.GetHeroNode();
        Vector3 direction = hero.transform.position - transform.position;
        //m_rigidbody.MovePosition(m_rigidbody.position + new Vector2(direction.x * m_moveSpeed, 0) * Time.deltaTime);

        if (Mathf.Abs(direction.x) > 5)
        {
            m_animPlayer.Play(AnimationData.DATA["hero_run"]);
            if (Mathf.Sign(direction.x) < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            transform.Translate(new Vector3(Mathf.Sign(direction.x) * direction.normalized.x, 0, 0) * m_moveSpeed * Time.deltaTime);
        }
        else
        {
            m_animPlayer.Play(AnimationData.DATA["hero_idle"]);
        }

    }

    void UpdateAnimation()
    {
        if (m_motionState == MotionState.Idle)
        {
            m_animPlayer.Play(AnimationData.DATA["hero_idle"]);
        }
        else if (m_motionState == MotionState.Die)
        {
            m_animPlayer.PlayOneFrame(AnimationData.DATA["hero_jump"],10);
        }
    }

    public void Die()
    {
        m_motionState = MotionState.Die;
    }

    public void Idle()
    {
        m_motionState = MotionState.Idle;
    }
}
