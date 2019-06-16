using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapHydrantNode : MapNode
{
    AnimationPlayer m_animPlayer;
    GameObject m_water;

    void Start()
    {
        m_water = transform.Find("ImgWaterAnim").gameObject;
        m_animPlayer = Util.GetOrAddComponent<AnimationPlayer>(m_water);
    }

    public void EnableWaterAnim()
    {
        m_water.GetComponent<Image>().enabled = true;
        m_animPlayer.Play(AnimationData.DATA["pengshui"]);
        AudioManager.Instance.PlaySoundByGO(AudioData.DATA["fire_hydrant_water"], gameObject);
    }
}
