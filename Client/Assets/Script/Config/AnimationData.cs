using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationInfo
{
    public string animationName;
    public string resName;
    public int startFrame;
    public int endFrame;
    public float duration;
    public bool loop;

    public AnimationInfo(string animName,string res,int start,int end, float time, bool isloop = false)
    {
        animationName = animName;
        resName = res;
        startFrame = start;
        endFrame = end;
        loop = isloop;
        duration = time;
    }
}

public class AnimationData
{
    public static Dictionary<string, AnimationInfo> DATA = new Dictionary<string, AnimationInfo>{
        { "hero_idle",new AnimationInfo("hero_idle","Sprite/Character/test01",0,5,1.5f,true) },
        { "hero_run",new AnimationInfo("hero_idle","Sprite/Character/test01",6,11,1.0f,true) },
        { "hero_jump",new AnimationInfo("hero_jump","Sprite/Character/test02",0,10,0.3f) },
    };
}


