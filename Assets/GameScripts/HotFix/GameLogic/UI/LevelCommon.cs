using UnityEngine;
using UnityEngine.UI;
using TEngine;

namespace GameLogic
{
    [Window(UILayer.UI)]
    class LevelCommon : UIWindow
    {
        #region 脚本工具生成的代码
        private Button m_btnUpArrow;
        private Button m_btnDownArrow;
        protected override void ScriptGenerator()
        {
            m_btnUpArrow = FindChildComponent<Button>("m_btnUpArrow");
            m_btnDownArrow = FindChildComponent<Button>("m_btnDownArrow");
            m_btnUpArrow.onClick.AddListener(OnClickUpArrowBtn);
            m_btnDownArrow.onClick.AddListener(OnClickDownArrowBtn);
        }
        #endregion

        #region 事件
        private void OnClickUpArrowBtn()
        {
        }
        private void OnClickDownArrowBtn()
        {
        }
        #endregion

    }
}