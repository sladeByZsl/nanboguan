using UnityEngine;
using UnityEngine.UI;
using TEngine;

namespace GameLogic
{
    [Window(UILayer.UI)]
    class Level1 : UIWindow
    {
        #region 脚本工具生成的代码
        private Button m_btnGloves;
        private Button m_btnRight;
        private Button m_btnLeft;
        protected override void ScriptGenerator()
        {
            m_btnGloves = FindChildComponent<Button>("m_btnGloves");
            m_btnRight = FindChildComponent<Button>("m_btnRight");
            m_btnLeft = FindChildComponent<Button>("m_btnLeft");
            m_btnGloves.onClick.AddListener(OnClickGlovesBtn);
            m_btnRight.onClick.AddListener(OnClickRightBtn);
            m_btnLeft.onClick.AddListener(OnClickLeftBtn);
        }
        #endregion

        #region 事件
        private void OnClickGlovesBtn()
        {
            
        }
        private void OnClickRightBtn()
        {
            GameModule.UI.HideUI<Level1>();
            GameModule.UI.ShowUI<Level2>();
        }
        private void OnClickLeftBtn()
        {
           
        }
        #endregion

    }
}