using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    public class Global
    {
        public static int TRIGGER_TAG_1 = LayerMask.NameToLayer("TriggerZone1");
        public static int TRIGGER_TAG_2 = LayerMask.NameToLayer("TriggerZone2");
        public static int TRIGGER_TAG_3 = LayerMask.NameToLayer("TriggerZone3");
        public static int TRIGGER_TAG_4 = LayerMask.NameToLayer("TriggerZone4");
        public static int TRIGGER_TAG_5 = LayerMask.NameToLayer("TriggerZone5");
        public static int TRIGGER_TAG_6 = LayerMask.NameToLayer("TriggerZone6");

        public static string Key_level1_tips = "level1_tips";
        public static string Key_level2_tips = "level2_tips";
        public static string Key_level2_liekai_tips = "level2_tips_liekai";
        public static string Key_level2_tips_wrong_tips = "level2_tips_wrong";
        
        public static string Key_item_new = "Item_New";

        public static string UI = "UI";
        public static string UI_TOP = "UITop";


        public const int Cfg_Item_Gloves = 10000;
        public const int Cfg_Item_Sticker = 10001;
        public const int Cfg_Item_Origin = 10002;
        public const int Cfg_Item_Doorknob = 10003;//门扳手
        public const int Cfg_Item_Brick = 10004;//砖头



        public static bool Level2Right = false;
    }
}