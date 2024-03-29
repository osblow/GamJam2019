﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;


public class UIInventory : MonoBehaviour
{ 
    private Transform m_gridRoot;

    private const string c_itemSamplePrefab = "Prefab/UI/Inventory/item";
    private GameObject m_itemSample;

    private Dictionary<int, GameObject> m_items = new Dictionary<int, GameObject>();

    private Transform m_flyItem;


    private void Awake()
    {
        m_gridRoot = transform.Find("Scroll View/Viewport/Content");
        m_itemSample = Resources.Load(c_itemSamplePrefab) as GameObject;

        // 获取道具时的动画
        m_flyItem = transform.Find("itemFly");
        m_flyItem.gameObject.SetActive(false);
    }

    public void AddProp(PropData data)
    {
        string icon = data.GetData<string>("icon");
        if(icon == default(string))
        {
            Debug.LogError("图标未配置");
            return;
        }

        GameObject newItem = Instantiate(m_itemSample);
        newItem.transform.SetParent(m_gridRoot, false);
        SetSprite(newItem.transform.GetChild(0).GetComponent<RawImage>(), icon);
        newItem.SetActive(false);

        // 飞出来的动画
        m_flyItem.gameObject.SetActive(true);
        SetSprite(m_flyItem.GetComponent<RawImage>(), icon);

        m_flyItem.localScale = Vector3.one * 5;
        m_flyItem.DOScale(Vector3.one, 1.0f);

        m_flyItem.position = Vector3.zero;
        // 从grid的参数和item的index，计算一个坐标
        GridLayoutGroup grid = m_gridRoot.transform.GetComponent<GridLayoutGroup>();
        int index = newItem.transform.GetSiblingIndex();
        float offsetX = (index + 0.5f) * grid.cellSize.x + Mathf.Max(index - 1, 0) * grid.spacing.x;
        Vector3 endPos = m_gridRoot.position;// + new Vector3(offsetX, (grid.cellSize.y + grid.spacing.y) * 0.5f, 0);
        m_flyItem.DOMove(endPos, 1.0f).onComplete = delegate() { newItem.SetActive(true); };

        m_items.Add(data.Id, newItem);
    }

    public void RemoveProp(int id)
    {
        if (!m_items.ContainsKey(id))
        {
            return;
        }

        Destroy(m_items[id]);
        m_items.Remove(id);
    }

    private static void SetSprite(RawImage img, string path)
    {
        Texture2D tex = Resources.Load(path) as Texture2D;
        img.texture = tex;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
