﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInfo
{
    public string audioName;
    public string resName;
    public bool loop;
    public float volume;

    public AudioInfo(string name, string res, bool isloop = false, float vol =1f)
    {
        audioName = name;
        resName = res;
        loop = isloop;
        volume = vol;
    }
}

public class AudioData
{
    public static Dictionary<string, AudioInfo> DATA = new Dictionary<string, AudioInfo>{
        { "hero_run",new AudioInfo("hero_idle","Audio/Sound/footstep_normal_1",true) },
        { "hero_jump",new AudioInfo("hero_jump","Audio/Sound/1004_jump") },
        { "bg_1",new AudioInfo("bg_1","Audio/BGM/Family Friendly Halloween",true,0.5f) },
    };
}