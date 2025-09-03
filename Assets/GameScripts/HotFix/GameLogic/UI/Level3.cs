using UnityEngine;
using UnityEngine.UI;
using TEngine;
using AudioType = TEngine.AudioType;
namespace GameLogic
{
    [Window(UILayer.UI,hideTimeToClose:0)]
    public class Level3 : UIWindow
    {
       	#region 脚本工具生成的代码
		private GameObject m_goEn;
		private GameObject m_goCN;
		private GameObject m_goTR;
		private Button m_btnRight;
		private Button m_btnLeft;
		protected override void ScriptGenerator()
		{
			m_goEn = FindChild("m_goEn").gameObject;
			m_goCN = FindChild("m_goCN").gameObject;
			m_goTR = FindChild("m_goTR").gameObject;
			m_btnRight = FindChildComponent<Button>("m_btnRight");
			m_btnLeft = FindChildComponent<Button>("m_btnLeft");
			m_btnRight.onClick.AddListener(OnClickRightBtn);
			m_btnLeft.onClick.AddListener(OnClickLeftBtn);
		}
		#endregion

        #region 事件
        private void OnClickRightBtn()
        {
            GameModule.Audio.Play(AudioType.UISound,"Menu1A");
            GameModule.UI.HideUI<Level3>();
            GameModule.UI.ShowUI<Level4>();
        }
        private void OnClickLeftBtn()
        {
            GameModule.Audio.Play(AudioType.UISound,"Menu1A");
            GameModule.UI.HideUI<Level3>();
            GameModule.UI.ShowUI<Level2>();
        }
        protected override void OnRefresh()
        {
            Global.level_index = 3;
            base.OnRefresh();
            if(LocalizationManager.Instance.language == Language.English)
            {
               m_goEn.SetActive(true);
               m_goCN.SetActive(false);
               m_goTR.SetActive(false);
            }
            else if(LocalizationManager.Instance.language == Language.ChineseSimplified)
            {
                m_goEn.SetActive(false);
                m_goCN.SetActive(true);
                m_goTR.SetActive(false);
            }
            else
            {
                m_goEn.SetActive(false);
                m_goCN.SetActive(false);
                m_goTR.SetActive(true);
            }
        }
        #endregion

    }
}