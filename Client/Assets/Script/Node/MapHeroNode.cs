﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapHeroNode : MapLifeNode
{
    

    public float m_moveSpeed = 5f;
    MotionState m_motionState = MotionState.Idle;

    Vector2 m_target_pos = Vector2.zero;
    Vector2 m_climb_pos = Vector2.zero;

    Rigidbody2D m_rigidbody;
    AnimationPlayer m_animPlayer;
    UIHangerText m_UIHanger;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animPlayer = Util.GetOrAddComponent<AnimationPlayer>(gameObject);
        m_UIHanger = Util.GetOrAddComponent<UIHangerText>(gameObject);

        CurHP = 100;
        MaxHP = 100;

        InitController();
        InitAnimation();
        InitAttack();


        //m_UIHanger.Init("Prefab/UI/Hanger/HangerNameBlood", transform.Find("Head").gameObject);
        //m_UIHanger.SetName("Hero");

    }


    void Update()
    {
        CheckMove();

        UpdateAnimation();
    }

    void CheckMove()
    {
        //爬梯
        if(m_climb_pos != Vector2.zero)
        {
            Vector2 direction = m_climb_pos - new Vector2(transform.position.x, transform.position.y);
            float distance = Mathf.Abs(direction.y);
            if (distance > 0.1)
            {
                if (Mathf.Sign(direction.y) < 0)
                {
                    direction = new Vector2(0,-1);
                }
                else
                {
                    direction = new Vector2(0, 1);
                }
                SetRigidbodyEnable(false);
                transform.Translate(direction * m_moveSpeed * Time.deltaTime);
                m_motionState = MotionState.Climb;
            }
            else
            {
                SetRigidbodyEnable(true);
                m_climb_pos = Vector2.zero;
                m_motionState = MotionState.Idle;
            }
            return;
        }

        //移动
        if(m_target_pos != Vector2.zero)
        {
            Vector3 mouse_pos = transform.parent.TransformPoint(m_target_pos);
            Vector2 direction = new Vector2(mouse_pos.x, mouse_pos.y) - new Vector2(transform.position.x,transform.position.y);
            float distance = Mathf.Abs(direction.x);
            if(distance > 0.1)
            {
                direction = direction.normalized;

                if (Mathf.Sign(direction.x) < 0)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                transform.Translate(new Vector2(Mathf.Sign(direction.x)* direction.x, 0) * m_moveSpeed * Time.deltaTime);
                m_motionState = MotionState.Run;
            }
            else
            {
                m_target_pos = Vector2.zero;
                m_motionState = MotionState.Idle;
            }
        }
        else
        {
            m_motionState = MotionState.Idle;
        }
    }

    public void Climb(Vector3 startPos, Vector3 endPos)
    {
        transform.position = startPos;
        m_climb_pos = endPos;
    }

    void SetRigidbodyEnable(bool flag)
    {
        if (flag)
        {
            transform.GetComponent<BoxCollider2D>().enabled = true;
            m_rigidbody.gravityScale = 1;
        }
        else
        {
            transform.GetComponent<BoxCollider2D>().enabled = false;
            m_rigidbody.gravityScale = 0;
        }  
    }

    void UpdateAnimation()
    {
        if(m_motionState == MotionState.Idle)
        {
            m_animPlayer.Play(AnimationData.DATA["hero_idle"]);
        }else if(m_motionState == MotionState.Run)
        {
            m_animPlayer.Play(AnimationData.DATA["hero_run"]);
        }else if(m_motionState == MotionState.Climb)
        {
            m_animPlayer.Play(AnimationData.DATA["hero_jump"]);
        }
    }

    void InitController()
    {
        m_rigidbody.freezeRotation = true;
        //InputManger.Instance.RegistMoveDelegate(Move);
    }

    void Move(Vector3 direction)
    {
        //transform.Translate(direction * m_moveSpeed * Time.deltaTime);
        //m_rigidbody.MovePosition(m_rigidbody.position + new Vector2(direction.x * m_move_speed, direction.y * m_jump_speed * m_jump_flag)* Time.deltaTime);
        //m_rigidbody.AddForce(new Vector2(direction.x * m_moveSpeed, direction.y * m_jumpSpeed * m_jumpFlag));
    }

    void InitAnimation()
    {
        m_animPlayer.Play(AnimationData.DATA["hero_idle"]);
    }

    //void OnCollisionEnter2D(Collision2D collision)
    //{

    //}

    //void OnCollisionStay2D(Collision2D collision)
    //{
    //    Debug.Log("OnCollisionStay");
    //    m_jump_flag = 1;
    //}

    //void OnCollisionExit2D(Collision2D collision)
    //{

    //}

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("OnTriggerEnter2D hero"+ collider.gameObject.name);
        if(collider.gameObject.tag == "Stairs")
        {
            MapStairsNode node = collider.gameObject.transform.parent.GetComponent<MapStairsNode>();
            node.SetButtonAble(true);
        }
        //m_UIHanger.SetAble(true);
    }

    //void OnTriggerStay2D(Collider2D collision)
    //{
    //    
    //}

    void OnTriggerExit2D(Collider2D collider)
    {
        Debug.Log("OnTriggerExit2D hero");
        if (collider.gameObject.tag == "Stairs")
        {
            MapStairsNode node = collider.gameObject.transform.parent.GetComponent<MapStairsNode>();
            node.SetButtonAble(false);
        }
        //m_UIHanger.SetAble(false);
    }

    void InitAttack()
    {
        InputManger.Instance.RegistClickDelegate(OnMouseClick);
    }

    void OnMouseClick(Vector2 clickPosition)
    {
        m_target_pos = clickPosition;
        //激光指示，最后去掉
        MapLaserNode laser = MapNodeManager.Instance.CreateNode<MapLaserNode>("Prefab/Bullet/Laser", transform.parent);
        laser.Init(transform.position, transform.parent.TransformPoint(clickPosition));
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.parent.TransformPoint(clickPosition) - transform.position, 10000f, 1 << LayerMask.NameToLayer("Scene"));
        //if (hit.collider != null)
        //{
        //    laser.Init(transform.position, hit.point);
        //}
        //else
        //{
        //    laser.Init(transform.position, transform.parent.TransformPoint(clickPosition));
        //}
    }

    void OnDestroy()
    {
        InputManger.Instance.UnRegistClickDelegate(OnMouseClick);
        //InputManger.Instance.UnRegistMoveDelegate(Move);
    }
}
