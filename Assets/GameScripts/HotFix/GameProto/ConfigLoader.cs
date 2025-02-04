using Luban;
using GameBase;
using GameConfig;
using TEngine;
using UnityEngine;

/// <summary>
/// 配置加载器。
/// </summary>
public class ConfigLoader : Singleton<ConfigLoader>
{
    private bool _init = false;

    private Tables _tables;

    public Tables Tables
    {
        get
        {
            if (!_init)
            {
                Load();
            }

            return _tables;
        }
    }

    /// <summary>
    /// 加载配置。
    /// </summary>
    public void Load()
    {
        _tables = new Tables(LoadByteBuf);
        _init = true;
    }

    /// <summary>
    /// 加载二进制配置。
    /// </summary>
    /// <param name="file">FileName</param>
    /// <returns>ByteBuf</returns>
    private ByteBuf LoadByteBuf(string file)
    {
        var textAssets = GameModule.Resource.LoadAsset<TextAsset>(file);
        byte[] ret = textAssets.bytes;
        return new ByteBuf(ret);
    }
}

public class ConfigSystem : BaseLogicSys<ConfigSystem>
{
    public Tables Tables => ConfigLoader.Instance.Tables;

    public override bool OnInit()
    {
        return base.OnInit();
    }
}