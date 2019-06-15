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
        // 第一个撬棍
        {201, new Dictionary<string, object>(){
            {"id", "201" },
            {"how_to_get", PropGetAction.INVENTORY }, // 
        } },
        // 第一个井盖, 要与第一个撬棍组合
        {202, new Dictionary<string, object>(){
            {"id", "202" },
            {"used_icon", "Sprite/Prop/hammer3" }, // 使用结束后替换的图标（插了撬棍的井盖）
            {"prev_prop", new Dictionary<int, string>(){ {201, ""} } }, // 字符串指向要替换自己的图标或者Prefab路径
            {"how_to_get", PropGetAction.SCENE }, // 
        } },
        // 第二个撬棍
        {203, new Dictionary<string, object>(){
            {"id", "203" },
            {"how_to_get", PropGetAction.INVENTORY }, // 
        } },
        // 第二个井盖, 要与第二个撬棍组合
        {204, new Dictionary<string, object>(){
            {"id", "202" },
            {"prev_prop", new Dictionary<int, string>(){ {203, ""} } }, // 字符串指向要替换自己的图标或者Prefab路径
            {"how_to_get", PropGetAction.SCENE }, // 
            {"anim_type", PropAnimationType.ROTATION },
            {"anim_rot", -160f },
        } },
        // 红绿灯开关
        {205, new Dictionary<string, object>(){
            {"id", "205" },
            {"how_to_get", PropGetAction.SCENE }, // 
            {"used_icon", "Sprite/Prop/hammer3" }, // 使用结束后替换的图标（开关状态转换）
            {"associated_obj_used_icon", "Sprite/Prop/hammer3" } // 所关联物体在自己使用结束后替换图标(地面上的红绿灯)
        } },
        // 消防栓
        {206, new Dictionary<string, object>(){
            {"id", "206" },
            {"how_to_get", PropGetAction.SCENE }, // 
            {"used_icon", "Sprite/Prop/hammer3" }, // 使用结束后替换的图标（开关状态转换）
            {"associated_obj_used_icon", "Sprite/Prop/hammer3" } // 所关联物体在自己使用结束后替换图标(地面上的红绿灯)
        } },
        // 路障，应用时有偏移
        {207, new Dictionary<string, object>(){
            {"id", "207" },
            {"how_to_get", PropGetAction.SCENE }, // 
            {"anim_type", PropAnimationType.OFFSET },
            {"anim_rot", new UnityEngine.Vector3(0, 50, 0) },
        } },
        // 扳手
        {208, new Dictionary<string, object>(){
            {"id", "208" },
            {"how_to_get", PropGetAction.INVENTORY }, // 
        } },
    };
}
