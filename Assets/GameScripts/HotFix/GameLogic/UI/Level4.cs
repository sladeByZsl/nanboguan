using UnityEngine;
using UnityEngine.UI;
using TEngine;

namespace GameLogic
{
    [Window(UILayer.UI)]
    public class Level4 : UIWindow
    {
        #region 脚本工具生成的代码
        private Button m_btnLeft;
        private Button m_btnRight;
        protected override void ScriptGenerator()
        {
            m_btnLeft = FindChildComponent<Button>("Bg/m_btnLeft");
            m_btnRight = FindChildComponent<Button>("Bg/m_btnRight");
            m_btnLeft.onClick.AddListener(OnClickLeftBtn);
            m_btnRight.onClick.AddListener(OnClickRightBtn);
        }
        #endregion

        #region 事件
        private void OnClickRightBtn()
        {
            
        }
        private void OnClickLeftBtn()
        {
            GameModule.UI.HideUI<Level4>();
            GameModule.UI.ShowUI<Level3>();
        }
        #endregion

    }
}