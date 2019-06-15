using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIHangerPropBtn : UIHanger
{
    private Button m_button;


    public override void Init(string hangObjPrefab, GameObject node, float offset = 10)
    {
        base.Init(hangObjPrefab, node, offset);
        m_button = m_hangObj.GetComponent<Button>();
    }

    public void SetIcon(string iconPath)
    {
        if(iconPath == null)
        {
            return;
        }

        RawImage img = m_button.transform.GetChild(0).GetComponent<RawImage>();
        Texture newTex = Instantiate<Texture>(Resources.Load(iconPath) as Texture);
        img.texture = newTex;
        //Material mat = new Material(img.material);
        //mat.mainTexture = newTex;
        //img.material = mat;
    }

    public void RegisterOnClick(UnityEngine.Events.UnityAction callback)
    {
        m_button.onClick.AddListener(callback);
    }
}
