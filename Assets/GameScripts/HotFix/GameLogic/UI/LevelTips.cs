using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TEngine;

namespace GameLogic
{
    [Window(UILayer.Tips)]
    public class LevelTips : UIWindow
    {
        private CanvasGroup m_tipCanvasGroup;
        private CancellationTokenSource _tipsCts;
        private Tween _currentTween;
        
        #region 脚本工具生成的代码
        private GameObject m_goTip;
        private Text m_textTips;
        protected override void ScriptGenerator()
        {
            m_goTip = FindChild("m_goTip").gameObject;
            m_textTips = FindChildComponent<Text>("m_goTip/m_textTips");
        }
        #endregion

        protected override void OnCreate()
        {
            base.OnCreate();// 确保组件正确初始化
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

        protected override void OnRefresh()
        {
            base.OnRefresh();
            m_goTip.SetActive(false);
        }

        protected override void RegisterEvent()
        {
            AddUIEvent<string>(ClientEventID.ShowTips,OnShowTips);
        }   

        private void OnShowTips(string id)
        {
            ShowTips(id);
        }
        
        public async void ShowTips(string id)
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
                m_textTips.text = LocalizationManager.Instance.GetText(id);
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
    }
}