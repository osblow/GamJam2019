using UnityEngine;
using UnityEngine.UI;

public class StateWelcome : StateBase
{
    Object m_pre = null;
    GameObject m_panel = null;
    public override void Start()
    {
        Transform transCanvas = GameObject.Find("UICanvas/Top").transform;
        m_pre = Resources.Load("Prefab/UI/Welcome/WekcomePanel");
        m_panel = GameObject.Instantiate(m_pre, transCanvas) as GameObject;

        m_panel.transform.Find("BtnStart").GetComponent<Button>().onClick.AddListener(OnClickStart);
    }

    public override void Run()
    {
        
    }

    void OnClickStart()
    {
        Debug.Log("game start");
        ClientStateManager.Instance.ChangeState<StatePlay>();
    }

    public override void End()
    {
        GameManager.Destroy(m_panel);
    }
}
