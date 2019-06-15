using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance; //单例

    public SceneBase CurScene
    {
        get { return m_curScene; }
    }
    SceneBase m_curScene = null;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (m_curScene != null)
        {
            m_curScene.Run();
        }
    }

    public void ChangeScene<T>() where T : SceneBase
    {
        Assembly assembly = Assembly.GetExecutingAssembly(); // 获取当前程序集 
        m_curScene = (T)assembly.CreateInstance(typeof(T).ToString());

        m_curScene.Init();
    }
}
