using UnityEngine;
using UnityEngine.UI;
using TEngine;

namespace GameLogic
{
    [Window(UILayer.UI,hideTimeToClose:0)]
    class StartPage : UIWindow
    {
        #region 脚本工具生成的代码
        private Button m_btnStartGame;
        private Button m_btnSettings;
        private Button m_btnAbout;
        private Button m_btnZan;
        private Button m_btnNoAd;
        private Button m_btnChengjiu;
        protected override void ScriptGenerator()
        {
            m_btnStartGame = FindChildComponent<Button>("Bg/m_btnStartGame");
            m_btnSettings = FindChildComponent<Button>("Bg/m_btnSettings");
            m_btnAbout = FindChildComponent<Button>("Bg/m_btnAbout");
            m_btnZan = FindChildComponent<Button>("Bg/m_btnZan");
            m_btnNoAd = FindChildComponent<Button>("Bg/m_btnNoAd");
            m_btnChengjiu = FindChildComponent<Button>("Bg/m_btnChengjiu");
            m_btnStartGame.onClick.AddListener(OnClickStartGameBtn);
            m_btnSettings.onClick.AddListener(OnClickSettingsBtn);
            m_btnAbout.onClick.AddListener(OnClickAboutBtn);
            m_btnZan.onClick.AddListener(OnClickZanBtn);
            m_btnNoAd.onClick.AddListener(OnClickNoAdBtn);
            m_btnChengjiu.onClick.AddListener(OnClickChengjiuBtn);
        }

        #endregion

        #region 事件
        
        
        private void OnClickChengjiuBtn()
        {
            
        }

        private void OnClickNoAdBtn()
        {
            
        }

        private void OnClickZanBtn()
        {
            
        }

        private void OnClickAboutBtn()
        {
            Debug.Log("About Click");
            GameModule.Audio.Play(TEngine.AudioType.UISound,"Menu1A");
            GameModule.UI.HideUI<StartPage>();
            GameModule.UI.ShowUI<AboutPage>();
        }

        private void OnClickStartGameBtn()
        {
            GameModule.Audio.Play(TEngine.AudioType.UISound,"Menu1A");
            GameModule.UI.HideUI<StartPage>();
            GameModule.UI.ShowUI<Level1>();
            GameModule.UI.ShowUI<LevelCommon>();
            GameModule.UI.ShowUI<LevelTips>();
        }
        private void OnClickSettingsBtn()
        {
            GameModule.Audio.Play(TEngine.AudioType.UISound,"Menu1A");
            GameModule.UI.HideUI<StartPage>();
            GameModule.UI.ShowUI<SettingPanel>();
        }
        #endregion


        protected override void OnRefresh()
        {
            base.OnRefresh();
            if (PlayerPrefs.GetInt("Music", 1) == 1)
            {
                GameModule.Audio.MusicEnable = true;
            }
            else
            {
                GameModule.Audio.MusicEnable = false;
            }

            if (PlayerPrefs.GetInt("Sound", 1) == 1)
            {
                GameModule.Audio.SoundEnable = true;
                GameModule.Audio.UISoundEnable = true;
            }
            else
            {
                GameModule.Audio.SoundEnable = false;
                GameModule.Audio.UISoundEnable = false;
            }
        }
    }
}