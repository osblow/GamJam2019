using System.Collections;
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
        { "hero_run",new AudioInfo("hero_run","Audio/Sound/walk",true) },
        { "hero_jump",new AudioInfo("hero_jump","Audio/Sound/1004_jump") },
        { "car_hit_body",new AudioInfo("car_hit_body","Audio/Sound/car_hit_body") },
        { "car_hit_car",new AudioInfo("car_hit_car","Audio/Sound/car_hit_car") },
        { "time_stretch",new AudioInfo("car_hit_car","Audio/Sound/time_stretch") },
        { "buttons",new AudioInfo("buttons","Audio/Sound/buttons",true) },
        { "car_drive",new AudioInfo("car_drive","Audio/Sound/car_drive",true) },
        { "walk_ladder",new AudioInfo("walk_ladder","Audio/Sound/walk_ladder",true) },
        { "metal_drop",new AudioInfo("metal_drop","Audio/Sound/metal_drop") },
        { "fire_hydrant_water",new AudioInfo("fire_hydrant_water","Audio/Sound/fire_hydrant_water",true) },
        { "error",new AudioInfo("error","Audio/Sound/error") },

        { "bg_1",new AudioInfo("bg_1","Audio/BGM/Family Friendly Halloween",true,0.5f) },
    };
}
