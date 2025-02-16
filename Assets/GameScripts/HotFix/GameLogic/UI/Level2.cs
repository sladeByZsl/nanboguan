using System;
using UnityEngine;
using UnityEngine.UI;
using TEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;

namespace GameLogic
{
    [Window(UILayer.UI)]
    class Level2 : UIWindow
    {
        public Image m_imgBottom;
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

        protected override void BindMemberProperty()
        {
            m_imgBottom=m_btnBottle.GetComponent<Image>();
            
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
        }

        protected override void RegisterEvent()
        {
            AddUIEvent<int>(ClientEventID.UseItem,OnUseItem);
        }

        private void OnUseItem(int id)
        {
            if (!BagManager.Instance.IsItemUsed(id) && id == Global.Cfg_Item_Gloves)
            {
                BagManager.Instance.UseItem(id);
                BagManager.Instance.AddItem(Global.Cfg_Item_Sticker);
                m_imgBottom.SetSprite(Global.Key_item_new);
            }
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

        private void OnClickItem1Btn()
        {
            GameEvent.Send(ClientEventID.ShowTips,Global.Key_level2_tips);
        }

        private void OnClickItem2Btn()
        {
            GameEvent.Send(ClientEventID.ShowTips,Global.Key_level2_tips);
        }

        private void OnClickItem3Btn()
        {
            GameEvent.Send(ClientEventID.ShowTips,Global.Key_level2_tips);
        }

       
        
        private void OnClickBottleBtn()
        {
            GameEvent.Send(ClientEventID.ShowTips,Global.Key_level2_tips);
            // if (BagManager.Instance.HasItem())
            // {
            // }
           
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
           
        }

        #endregion
    }
}
