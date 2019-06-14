using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropBase : MonoBehaviour
{
    public int PropId;

    public bool ShowByDefault = true; // 场景初始化时是否直接显示

    public PropData PropData; // 从id初始化

    
    public void OnPropUsed()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        // 显示操作按钮
    }

    public void OnTriggerExit(Collider other)
    {
        // 隐藏操作按钮
    }

    private void Awake()
    {
        // 初始化数据
        PropData = new PropData(PropId);

        // 初始化显示状态
        gameObject.SetActive(ShowByDefault);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
