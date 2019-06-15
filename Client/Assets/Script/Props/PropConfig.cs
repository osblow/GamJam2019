using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public enum PropGetAction
{
    NONE = 0,
    SCENE = 1, // 获取之后会显示在场景
    INVENTORY = 2, // 获取之后会显示在背包
}

class PropConfig
{
    public static Dictionary<int, Dictionary<string, object>> S_CONFIGS = new Dictionary<int, Dictionary<string, object>>()
    {
        {101, new Dictionary<string, object>(){
            {"id", "101" },
            {"icon", "Sprite/Prop/hammer2" },
            {"prev_prop", new Dictionary<int, string>(){ {102, ""} } }, // 字符串指向要替换自己的图标或者Prefab路径
            {"how_to_get", PropGetAction.SCENE }, // 
        } },
        {102, new Dictionary<string, object>(){
            {"id", "102" },
            {"icon", "Sprite/Prop/hammer2" },
            {"how_to_get", PropGetAction.SCENE }, // 
        } },
        {103, new Dictionary<string, object>(){
            {"id", "102" },
            {"prev_prop", new Dictionary<int, string>(){ {101, ""} } },
            {"how_to_get", PropGetAction.SCENE }, // 
        } },
    };
}
