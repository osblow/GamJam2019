using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public enum ResultType
{
    BAD = 1,
    GOOD = 2,
    NORMAL = 3,
}

public class ResultView : MonoBehaviour
{
    public static ResultView Instance;
    public Text TipsText;

    private Dictionary<ResultType, GameObject> ResultLabels = new Dictionary<ResultType, GameObject>();


    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);

        ResultLabels.Add(ResultType.BAD, transform.Find("bad_end").gameObject);
        ResultLabels.Add(ResultType.GOOD, transform.Find("good_end").gameObject);
        ResultLabels.Add(ResultType.NORMAL, transform.Find("normal_end").gameObject);
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

    public void SetTips(ResultType type)
    {
        foreach(KeyValuePair<ResultType, GameObject> kval in ResultLabels)
        {
            kval.Value.SetActive(kval.Key == type);
        }
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
