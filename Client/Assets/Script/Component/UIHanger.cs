using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHanger : MonoBehaviour
{
    public GameObject HangObject
    {
        get
        {
            return m_hangObj;
        }
    }

    protected Camera m_camScene;
    protected Camera m_camUI;
    protected GameObject m_UICanvas;
    protected GameObject m_node;  //挂载实体
    protected GameObject m_root;  //UI根节点
    protected GameObject m_hangObj;
    protected bool m_isInit = false;
    protected float m_offset = 0;

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

    public virtual void Init(string hangObjPrefab, GameObject node,float offset = 10f)
    {
        if(m_root == null)
        {
            m_root = new GameObject("UIHanger");
            m_root.transform.SetParent(m_UICanvas.GetComponent<RectTransform>());
            //Util.GetOrAddComponent<Image>(m_root);
            m_root.transform.localScale = new Vector3(1, 1, 1);

            m_hangObj = GameObject.Instantiate(Resources.Load("Prefab/UI/Hanger/HangerNameBlood"), m_root.transform) as GameObject;
            m_hangObj = GameObject.Instantiate(Resources.Load(hangObjPrefab), m_root.transform) as GameObject;
        }
        m_offset = offset;
        m_node = node;
        m_isInit = true;
    }
    

    void OnDestroy(){
        if (m_root != null)
        {
            Destroy(m_root);
        }
   }
}
