using System;
using UnityEngine;
using UnityEngine.UI;
using TEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
using AudioType = TEngine.AudioType;

namespace GameLogic
{
    [Window(UILayer.UI,hideTimeToClose:0)]
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
		private Button m_btnShowBackGroud;
		private Button m_btnDoorknob;
		private Button m_btnBackgroud;
		private InputField m_input1;
		private InputField m_input2;
		private InputField m_input3;
		private InputField m_input4;
		private Button m_btnInputOK;
		private GameObject m_goAnswer;
		protected override void ScriptGenerator()
		{
			m_btnRight = FindChildComponent<Button>("Bg/m_btnRight");
			m_btnLeft = FindChildComponent<Button>("Bg/m_btnLeft");
			m_btnItem1 = FindChildComponent<Button>("Bg/m_btnItem1");
			m_btnItem2 = FindChildComponent<Button>("Bg/m_btnItem2");
			m_btnItem3 = FindChildComponent<Button>("Bg/m_btnItem3");
			m_btnBottle = FindChildComponent<Button>("Bg/m_btnBottle");
			m_btnShowBackGroud = FindChildComponent<Button>("Bg/m_btnShowBackGroud");
			m_btnDoorknob = FindChildComponent<Button>("Bg/m_btnDoorknob");
			m_btnBackgroud = FindChildComponent<Button>("Bg/m_btnBackgroud");
			m_input1 = FindChildComponent<InputField>("Bg/m_btnBackgroud/m_input1");
			m_input2 = FindChildComponent<InputField>("Bg/m_btnBackgroud/m_input2");
			m_input3 = FindChildComponent<InputField>("Bg/m_btnBackgroud/m_input3");
			m_input4 = FindChildComponent<InputField>("Bg/m_btnBackgroud/m_input4");
			m_btnInputOK = FindChildComponent<Button>("Bg/m_btnBackgroud/m_btnInputOK");
			m_goAnswer = FindChild("Bg/m_goAnswer").gameObject;
			m_btnRight.onClick.AddListener(OnClickRightBtn);
			m_btnLeft.onClick.AddListener(OnClickLeftBtn);
			m_btnItem1.onClick.AddListener(OnClickItem1Btn);
			m_btnItem2.onClick.AddListener(OnClickItem2Btn);
			m_btnItem3.onClick.AddListener(OnClickItem3Btn);
			m_btnBottle.onClick.AddListener(OnClickBottleBtn);
			m_btnShowBackGroud.onClick.AddListener(OnClickShowBackGroudBtn);
			m_btnDoorknob.onClick.AddListener(OnClickDoorknobBtn);
			m_btnBackgroud.onClick.AddListener(OnClickBackgroudBtn);
			m_btnInputOK.onClick.AddListener(OnClickInputOKBtn);
		}
		#endregion

        #region 事件

        private void OnClickShowBackGroudBtn()
        {
            GameModule.Audio.Play(AudioType.UISound,"Menu1A");
            m_btnBackgroud.gameObject.SetActive(true);
        }
        
        private void OnClickDoorknobBtn()
        {
            GameModule.Audio.Play(AudioType.UISound,"Menu1A");
            if (BagManager.Instance.IsCanAdd(Global.Cfg_Item_Doorknob))
            {
                BagManager.Instance.AddItem(Global.Cfg_Item_Doorknob);
                m_btnDoorknob.gameObject.SetActive(false);
            }
        }

        
        private void OnClickInputOKBtn()
        {
            GameModule.Audio.Play(AudioType.UISound,"Menu1A");
            if (m_input1.text=="0"&&m_input2.text=="4"&&m_input3.text=="1"&&m_input4.text=="0")
            {
                GameEvent.Send(ClientEventID.ShowTips,Global.Key_level2_liekai_tips);
                m_btnBackgroud.gameObject.SetActive(false);
                m_btnDoorknob.gameObject.SetActive(true);
                m_goAnswer.SetActive(true);
                Global.Level2Right = true;
            }
            else
            {
                GameEvent.Send(ClientEventID.ShowTips,Global.Key_level2_tips_wrong_tips);
            }
        }

        private void OnClickBackgroudBtn()
        {
            GameModule.Audio.Play(AudioType.UISound,"Menu1A");
            m_btnBackgroud.gameObject.SetActive(false);
        }

        protected override void BindMemberProperty()
        {
            m_imgBottom=m_btnBottle.GetComponent<Image>();
            
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            m_btnBackgroud.gameObject.SetActive(false);
            if (!Global.Level2Right)
            {
                m_btnDoorknob.gameObject.SetActive(false);
                m_goAnswer.SetActive(false);
            }
            else
            {
                m_goAnswer.SetActive(true);
            }

            if (BagManager.Instance.IsItemUsed(Global.Cfg_Item_Doorknob))
            {
                m_btnDoorknob.gameObject.SetActive(false);
            }

            if (BagManager.Instance.HasItem(Global.Cfg_Item_Sticker))
            {
                m_imgBottom.SetSprite(Global.Key_item_new);
            }
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
            GameModule.Audio.Play(AudioType.UISound,"Menu1A");
            GameModule.UI.HideUI<Level2>();
            GameModule.UI.ShowUI<Level3>();
        }

        private void OnClickLeftBtn()
        {
            GameModule.Audio.Play(AudioType.UISound,"Menu1A");
            GameModule.UI.HideUI<Level2>();
            GameModule.UI.ShowUI<Level1>();
        }

        private void OnClickItem1Btn()
        {
            GameModule.Audio.Play(AudioType.UISound,"Menu1A");
            GameEvent.Send(ClientEventID.ShowTips,Global.Key_level2_tips);
        }

        private void OnClickItem2Btn()
        {
            GameModule.Audio.Play(AudioType.UISound,"Menu1A");
            GameEvent.Send(ClientEventID.ShowTips,Global.Key_level2_tips);
        }

        private void OnClickItem3Btn()
        {
            GameModule.Audio.Play(AudioType.UISound,"Menu1A");
            GameEvent.Send(ClientEventID.ShowTips,Global.Key_level2_tips);
        }

       
        
        private void OnClickBottleBtn()
        {
            GameModule.Audio.Play(AudioType.UISound,"Menu1A");
            GameEvent.Send(ClientEventID.ShowTips,Global.Key_level2_tips);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
           
        }

        #endregion
    }
}
