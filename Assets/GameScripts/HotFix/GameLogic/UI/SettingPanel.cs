using UnityEngine;
using UnityEngine.UI;
using TEngine;

namespace GameLogic
{
    [Window(UILayer.UI,hideTimeToClose:0)]
    class SettingPanel : UIWindow
    {
        #region 脚本工具生成的代码
        private Button m_btnBack;
        private Button m_btnMusicOn;
        private Button m_btnMusicOff;
        private Button m_btnSoundOn;
        private Button m_btnSoundOff;
        private Button m_btnEnglish;
        private Button m_btnZHCN;
        private Button m_btnZH;
        protected override void ScriptGenerator()
        {
            m_btnBack = FindChildComponent<Button>("Bg/m_btnBack");
            m_btnMusicOn = FindChildComponent<Button>("Bg/Music/m_btnMusicOn");
            m_btnMusicOff = FindChildComponent<Button>("Bg/Music/m_btnMusicOff");
            m_btnSoundOn = FindChildComponent<Button>("Bg/Sound/m_btnSoundOn");
            m_btnSoundOff = FindChildComponent<Button>("Bg/Sound/m_btnSoundOff");
            m_btnEnglish = FindChildComponent<Button>("Bg/m_btnEnglish");
            m_btnZHCN = FindChildComponent<Button>("Bg/m_btnZHCN");
            m_btnZH = FindChildComponent<Button>("Bg/m_btnZH");
            m_btnBack.onClick.AddListener(OnClickBackBtn);
            m_btnMusicOn.onClick.AddListener(OnClickMusicOnBtn);
            m_btnMusicOff.onClick.AddListener(OnClickMusicOffBtn);
            m_btnSoundOn.onClick.AddListener(OnClickSoundOnBtn);
            m_btnSoundOff.onClick.AddListener(OnClickSoundOffBtn);
            m_btnEnglish.onClick.AddListener(OnClickEnglishBtn);
            m_btnZHCN.onClick.AddListener(OnClickZHCNBtn);
            m_btnZH.onClick.AddListener(OnClickZHBtn);
        }
        #endregion

        #region 事件

        private void OnClickBackBtn()
        {
            GameModule.Audio.Play(TEngine.AudioType.UISound,"Menu1A");
            GameModule.UI.HideUI<SettingPanel>();
            GameModule.UI.ShowUI<StartPage>();
        }

        private void OnClickMusicOnBtn()
        {
            GameModule.Audio.Play(TEngine.AudioType.UISound,"Menu1A");
            GameModule.Audio.MusicEnable = true;
            PlayerPrefs.SetInt("Music",1);
            GameModule.Audio.Play(TEngine.AudioType.Music,"song18",true);
            ShowMusicOn(true);
        }

        private void ShowMusicOn(bool show)
        {
            GameModule.Audio.Play(TEngine.AudioType.UISound,"Menu1A");
            if (show)
            {
                Color srcColor = m_btnMusicOn.GetComponent<Image>().color;
                m_btnMusicOn.GetComponent<Image>().color = new Color(srcColor.r,srcColor.g,srcColor.b,1);
            
                Color srcColor2 = m_btnMusicOff.GetComponent<Image>().color;
                m_btnMusicOff.GetComponent<Image>().color = new Color(srcColor2.r,srcColor2.g,srcColor2.b,0);
            }
            else
            {
                Color srcColor = m_btnMusicOn.GetComponent<Image>().color;
                m_btnMusicOn.GetComponent<Image>().color = new Color(srcColor.r,srcColor.g,srcColor.b,0);
            
                Color srcColor2 = m_btnMusicOff.GetComponent<Image>().color;
                m_btnMusicOff.GetComponent<Image>().color = new Color(srcColor2.r,srcColor2.g,srcColor2.b,1);
            }
        }

        private void OnClickMusicOffBtn()
        {
            GameModule.Audio.Play(TEngine.AudioType.UISound,"Menu1A");
            GameModule.Audio.MusicEnable = false;
            PlayerPrefs.SetInt("Music",0);
            ShowMusicOn(false);
        }
        private void OnClickSoundOnBtn()
        {
            GameModule.Audio.Play(TEngine.AudioType.UISound,"Menu1A");
            GameModule.Audio.SoundEnable = true;
            GameModule.Audio.UISoundEnable = true;
            PlayerPrefs.SetInt("Sound",1);
            ShowSoundOn(true);
        }
        private void OnClickSoundOffBtn()
        {
            GameModule.Audio.SoundEnable = false;
            GameModule.Audio.UISoundEnable = false;
            GameModule.Audio.Play(TEngine.AudioType.UISound,"Menu1A");
            PlayerPrefs.SetInt("Sound",0);
            ShowSoundOn(false);
        }
        
        private void ShowSoundOn(bool show)
        {
            if (show)
            {
                Color srcColor = m_btnSoundOn.GetComponent<Image>().color;
                m_btnSoundOn.GetComponent<Image>().color = new Color(srcColor.r,srcColor.g,srcColor.b,1);
            
                Color srcColor2 = m_btnSoundOff.GetComponent<Image>().color;
                m_btnSoundOff.GetComponent<Image>().color = new Color(srcColor2.r,srcColor2.g,srcColor2.b,0);
            }
            else
            {
                Color srcColor = m_btnSoundOn.GetComponent<Image>().color;
                m_btnSoundOn.GetComponent<Image>().color = new Color(srcColor.r,srcColor.g,srcColor.b,0);
            
                Color srcColor2 = m_btnSoundOff.GetComponent<Image>().color;
                m_btnSoundOff.GetComponent<Image>().color = new Color(srcColor2.r,srcColor2.g,srcColor2.b,1);
            }
        }

        private void ShowLanguage(int languageType)
        {
            if (languageType==1)
            {
                Color srcColor = m_btnEnglish.GetComponent<Image>().color;
                m_btnEnglish.GetComponent<Image>().color = new Color(srcColor.r,srcColor.g,srcColor.b,1);
                
                Color srcColor1 = m_btnEnglish.GetComponent<Image>().color;
                m_btnZHCN.GetComponent<Image>().color = new Color(srcColor1.r,srcColor1.g,srcColor1.b,0);
                
                Color srcColor2 = m_btnEnglish.GetComponent<Image>().color;
                m_btnZH.GetComponent<Image>().color = new Color(srcColor.r,srcColor.g,srcColor.b,0);
                
            }
            else if (languageType==2)
            {
                Color srcColor = m_btnEnglish.GetComponent<Image>().color;
                m_btnEnglish.GetComponent<Image>().color = new Color(srcColor.r,srcColor.g,srcColor.b,0);
                
                Color srcColor1 = m_btnEnglish.GetComponent<Image>().color;
                m_btnZHCN.GetComponent<Image>().color = new Color(srcColor1.r,srcColor1.g,srcColor1.b,1);
                
                Color srcColor2 = m_btnEnglish.GetComponent<Image>().color;
                m_btnZH.GetComponent<Image>().color = new Color(srcColor.r,srcColor.g,srcColor.b,0);
            }
            else
            {
                Color srcColor = m_btnEnglish.GetComponent<Image>().color;
                m_btnEnglish.GetComponent<Image>().color = new Color(srcColor.r,srcColor.g,srcColor.b,0);
                
                Color srcColor1 = m_btnEnglish.GetComponent<Image>().color;
                m_btnZHCN.GetComponent<Image>().color = new Color(srcColor1.r,srcColor1.g,srcColor1.b,0);
                
                Color srcColor2 = m_btnEnglish.GetComponent<Image>().color;
                m_btnZH.GetComponent<Image>().color = new Color(srcColor.r,srcColor.g,srcColor.b,1);
            }
        }

        private void OnClickEnglishBtn()
        {
            GameModule.Audio.Play(TEngine.AudioType.UISound,"Menu1A");
            PlayerPrefs.SetInt("Language",1);//英语
            LocalizationManager.Instance.SetLan(1);
            ShowLanguage(1);
            
        }
        private void OnClickZHCNBtn()
        {
            GameModule.Audio.Play(TEngine.AudioType.UISound,"Menu1A");
            PlayerPrefs.SetInt("Language",2);//中文简体
            LocalizationManager.Instance.SetLan(2);
            ShowLanguage(2);
        }
        private void OnClickZHBtn()
        {
            GameModule.Audio.Play(TEngine.AudioType.UISound,"Menu1A");
            PlayerPrefs.SetInt("Language",3);//中文繁体
            LocalizationManager.Instance.SetLan(3);
            ShowLanguage(3);
        }
        #endregion

        protected override void OnRefresh()
        {
            base.OnRefresh();
            if (PlayerPrefs.GetInt("Music",1)==1)
            {
                ShowMusicOn(true);
            }
            else
            {
                ShowMusicOn(false);
            }
            
            if (PlayerPrefs.GetInt("Sound",1)==1)
            {
                ShowSoundOn(true);
            }
            else
            {
                ShowSoundOn(false);
            }

            int language= PlayerPrefs.GetInt("Language", 1);
            ShowLanguage(language);
        }
    }
}