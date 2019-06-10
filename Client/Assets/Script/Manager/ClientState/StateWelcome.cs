using UnityEngine;
using UnityEngine.UI;

public class StateWelcome : StateBase
{
    public override void Start()
    {
        Transform transCanvas = GameObject.Find("Canvas/Top").transform;
        Object pre = Resources.Load("Prefab/UI/Welcome/WekcomePanel");
        GameObject panel = GameObject.Instantiate(pre, transCanvas) as GameObject;

        panel.transform.Find("BtnStart").GetComponent<Button>().onClick.AddListener(OnClickStart);
    }

    public override void Run()
    {
        
    }

    void OnClickStart()
    {
        Debug.Log("game start");
    }
}
