using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManger : MonoBehaviour
{
    public static InputManger Instance; //单例

    public delegate void MoveDelegate(Vector3 direction);
    MoveDelegate m_moveDelegate;

    public bool m_jumpMode = true;  //弹跳模式

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveControlByTranslate();
    }

    public void RegistMoveDelegate(MoveDelegate moveDel)
    {
        m_moveDelegate += moveDel;
    }

    //Translate移动控制函数
    void MoveControlByTranslate()
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
        if(direction != Vector3.zero)
        {
            m_moveDelegate(direction);
        }
        
    }
}
