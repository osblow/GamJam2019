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

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        InitController();
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter");
        m_jumpFlag = 1;
    }

    //void OnCollisionStay2D(Collision2D collision)
    //{
    //    Debug.Log("OnCollisionStay");
    //    m_jump_flag = 1;
    //}

    void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("OnCollisionExit");
        m_jumpFlag = 0;
    }
}
