using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHanger : MonoBehaviour
{
    Camera m_camScene;
    Camera m_camUI;
    GameObject m_UICanvas;
    GameObject m_node;  //挂载实体
    GameObject m_root;  //UI根节点
    GameObject m_hangObj;
    bool m_isInit = false;
    float m_offset = 0;

    void Awake()
    {
        m_UICanvas = GameObject.Find("UICanvas/UI");
        m_camScene = GameObject.Find("MainCanvas/Main/SceneCamera").GetComponent<Camera>();
        m_camUI = GameObject.Find("UICamera").GetComponent<Camera>();
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        if (m_isInit)
        {
            Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(m_camScene, m_node.transform.position);
            Vector2 pos; 
            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_UICanvas.GetComponent<RectTransform>(), screenPos, m_camUI,out pos);
            m_root.transform.localPosition = new Vector3(pos.x,pos.y + m_offset, 0);
        }
    }

    public void Init(GameObject node,string path, float offset = 10f)
    {
        if(m_root == null)
        {
            m_root = new GameObject("UIHanger");
            m_root.transform.SetParent(m_UICanvas.GetComponent<RectTransform>());
            //Util.GetOrAddComponent<Image>(m_root);
            m_root.transform.localScale = new Vector3(1, 1, 1);

            m_hangObj = GameObject.Instantiate(Resources.Load(path), m_root.transform) as GameObject;
        }
        m_offset = offset;
        m_node = node;
        m_isInit = true;
    }

    public void SetAble(bool flag)
    {
        m_root.SetActive(flag);
    }

    public void SetName(string name)
    {
        m_hangObj.transform.Find("TxtName").GetComponent<Text>().text = name;
    }

    void OnDestroy(){
        if (m_root != null)
        {
            Destroy(m_root);
        }
   }
}
