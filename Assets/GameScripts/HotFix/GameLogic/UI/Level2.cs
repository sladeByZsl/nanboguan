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
        protected override void ScriptGenerator()
        {
            m_btnBottle = FindChildComponent<Button>("m_btnBottle");
            m_btnBottle.onClick.AddListener(OnClickBottleBtn);
        }
        #endregion

        #region 事件
        private void OnClickBottleBtn()
        {
        }
        #endregion

    }
}