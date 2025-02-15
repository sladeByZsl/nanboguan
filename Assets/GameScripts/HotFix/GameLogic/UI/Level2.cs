using UnityEngine;
using UnityEngine.UI;
using TEngine;


namespace GameLogic
{
    [Window(UILayer.UI)]
    class Level2 : UIWindow
    {
        #region 脚本工具生成的代码

        private Button m_btnRight;
        private Button m_btnLeft;
        private Button m_btnItem1;
        private Button m_btnItem2;
        private Button m_btnItem3;
        private Button m_btnBottle;

        protected override void ScriptGenerator()
        {
            m_btnRight = FindChildComponent<Button>("Bg/m_btnRight");
            m_btnLeft = FindChildComponent<Button>("Bg/m_btnLeft");
            m_btnItem1 = FindChildComponent<Button>("Bg/m_btnItem1");
            m_btnItem2 = FindChildComponent<Button>("Bg/m_btnItem2");
            m_btnItem3 = FindChildComponent<Button>("Bg/m_btnItem3");
            m_btnBottle = FindChildComponent<Button>("Bg/m_btnBottle");
            m_btnRight.onClick.AddListener(OnClickRightBtn);
            m_btnLeft.onClick.AddListener(OnClickLeftBtn);
            m_btnItem1.onClick.AddListener(OnClickItem1Btn);
            m_btnItem2.onClick.AddListener(OnClickItem2Btn);
            m_btnItem3.onClick.AddListener(OnClickItem3Btn);
            m_btnBottle.onClick.AddListener(OnClickBottleBtn);
        }

        #endregion

        #region 事件

        private void OnClickRightBtn()
        {
            GameModule.UI.HideUI<Level2>();
            GameModule.UI.ShowUI<Level3>();
        }

        private void OnClickLeftBtn()
        {
            GameModule.UI.HideUI<Level2>();
            GameModule.UI.ShowUI<Level1>();
        }

        private void OnClickItem1Btn()
        {
        }

        private void OnClickItem2Btn()
        {
        }

        private void OnClickItem3Btn()
        {
        }

        private void OnClickBottleBtn()
        {
        }

        #endregion
    }
}