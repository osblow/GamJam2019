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

    private static void ResetSwitch()
    {
        // 判断是否开始消防栓
        if (Inventory.Instance.GetProp(206) != null)
        {
            // 如果开，女主就活了
            ChangeToStage(MainScene.Stage.Stage3);
        }
        else
        {
            // 关，女主死
            ChangeToStage(MainScene.Stage.Start);
        }
    }

    private static void HoldStop()
    {
        // 举起路障
        // 分为三种情况
        bool hasHydrant = Inventory.Instance.GetProp(206) != null;
        bool hasSwitch = Inventory.Instance.GetProp(205) != null;
        if (!hasHydrant)
        {
            // 1. 消防栓没开，左车撞死
            ChangeToStage(MainScene.Stage.BadEnd);
        }
        else if (hasSwitch)
        {
            // 2. 消防栓开了，有红绿灯，右车撞死，女主死
            ChangeToStage(MainScene.Stage.BadEnd2);
        }
        else
        {
            // 3. 消防栓开了，无红绿灯，右车撞死，女主活
            ChangeToStage(MainScene.Stage.GoodEnd);
        }
    }

    private static void UsedGreenLight()
    {
        bool hasDryant = Inventory.Instance.GetProp(206) != null;

        if (hasDryant)
        {
            // 1. 有消防栓，红灯。女主被砸死
            ChangeToStage(MainScene.Stage.Stage4);
        }
        else
        {
            // 2. 无消防栓，红灯。女主被砸死
            ChangeToStage(MainScene.Stage.Stage1);
        }
    }

    private static void UsedHydrant()
    {
        bool usedLight = Inventory.Instance.GetProp(205) != null;

        if (usedLight)
        {
            // 1. 有消防栓，红灯。女主被砸死
            ChangeToStage(MainScene.Stage.Stage4);
        }
        else
        {
            // 2. 有消防栓，绿灯。女主活
            ChangeToStage(MainScene.Stage.Stage3);
        }
    }


    public static Dictionary<int, Dictionary<string, object>> S_CONFIGS = new Dictionary<int, Dictionary<string, object>>()
    {
        {101, new Dictionary<string, object>(){
            {"id", "101" },
            {"icon", "Sprite/Prop/hammer2" },
            {"prev_prop", 102}, // 字符串指向要替换自己的图标或者Prefab路径
            {"how_to_get", PropGetAction.SCENE }, // 
            {"used_comment", "" }
        } },
        {102, new Dictionary<string, object>(){
            {"id", "102" },
            {"icon", "Sprite/Prop/hammer2" },
            {"how_to_get", PropGetAction.SCENE }, // 
        } },
        {103, new Dictionary<string, object>(){
            {"id", "103" },
            {"prev_prop", 101},
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
            {"icon", "Sprite/Prop/img_ganggan" },
            {"how_to_get", PropGetAction.INVENTORY }, // 
            {"used_comment", "好像可以插进井盖的孔里，会发生什么呢？" }
        } },
        // 第一个井盖, 要与第一个撬棍组合
        {202, new Dictionary<string, object>(){
            {"id", "202" },
            {"icon", "Sprite/Prop/hammer2" },
            {"used_icon", "Sprite/Prop/lid_with_bar" }, // 使用结束后替换的图标（插了撬棍的井盖）
            {"prev_prop", 201}, // 字符串指向要替换自己的图标或者Prefab路径
            {"how_to_get", PropGetAction.SCENE }, // 
            {"prev_comment", "井盖上有个孔" },
            {"used_comment", "哎哟哎哟，车来啦！ 咔！~~~" }
        } },
        // 第二个撬棍
        {203, new Dictionary<string, object>(){
            {"id", "203" },
            {"icon", "Sprite/Prop/img_qiaogun" },
            {"how_to_get", PropGetAction.INVENTORY }, // 
            {"used_comment", "这根虽然没有之前那么粗，但是好像也可以用来撬开东西" }
        } },
        // 第二个井盖, 要与第二个撬棍组合
        {204, new Dictionary<string, object>(){
            {"id", "204" },
            {"prev_prop", 203}, // 字符串指向要替换自己的图标或者Prefab路径
            {"how_to_get", PropGetAction.SCENE }, // 
            {"anim_type", PropAnimationType.ROTATION },
            {"anim_rot", -160f },
            {"prev_comment", "还有一个井盖" },
            {"used_comment", "咦？又有一个梯子，下面有什么呢？" }
        } },
        // 红绿灯开关
        {205, new Dictionary<string, object>(){
            {"id", "205" },
            {"how_to_get", PropGetAction.SCENE }, // 
            //{"used_icon", "Sprite/Prop/hammer3" }, // 使用结束后替换的图标（开关状态转换）
            //{"associated_obj_used_icon", "Sprite/Prop/hammer3" }, // 所关联物体在自己使用结束后替换图标(地面上的红绿灯)
            {"used_action",  new PropAction{Action=delegate(){ UsedGreenLight(); } } },
            {"reset_action",  new PropAction{Action=delegate(){ ResetSwitch(); } } },
            {"reset_comment", "又变回绿灯了，小美快过去！不然会被钢管砸到！！！" },
            {"used_comment", "嗯。。。变成红灯了，小美就不会过马路，也不会被车撞到了" }
        } },
        // 消防栓
        {206, new Dictionary<string, object>(){
            {"id", "206" },
            {"prev_prop", 208}, // 字符串指向要替换自己的图标或者Prefab路径
            {"how_to_get", PropGetAction.SCENE }, // 
            //{"used_icon", "Sprite/Prop/hammer3" }, // 使用结束后替换的图标（开关状态转换）
            //{"associated_obj_used_icon", "Sprite/Prop/hammer3" }, // 所关联物体在自己使用结束后替换图标(地面上的红绿灯)
            {"used_action",  new PropAction{Action=delegate(){ UsedHydrant(); } } },
            {"anim_type", PropAnimationType.ROTATION },
            {"anim_rot", 359f },
            {"prev_comment", "已经锈死了，必须找一个扳手" },
            {"used_comment", "消防栓在喷水，那辆车一定会停下来。但..." }
        } },
        // 路障，应用时有偏移
        {207, new Dictionary<string, object>(){
            {"id", "207" },
            {"how_to_get", PropGetAction.SCENE }, // 
            {"used_action",  new PropAction{Action=delegate(){ HoldStop(); } } },
            {"anim_type", PropAnimationType.ROTATION },
            {"anim_rot", -72f },
            {"used_comment", "嘿嘿，告诉你禁止通行！\n哎，哎，哎呀。这路牌怎么站不住呀，赶紧扶好~~" }
        } },
        // 扳手
        {208, new Dictionary<string, object>(){
            {"id", "208" },
            {"icon", "Sprite/Prop/img_banshou" },
            {"how_to_get", PropGetAction.INVENTORY }, // 
            {"used_comment", "去下面试试？" }
        } },
        // 心心
        {209, new Dictionary<string, object>(){
            {"id", "209" },
            {"icon", "Sprite/Prop/use_love" },
            {"how_to_get", PropGetAction.SCENE }, // 
            {"used_action",  new PropAction{Action=delegate(){ ChangeToStage(MainScene.Stage.NormalEnd); } } },
            {"used_comment", "小美，我给你讲一个王子困在时间循环里，最终救下公主的故事吧。" }
        } },
    };
}
