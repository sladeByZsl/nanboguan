using UnityEngine;
using UnityEngine.UI;
using TEngine;

namespace GameLogic
{
    [Window(UILayer.UI)]
    public class Level3 : UIWindow
    {
        #region 脚本工具生成的代码
        private Button m_btnRight;
        private Button m_btnLeft;
        private Text m_text1;
        private Text m_text2;
        private Text m_text3;
        protected override void ScriptGenerator()
        {
            m_btnRight = FindChildComponent<Button>("Bg/m_btnRight");
            m_btnLeft = FindChildComponent<Button>("Bg/m_btnLeft");
            m_text1 = FindChildComponent<Text>("m_text1");
            m_text2 = FindChildComponent<Text>("m_text2");
            m_text3 = FindChildComponent<Text>("m_text3");
            m_btnRight.onClick.AddListener(OnClickRightBtn);
            m_btnLeft.onClick.AddListener(OnClickLeftBtn);
        }
        #endregion

        #region 事件
        private void OnClickRightBtn()
        {
            GameModule.UI.HideUI<Level3>();
            GameModule.UI.ShowUI<Level4>();
        }
        private void OnClickLeftBtn()
        {
            GameModule.UI.HideUI<Level3>();
            GameModule.UI.ShowUI<Level2>();
        }
        #endregion

    }
}