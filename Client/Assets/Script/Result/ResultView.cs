using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultView : MonoBehaviour
{
    public static ResultView Instance;
    public Text TipsText;


    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void OnClickConfirm()
    {
        Debug.Log("restart");

        // 重新加载一次场景
        Scene scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene.name);
    }

    public void SetTips(string tips)
    {
        TipsText.text = tips;
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
