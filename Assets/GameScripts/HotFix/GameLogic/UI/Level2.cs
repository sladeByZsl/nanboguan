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
        #region 脚本工具生成的代码
        private Button m_btnRight;
        private Button m_btnLeft;
        private Button m_btnItem1;
        private Button m_btnItem2;
        private Button m_btnItem3;
        private Button m_btnBottle;
        private GameObject m_goTip;
        private Text m_textTips;
        private CanvasGroup m_tipCanvasGroup;
        private CancellationTokenSource _tipsCts;
        private Tween _currentTween;


        protected override void ScriptGenerator()
        {
            m_btnRight = FindChildComponent<Button>("Bg/m_btnRight");
            m_btnLeft = FindChildComponent<Button>("Bg/m_btnLeft");
            m_btnItem1 = FindChildComponent<Button>("Bg/m_btnItem1");
            m_btnItem2 = FindChildComponent<Button>("Bg/m_btnItem2");
            m_btnItem3 = FindChildComponent<Button>("Bg/m_btnItem3");
            m_btnBottle = FindChildComponent<Button>("Bg/m_btnBottle");
            m_goTip = FindChild("Bg/m_goTip").gameObject;
            m_textTips = FindChildComponent<Text>("Bg/m_goTip/m_textTips");
            m_tipCanvasGroup = m_goTip.GetComponent<CanvasGroup>();
            if (m_tipCanvasGroup == null)
            {
                m_tipCanvasGroup = m_goTip.AddComponent<CanvasGroup>();
            }
            m_btnRight.onClick.AddListener(OnClickRightBtn);
            m_btnLeft.onClick.AddListener(OnClickLeftBtn);
            m_btnItem1.onClick.AddListener(OnClickItem1Btn);
            m_btnItem2.onClick.AddListener(OnClickItem2Btn);
            m_btnItem3.onClick.AddListener(OnClickItem3Btn);
            m_btnBottle.onClick.AddListener(OnClickBottleBtn);
        }
        #endregion

        #region 事件

        protected override void OnRefresh()
        {
            base.OnRefresh();
            
            // 确保组件正确初始化
            if (m_goTip != null)
            {
                m_goTip.SetActive(false);
                
                // 确保 CanvasGroup 存在
                if (m_tipCanvasGroup == null)
                {
                    m_tipCanvasGroup = m_goTip.GetComponent<CanvasGroup>();
                    if (m_tipCanvasGroup == null)
                    {
                        m_tipCanvasGroup = m_goTip.AddComponent<CanvasGroup>();
                    }
                }
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
            ShowTips();
        }

        private void OnClickItem2Btn()
        {
            ShowTips();
        }

        private void OnClickItem3Btn()
        {
            ShowTips();
        }

        private async void ShowTips()
        {
            // 检查必要组件是否存在
            if (m_goTip == null || m_tipCanvasGroup == null)
            {
                Log.Error("Tips components are not properly initialized!");
                return;
            }

            try
            {
                _tipsCts?.Cancel();
                _tipsCts = new CancellationTokenSource();
                m_textTips.text = LocalizationManager.Instance.GetText(Global.Key_level2_tips);
                m_goTip.SetActive(true);
                m_tipCanvasGroup.alpha = 0;
               
                // 淡入
                await m_tipCanvasGroup.DOFade(1.0f, 0.3f)
                    .SetEase(Ease.OutQuad)
                    .ToUniTask(cancellationToken:_tipsCts.Token);
                
                // 等待显示时间
                await UniTask.Delay(1500);
                
                // 淡出
                await m_tipCanvasGroup.DOFade(0, 0.3f)
                    .SetEase(Ease.InQuad)
                    .ToUniTask(cancellationToken:_tipsCts.Token);
                
                if (m_goTip != null)
                {
                    m_goTip.SetActive(false);
                }
            }
            catch (Exception e)
            {
                Log.Error($"Error in ShowTips: {e}");
            }
        }

        private void OnClickBottleBtn()
        {
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _tipsCts?.Cancel();
            _tipsCts = null;
            
            // 确保清理 Tween
            if (_currentTween != null)
            {
                _currentTween.Kill(complete: false);
                _currentTween = null;
            }
        }

        #endregion
    }
}
