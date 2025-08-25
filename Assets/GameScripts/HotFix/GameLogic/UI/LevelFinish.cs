using UnityEngine;
using UnityEngine.UI;
using TEngine;
using DG.Tweening;

namespace GameLogic
{
    [Window(UILayer.UI,hideTimeToClose:0)]
    public class LevelFinish : UIWindow
    {
        #region 脚本工具生成的代码
        private Button m_btnBackGround;
        private Text m_textDes;
        protected override void ScriptGenerator()
        {
            m_btnBackGround = FindChildComponent<Button>("m_btnBackGround");
            m_textDes = FindChildComponent<Text>("m_btnBackGround/m_textDes");
            m_btnBackGround.onClick.AddListener(OnClickBackGroundBtn);
        }
        #endregion

        #region 事件

        private int desIndex = 1;
        private bool isAnimating = false;  // 添加动画状态标记

        private void OnClickBackGroundBtn()
        {
            if (desIndex>=6)
            {
                BagManager.Instance.Clear();
                Global.Level2Right = false;
                GameModule.UI.HideUI<LevelFinish>();
                GameModule.UI.ShowUI<StartPage>();
            }
            // 如果正在播放动画或已达到最大提示数，则不响应点击
            if (isAnimating || desIndex >= 6)
            {
                return;
            }
            
            isAnimating = true;  // 标记动画开始
            
            // 当前文本渐隐
            m_textDes.DOFade(0f, 0.3f).OnComplete(() =>
            {
                desIndex++;
                string configKey = $"level4_tip{desIndex}";
                string tipText = LocalizationManager.Instance.GetText(configKey);
                m_textDes.text = tipText;
                
                // 新文本渐显
                m_textDes.DOFade(1f, 0.3f).OnComplete(() =>
                {
                    isAnimating = false;  // 标记动画结束
                });
            });
        }

        protected override void OnRefresh()
        {
            string configKey = $"level4_tip{desIndex}";
            string tipText = LocalizationManager.Instance.GetText(configKey);
            m_textDes.text = tipText;
        }

        private void OnClickRightBtn()
        {
            
        }
        private void OnClickLeftBtn()
        {
        }

        #endregion

    }
}