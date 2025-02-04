using Cysharp.Threading.Tasks;
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
        protected override void ScriptGenerator()
        {
            m_btnGloves = FindChildComponent<Button>("m_btnGloves");
            m_btnGloves.onClick.AddListener(OnClickGlovesBtn);
        }
        #endregion

        #region 事件
        private void OnClickGlovesBtn()
        {
            Debug.Log("123");
        }
        #endregion

    }
}