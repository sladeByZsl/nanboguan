using System;
using UnityEngine;
using UnityEngine.UI;
using TEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
using AudioType = TEngine.AudioType;
using Com.TheFallenGames.OSA.Demos.LoopingSpinners;

namespace GameLogic
{
    [Window(UILayer.UI,hideTimeToClose:0)]
    class Level2 : UIWindow
    {
        public Image m_imgBottom;
        public LoopingSpinnerExample m_loopingSpinner1;
        public LoopingSpinnerExample m_loopingSpinner2;
        public LoopingSpinnerExample m_loopingSpinner3;
        public LoopingSpinnerExample m_loopingSpinner4;
        		#region 脚本工具生成的代码
		private Button m_btnRight;
		private Button m_btnLeft;
		private Button m_btnItem1;
		private Button m_btnItem2;
		private Button m_btnItem3;
		private GameObject m_goAnswerTi;
		private GameObject m_goAnswerAnswer;
		private Button m_btnBottle;
		private Button m_btnShowBackGroud;
		private Button m_btnDoorknob;
		private GameObject m_goBackgroud;
		private Button m_btnInputOK;
		private Button m_btnReturn;
		protected override void ScriptGenerator()
		{
			m_btnRight = FindChildComponent<Button>("Bg/m_btnRight");
			m_btnLeft = FindChildComponent<Button>("Bg/m_btnLeft");
			m_btnItem1 = FindChildComponent<Button>("Bg/m_btnItem1");
			m_btnItem2 = FindChildComponent<Button>("Bg/m_btnItem2");
			m_btnItem3 = FindChildComponent<Button>("Bg/m_btnItem3");
			m_goAnswerTi = FindChild("Bg/m_goAnswerTi").gameObject;
			m_goAnswerAnswer = FindChild("Bg/m_goAnswerAnswer").gameObject;
			m_btnBottle = FindChildComponent<Button>("Bg/m_btnBottle");
			m_btnShowBackGroud = FindChildComponent<Button>("Bg/m_btnShowBackGroud");
			m_btnDoorknob = FindChildComponent<Button>("Bg/m_btnDoorknob");
			m_goBackgroud = FindChild("Bg/m_goBackgroud").gameObject;
			m_btnInputOK = FindChildComponent<Button>("Bg/m_goBackgroud/m_btnInputOK");
			m_btnReturn = FindChildComponent<Button>("Bg/m_goBackgroud/m_btnReturn");
			m_btnRight.onClick.AddListener(OnClickRightBtn);
			m_btnLeft.onClick.AddListener(OnClickLeftBtn);
			m_btnItem1.onClick.AddListener(OnClickItem1Btn);
			m_btnItem2.onClick.AddListener(OnClickItem2Btn);
			m_btnItem3.onClick.AddListener(OnClickItem3Btn);
			m_btnBottle.onClick.AddListener(OnClickBottleBtn);
			m_btnShowBackGroud.onClick.AddListener(OnClickShowBackGroudBtn);
			m_btnDoorknob.onClick.AddListener(OnClickDoorknobBtn);
			m_btnInputOK.onClick.AddListener(OnClickInputOKBtn);
			m_btnReturn.onClick.AddListener(OnClickReturnBtn);
		}
		#endregion

        #region 事件

        protected override void OnCreate()
        {
            base.OnCreate();
            m_loopingSpinner1= FindChildComponent<LoopingSpinnerExample>("Bg/m_btnBackgroud/m_num1");
            m_loopingSpinner2= FindChildComponent<LoopingSpinnerExample>("Bg/m_btnBackgroud/m_num2");
            m_loopingSpinner3= FindChildComponent<LoopingSpinnerExample>("Bg/m_btnBackgroud/m_num3");
            m_loopingSpinner4= FindChildComponent<LoopingSpinnerExample>("Bg/m_btnBackgroud/m_num4");
        }
        protected override void RegisterEvent()
        {
            base.RegisterEvent();
            GameEvent.AddEventListener(ClientEventID.LanguageChanged,OnRefreshFont);
            AddUIEvent<int>(ClientEventID.UseItem,OnUseItem);
        }

        private void OnRefreshFont()
        {
            var texts = transform.GetComponentsInChildren<UnityEngine.UI.Text>(true);
            foreach (var txt in texts)
            {
                txt.font = Global.GetFont();
            }
        }

        private void OnClickNum1Btn()
        {
           
        }

        private void OnClickReturnBtn()
        {
            GameModule.Audio.Play(AudioType.UISound,"Menu1A");
            m_goBackgroud.SetActive(false);
        }

        private void OnClickShowBackGroudBtn()
        {
            GameModule.Audio.Play(AudioType.UISound, "Menu1A");
            m_goBackgroud.gameObject.SetActive(true);
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

            var middleVH1 = m_loopingSpinner1.Parameters.Snapper.GetMiddleVH(out _) as MyItemViewsHolder;
            int index1 = middleVH1.ItemIndex;
            
            var middleVH2 = m_loopingSpinner2.Parameters.Snapper.GetMiddleVH(out _) as MyItemViewsHolder;
            int index2 = middleVH2.ItemIndex;
            
            var middleVH3 = m_loopingSpinner3.Parameters.Snapper.GetMiddleVH(out _) as MyItemViewsHolder;
            int index3 = middleVH3.ItemIndex;
            
            var middleVH4 = m_loopingSpinner4.Parameters.Snapper.GetMiddleVH(out _) as MyItemViewsHolder;
            int index4 = middleVH4.ItemIndex;
            Debug.Log($"m_loopingSpinner1:{index1}");
            Debug.Log($"m_loopingSpinner2:{index2}");
            Debug.Log($"m_loopingSpinner3:{index3}");
            Debug.Log($"m_loopingSpinner4:{index4}");
            
            if (index1==0&&index2==4&&index3==1&&index4==0)
            {
                if (!Global.Level2Right)
                {
                    GameEvent.Send(ClientEventID.ShowTips,Global.Key_level2_liekai_tips);
                    m_goBackgroud.gameObject.SetActive(false);
                    m_btnDoorknob.gameObject.SetActive(true);
                    m_goAnswerTi.SetActive(false);
                    m_goAnswerAnswer.SetActive(true);
                    Global.Level2Right = true;
                }
            }
            else
            {
                GameEvent.Send(ClientEventID.ShowTips,Global.Key_level2_tips_wrong_tips);
            }
        }

        private void OnClickBackgroudBtn()
        {
            GameModule.Audio.Play(AudioType.UISound,"Menu1A");
            m_goBackgroud.gameObject.SetActive(false);
        }

        protected override void BindMemberProperty()
        {
            m_imgBottom=m_btnBottle.GetComponent<Image>();
            
        }

        protected override void OnRefresh()
        {
            Global.level_index = 2;
            base.OnRefresh();
            OnRefreshFont();
            if (m_loopingSpinner1!=null&&m_loopingSpinner1.IsInitialized)
            {
                Debug.Log("init list");
            }

            m_goBackgroud.gameObject.SetActive(false);
            if (!Global.Level2Right)
            {
                m_btnDoorknob.gameObject.SetActive(false);
                m_goAnswerAnswer.SetActive(false);
                m_goAnswerTi.SetActive(true);
            }
            else
            {
                m_goAnswerAnswer.SetActive(true);
                m_goAnswerTi.SetActive(false);
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
