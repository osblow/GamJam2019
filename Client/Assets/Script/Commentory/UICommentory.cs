using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICommentory : MonoBehaviour
{
    public static UICommentory Instance;

    public Text Tips;

    private const int MAX_CHAR_NUM = 50;
    private Queue<string> m_tipsQueue = new Queue<string>();

    private const float TIPS_UPDATE_INTERVAL = 2; // 两秒更新一次
    private float m_timer = float.MaxValue;

    private void Awake()
    {
        Instance = this;
    }

    private string m_lastTips = "";
    public void SetTips(string tips)
    {
        // 防止意外情况下重复调用，造成同一句话不停循环
        if(tips == m_lastTips)
        {
            return;
        }

        if(tips.Length <= MAX_CHAR_NUM)
        {
            m_tipsQueue.Enqueue(tips);
        }
        else
        {
            // 文字长度超出限制，切断
            int length = 0;
            char[] temp;
            while(length < tips.Length)
            {
                int tempLen = Mathf.Min(MAX_CHAR_NUM, tips.Length-length);
                temp = new char[tempLen];
                tips.CopyTo(length, temp, 0, tempLen);
                m_tipsQueue.Enqueue(new string(temp));

                length += MAX_CHAR_NUM;
            }
        }

        m_lastTips = tips;

        // 
        if (m_timer <= 0) m_timer = TIPS_UPDATE_INTERVAL; // 只要不是转换过程中，就立即播放
    }

    private void Update()
    {
        if(m_tipsQueue.Count <= 0)
        {
            return;
        }

        m_timer += Time.deltaTime;
        if(m_timer >= TIPS_UPDATE_INTERVAL)
        {
            string tips = m_tipsQueue.Dequeue();
            Tips.text = tips;
            m_timer = 0;
        }
    }
}
