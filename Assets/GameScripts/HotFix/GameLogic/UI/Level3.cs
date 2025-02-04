using UnityEngine;
using UnityEngine.UI;
using TEngine;

namespace GameLogic
{
    [Window(UILayer.UI)]
    class Level3 : UIWindow
    {
        #region 脚本工具生成的代码
        private Text m_text1;
        private Text m_text2;
        private Text m_text3;
        protected override void ScriptGenerator()
        {
            m_text1 = FindChildComponent<Text>("m_text1");
            m_text2 = FindChildComponent<Text>("m_text2");
            m_text3 = FindChildComponent<Text>("m_text3");
        }
        #endregion

        #region 事件
        #endregion

    }
}