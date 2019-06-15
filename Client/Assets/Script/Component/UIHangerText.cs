using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHangerText : UIHanger
{
    public void SetName(string name)
    {
        m_hangObj.transform.Find("TxtName").GetComponent<Text>().text = name;
    }
}
