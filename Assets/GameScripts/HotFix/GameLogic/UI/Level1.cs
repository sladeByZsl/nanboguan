using GameConfig.item;
using UnityEngine;
using UnityEngine.UI;
using TEngine;

namespace GameLogic
{
    [Window(UILayer.UI)]
    public class Level1 : UIWindow
    {
        #region 脚本工具生成的代码
        private Button m_btnGloves;
        private Button m_btnRight;
        private Button m_btnLeft;
        private Button m_btnShowTask;
        private GameObject m_goAnswer;
        private GameObject m_goNoTalk;
        private GameObject m_go_Talk;
        private Button m_btnBack;
        private GameObject m_goTrigger1;
        protected override void ScriptGenerator()
        {
            m_btnGloves = FindChildComponent<Button>("Bg/m_btnGloves");
            m_btnRight = FindChildComponent<Button>("Bg/m_btnRight");
            m_btnLeft = FindChildComponent<Button>("Bg/m_btnLeft");
            m_btnShowTask = FindChildComponent<Button>("Bg/m_btnShowTask");
            m_goAnswer = FindChild("Bg/m_goAnswer").gameObject;
            m_goNoTalk = FindChild("Bg/Image/m_goNoTalk").gameObject;
            m_go_Talk = FindChild("Bg/Image/m_go_Talk").gameObject;
            m_btnBack = FindChildComponent<Button>("Bg/m_btnBack");
            m_goTrigger1 = FindChild("Bg/m_btnBack/bg/m_goTrigger1").gameObject;
            m_btnGloves.onClick.AddListener(OnClickGlovesBtn);
            m_btnRight.onClick.AddListener(OnClickRightBtn);
            m_btnLeft.onClick.AddListener(OnClickLeftBtn);
            m_btnShowTask.onClick.AddListener(OnClickShowTaskBtn);
            m_btnBack.onClick.AddListener(OnClickBackBtn);
        }
        #endregion

        #region 事件

        private void OnClickBackBtn()
        {
            m_btnBack.gameObject.SetActive(false);
        }
        private void OnClickShowTaskBtn()
        {
            m_btnBack.gameObject.SetActive(true);
        }

        protected override void OnRefresh()
        {
            //Debug.LogError("show:"+BagManager.Instance.IsItemUsed(Global.Cfg_Item_Sticker));
            m_btnBack.gameObject.SetActive(false);
            if (BagManager.Instance.IsItemUsed(Global.Cfg_Item_Sticker))
            {
                m_goNoTalk.SetActive(false);
                m_go_Talk.SetActive(true);
                m_goAnswer.SetActive(true);
            }
            else
            {
                m_goNoTalk.SetActive(true);
                m_go_Talk.SetActive(false);
                m_goAnswer.SetActive(false);
            }
        }

        private void OnClickGlovesBtn()
        {
            if(BagManager.Instance.IsCanAdd(Global.Cfg_Item_Gloves)) 
            {
                BagManager.Instance.AddItem(Global.Cfg_Item_Gloves);
                m_btnGloves.gameObject.SetActive(false);
            }

            //Log.Error(LocalizationManager.Instance.GetText(Global.Key_level2_tips));
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
            AddUIEvent<int>(ClientEventID.UseItem1,OnUseItem1);
        }

        private void OnUseItem1(int id)
        {
            if (!BagManager.Instance.IsItemUsed(id))
            {
                BagManager.Instance.UseItem(id);
                m_btnBack.gameObject.SetActive(false);
                m_goAnswer.SetActive(true);
                GameEvent.Send(ClientEventID.ShowTips,Global.Key_level1_tips);
            }
        }

        private void OnAddItem(int id)
        {
            if (id == Global.Cfg_Item_Gloves)
            {
                m_btnGloves.gameObject.SetActive(false);
            }
        }
    }
}