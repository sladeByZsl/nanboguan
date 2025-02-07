using System.Collections;
using System.Collections.Generic;
using GameBase;
using TEngine;
using UnityEngine;

public class LocalizationManager : Singleton<LocalizationManager>
{
    private Language language;
    public LocalizationManager()
    {
        language=Language.English;
    }
    
    public string GetText(string key)
    {
        var data = ConfigSystem.Instance.Tables.TbI18L.Get(key);
        if (data == null) return key;

        switch (language)
        {
            case Language.ChineseSimplified:
                return data.OriginText;
            case Language.ChineseTraditional:
                return data.TextTw;
            case Language.English:
                return data.TextEn;
            default:
                return data.OriginText;
        }
    }
}
