using UnityEngine;
using UnityEngine.UI;
using TEngine;

namespace GameLogic
{
    [Window(UILayer.UI)]
    class Level2 : UIWindow
    {
        #region 脚本工具生成的代码
        private Button m_btnBottle;
        private Button m_btnRight;
        private Button m_btnLeft;
        protected override void ScriptGenerator()
        {
            m_btnBottle = FindChildComponent<Button>("m_btnBottle");
            m_btnRight = FindChildComponent<Button>("m_btnRight");
            m_btnLeft = FindChildComponent<Button>("m_btnLeft");
            m_btnBottle.onClick.AddListener(OnClickBottleBtn);
            m_btnRight.onClick.AddListener(OnClickRightBtn);
            m_btnLeft.onClick.AddListener(OnClickLeftBtn);
        }
        #endregion

        #region 事件
        private void OnClickBottleBtn()
        {
        }
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
        #endregion

    }
}