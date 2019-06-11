using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ClientStateManager : MonoBehaviour
{
    public static ClientStateManager Instance; //单例
    StateBase m_curState = null;    //当前状态

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Debug.Log("ClientStateManager Start");
        ChangeState<StateWelcome>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_curState != null)
        {
            m_curState.Run();
        }
    }

    public void ChangeState<T>() where T: StateBase 
    {
        if(m_curState != null)
        {
            m_curState.End();
        }
        
        Assembly assembly = Assembly.GetExecutingAssembly(); // 获取当前程序集 
        m_curState = (T)assembly.CreateInstance(typeof(T).ToString());

        m_curState.Start();
    }
}
