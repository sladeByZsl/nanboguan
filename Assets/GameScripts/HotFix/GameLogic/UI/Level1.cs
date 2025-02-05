using GameConfig.item;
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

        private int itemId = 10000;
        private void OnClickGlovesBtn()
        {
            BagManager.Instance.AddItem(10000);
            m_btnGloves.gameObject.SetActive(false);
            // if (itemId < 10005)
            // {
            //     BagManager.Instance.AddItem(itemId);
            //     itemId++;
            // }
        
            //ConfigLoader.Instance.Tables.GetTable<TbItem>(10000);
            // var skillBaseConfig = ConfigLoader.Instance.Tables.TbItem.Get(10000);
            // Log.Error(skillBaseConfig);
            // var test2= ConfigSystem.Instance.Tables.TbItem[10001];
            // Log.Error(test2);
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

        protected override void RegisterEvent()
        {
            AddUIEvent<int>(ClientEventID.AddItem,OnAddItem);
        }

        private void OnAddItem(int id)
        {
            if (id == 1000)
            {
                m_btnGloves.gameObject.SetActive(false);
            }
        }
    }
}