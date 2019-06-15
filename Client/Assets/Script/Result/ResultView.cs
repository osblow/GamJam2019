using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultView : MonoBehaviour
{
    public static ResultView Instance;


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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
