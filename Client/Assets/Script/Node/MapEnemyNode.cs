using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapEnemyNode : MapLifeNode
{
    public float m_maxMoveSpeed = 2.5f;
    public float m_maxJumpSpeed = 8f;
    public float m_moveSpeed = 2f;
    public float m_jumpSpeed = 500f;
    int m_jumpFlag = 1;  //弹跳标志符

    AnimationPlayer m_animPlayer;

    void Start()
    {
        m_animPlayer = Util.GetOrAddComponent<AnimationPlayer>(gameObject);

        CurHP = 100;
        MaxHP = 100;

        InitAnimation();
        InitAttack();
    }


    void Update()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        MapNode hero = MapNodeManager.Instance.GetHeroNode();
        Vector3 direction = hero.transform.position - transform.position;
        //m_rigidbody.MovePosition(m_rigidbody.position + new Vector2(direction.x * m_moveSpeed, 0) * Time.deltaTime);

        if(Mathf.Abs(direction.x) > 5 )
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
            transform.Translate(new Vector3(Mathf.Sign(direction.x)*direction.normalized.x, 0, 0) * m_moveSpeed * Time.deltaTime);
        }
        else
        {
            m_animPlayer.Play(AnimationData.DATA["hero_idle"]);
        }
        
    }


    void InitAnimation()
    {
        m_animPlayer.Play(AnimationData.DATA["cat_idle"]);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        m_jumpFlag = 1;
    }

    //void OnCollisionStay2D(Collision2D collision)
    //{
    //    Debug.Log("OnCollisionStay");
    //    m_jump_flag = 1;
    //}

    void OnCollisionExit2D(Collision2D collision)
    {
        m_jumpFlag = 0;
    }

    void InitAttack()
    {
        
    }
}

