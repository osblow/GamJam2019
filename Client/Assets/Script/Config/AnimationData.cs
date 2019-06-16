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
    public string soundName;

    public AnimationInfo(string animName,string res,int start,int end, float time, bool isloop = false,string sound = "")
    {
        animationName = animName;
        resName = res;
        startFrame = start;
        endFrame = end;
        loop = isloop;
        duration = time;
        soundName = sound;
    }
}

public class AnimationData
{
    public static Dictionary<string, AnimationInfo> DATA = new Dictionary<string, AnimationInfo>{
        { "hero_idle",new AnimationInfo("hero_idle","Sprite/Character/test01",0,5,1.5f,true) },
        { "hero_run",new AnimationInfo("hero_idle","Sprite/Character/test01",6,11,1.0f,true,"hero_run") },
        { "hero_jump",new AnimationInfo("hero_jump","Sprite/Character/test02",0,10,0.3f,false,"hero_jump") },
        { "cat_idle",new AnimationInfo("cat_idle","Sprite/Character/cat_anim_2",4,6,0.75f,true) },
        { "cat_run",new AnimationInfo("cat_run","Sprite/Character/cat_anim_1",3,7,1.25f,true,"hero_run") },
        { "cat_operate",new AnimationInfo("cat_operate","Sprite/Character/cat_anim_2",0,3,1f,true) },
        { "cat_climb",new AnimationInfo("cat_climb","Sprite/Character/cat_anim_1",0,2,0.75f,true) },
        { "cat_die",new AnimationInfo("cat_die","Sprite/Character/cat_anim_1",8,8,0.5f,true) },
        { "pengshui",new AnimationInfo("pengshui","Sprite/pengshui",0,5,1f,true) },
    };
}


