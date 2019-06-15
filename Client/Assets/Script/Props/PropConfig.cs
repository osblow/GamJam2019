using System;
using System.Collections.Generic;


public enum PropGetAction
{
    NONE = 0,
    SCENE = 1, // 获取之后会显示在场景
    INVENTORY = 2, // 获取之后会显示在背包
}


public enum PropAnimationType
{
    NONE = 0,
    OFFSET = 1,
    ROTATION = 2,
}



struct PropAction
{
    public Action Action;
}

class PropConfig
{
    private static void ChangeToStage(MainScene.Stage stage)
    {
        UnityEngine.Debug.Log("change state to " + stage);
        ((MainScene)SceneManager.Instance.CurScene).TargetStage = stage;
    }



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
            {"id", "103" },
            {"prev_prop", new Dictionary<int, string>(){ {101, ""} } },
            {"how_to_get", PropGetAction.SCENE }, // 
        } },
        {104, new Dictionary<string, object>(){
            {"id", "104" },
            {"how_to_get", PropGetAction.SCENE }, // 
            {"anim_type", PropAnimationType.ROTATION },
            {"anim_rot", -160f },
            {"used_action",  new PropAction{Action=delegate(){ ChangeToStage(MainScene.Stage.Stage1); } } },
        } },
        {105, new Dictionary<string, object>(){
            {"id", "105" },
            {"icon", "Sprite/Prop/hammer3" },
            {"how_to_get", PropGetAction.SCENE }, // 
            //{"used_icon", "Sprite/Prop/hammer3" }, // 使用结束后替换的图标
            {"associated_obj_used_icon", "Sprite/Prop/hammer3" } // 所关联物体在自己使用结束后替换图标。todo: 序列帧
        } },
    };
}
