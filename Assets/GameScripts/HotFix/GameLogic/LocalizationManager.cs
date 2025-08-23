using System.Collections;
using System.Collections.Generic;
using GameBase;
using TEngine;
using UnityEngine;

public class LocalizationManager : Singleton<LocalizationManager>
{
    public Language language;
    public LocalizationManager()
    {
        int local_language= PlayerPrefs.GetInt("Language", 1);
        if (local_language == 1)
        {
            language=Language.English;
        }
        else if (local_language == 2)
        {
            language=Language.ChineseSimplified;
        }
        else
        {
            language=Language.ChineseTraditional;
        }
    }
    
    
    
    public string GetText(string key)
    {
        var data = ConfigSystem.Instance.Tables.TbI18L.GetOrDefault(key);
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
