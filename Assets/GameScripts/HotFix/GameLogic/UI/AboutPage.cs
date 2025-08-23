using UnityEngine;
using UnityEngine.UI;
using TEngine;

namespace GameLogic
{
    [Window(UILayer.UI,hideTimeToClose:0)]
    class AboutPage : UIWindow
    {
        #region 脚本工具生成的代码
        private Button m_btnBack;
        private Button m_btnContact;
        private Button m_btnPrivacy;
        protected override void ScriptGenerator()
        {
            m_btnBack = FindChildComponent<Button>("Bg/m_btnBack");
            m_btnContact = FindChildComponent<Button>("Bg/m_btnContact");
            m_btnPrivacy = FindChildComponent<Button>("Bg/m_btnPrivacy");
            m_btnBack.onClick.AddListener(OnClickBackBtn);
            m_btnContact.onClick.AddListener(OnClickContactBtn);
            m_btnPrivacy.onClick.AddListener(OnClickPrivacyBtn);
        }
        #endregion

        #region 事件
        private void OnClickBackBtn()
        {
            GameModule.Audio.Play(TEngine.AudioType.UISound,"Menu1A");
            GameModule.UI.HideUI<AboutPage>();
            GameModule.UI.ShowUI<StartPage>();
        }
        private void OnClickContactBtn()
        {
        }
        private void OnClickPrivacyBtn()
        {
        }
        #endregion

    }
}