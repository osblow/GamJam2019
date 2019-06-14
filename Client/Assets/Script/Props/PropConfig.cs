using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public enum PropGetAction
{
    SCENE = 1,
    INVENTORY = 2,
}

class PropConfig
{
    private static Dictionary<int, Dictionary<string, object>> S_CONFIGS = new Dictionary<int, Dictionary<string, object>>()
    {
        {101, new Dictionary<string, object>(){
            {"id", "101" },
            {"icon", "textures/101.png" },
            {"prev_prop", new Dictionary<int, string>(){ {102, ""} } }, // 字符串指向要替换自己的图标或者Prefab路径
            {"how_to_get", PropGetAction.SCENE }, // 
        } },
    };
}
