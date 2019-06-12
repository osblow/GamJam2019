using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHeroNode : MapNode
{
    public float m_maxMoveSpeed = 2.5f;
    public float m_maxJumpSpeed = 8f;
    public float m_moveSpeed = 5f;
    public float m_jumpSpeed = 500f;
    int m_jumpFlag = 1;  //弹跳标志符
    Rigidbody2D m_rigidbody;
    AnimationPlayer m_animPlayer;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animPlayer = Util.GetOrAddComponent<AnimationPlayer>(gameObject);
        InitController();
        InitAnimation();
    }


    void Update()
    {
        //限制移动速度
        if(Mathf.Abs(m_rigidbody.velocity.x) >= m_maxMoveSpeed)
        {
            m_rigidbody.velocity = new Vector2(Mathf.Sign(m_rigidbody.velocity.x) * m_maxMoveSpeed, m_rigidbody.velocity.y);
        }

        if(Mathf.Abs(m_rigidbody.velocity.y) >= m_maxJumpSpeed)
        {
            m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, Mathf.Sign(m_rigidbody.velocity.y) * m_maxJumpSpeed);
        }
        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        if (Mathf.Abs(m_rigidbody.velocity.y) > 0)
        {
            m_animPlayer.Play(AnimationData.DATA["hero_jump"]);
        }
        else if (Mathf.Abs(m_rigidbody.velocity.x) > 0)
        {
            if (Mathf.Sign(m_rigidbody.velocity.x) < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            m_animPlayer.Play(AnimationData.DATA["hero_run"]);
        }
        else
        {
            m_animPlayer.Play(AnimationData.DATA["hero_idle"]);
        }

        
    }

    void InitController()
    {
        m_rigidbody.freezeRotation = true;
        InputManger.Instance.RegistMoveDelegate(delegate (Vector3 direction)
        {
            //transform.Translate(direction * m_speed * Time.deltaTime);
            //m_rigidbody.MovePosition(m_rigidbody.position + new Vector2(direction.x * m_move_speed, direction.y * m_jump_speed * m_jump_flag)* Time.deltaTime);
            m_rigidbody.AddForce(new Vector2(direction.x * m_moveSpeed, direction.y * m_jumpSpeed * m_jumpFlag));
        });
    }

    void InitAnimation()
    {
        m_animPlayer.Play(AnimationData.DATA["hero_idle"]);
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
}
