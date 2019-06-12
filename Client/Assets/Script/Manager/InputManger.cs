﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManger : MonoBehaviour
{
    public static InputManger Instance; //单例

    public delegate void MoveDelegate(Vector3 direction);
    MoveDelegate m_moveDelegate;

    public delegate void ClickDelegate(Vector2 position);
    ClickDelegate m_clickDelegate;

    public bool m_jumpMode = true;  //弹跳模式

    Camera m_camScene;
    RectTransform m_mainParent;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        m_camScene = GameObject.Find("MainCanvas/Main/SceneCamera").GetComponent<Camera>();
        m_mainParent = GameObject.Find("MainCanvas/Main").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckMoveKey();
        CheckMouseClick();
    }

    public void RegistMoveDelegate(MoveDelegate moveDel)
    {
        m_moveDelegate += moveDel;
    }

    public void RegistClickDelegate(ClickDelegate clickDel)
    {
        m_clickDelegate += clickDel;
    }

    //Translate移动控制函数
    void CheckMoveKey()
    {
        Vector3 direction = Vector3.zero;
        if (m_jumpMode)
        {
            if (Input.GetKeyDown(KeyCode.W) | Input.GetKeyDown(KeyCode.UpArrow)) //前
            {
                direction.y = 1;
            }
            if (Input.GetKeyDown(KeyCode.S) | Input.GetKeyDown(KeyCode.DownArrow)) //后
            {
                //direction.y = -1;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.UpArrow)) //前
            {
                direction.y = 1;
            }
            if (Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.DownArrow)) //后
            {
                direction.y = -1;
            }
        }
        
        if (Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.LeftArrow)) //左
        {
            direction.x = -1;
        }
        if (Input.GetKey(KeyCode.D) | Input.GetKey(KeyCode.RightArrow)) //右
        {
            direction.x = 1;
        }
        if (direction != Vector3.zero)
        {
            m_moveDelegate(direction);
        }
        
    }

    void CheckMouseClick()
    {
        Vector2 vecMouse = Vector2.zero;
        bool click = false;
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Input.mousePosition" + Input.mousePosition);
            
            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_mainParent, Input.mousePosition, m_camScene, out vecMouse);
            //Debug.Log("Input.mousePosition screen" + vecMouse);
            click = true;
        }

        if (Input.GetMouseButtonDown(1))
        {

        }

        if (Input.GetMouseButtonDown(2))
        {

        }
        if (m_clickDelegate != null && click == true)
        {
            m_clickDelegate(vecMouse);
        }
    }
}
