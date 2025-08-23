using UnityEngine;
using UnityEngine.UI;
using TEngine;
using DG.Tweening;
using AudioType = TEngine.AudioType;
namespace GameLogic
{
    [Window(UILayer.UI,hideTimeToClose:0)]
    public class Level4 : UIWindow
    {
        #region 脚本工具生成的代码
        private Button m_btnLeft;
        private Button m_btnRight;
        protected override void ScriptGenerator()
        {
            m_btnLeft = FindChildComponent<Button>("Bg/m_btnLeft");
            m_btnRight = FindChildComponent<Button>("Bg/m_btnRight");
            m_btnLeft.onClick.AddListener(OnClickLeftBtn);
            m_btnRight.onClick.AddListener(OnClickRightBtn);
        }
        #endregion

        #region 事件


        protected override void OnRefresh()
        {
           
        }

        private void OnClickRightBtn()
        {
            GameModule.Audio.Play(AudioType.UISound,"Menu1A");
        }
        private void OnClickLeftBtn()
        {
            GameModule.Audio.Play(AudioType.UISound,"Menu1A");
            GameModule.UI.HideUI<Level4>();
            GameModule.UI.ShowUI<Level3>();
        }

        protected override void RegisterEvent()
        {
            AddUIEvent<int>(ClientEventID.UseItem,OnUseItem);
        }

        private void OnUseItem(int id)
        {
            Debug.Log("Level4:"+id);
            if (!BagManager.Instance.IsItemUsed(id) && id == Global.Cfg_Item_Doorknob)
            {
                BagManager.Instance.UseItem(id);
                GameModule.UI.HideUI<Level4>();
                GameModule.UI.ShowUI<LevelFinish>();
            }
        }

        #endregion

    }
}