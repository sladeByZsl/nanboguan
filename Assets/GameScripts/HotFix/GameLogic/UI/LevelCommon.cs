using UnityEngine;
using UnityEngine.UI;
using TEngine;
using System.Collections.Generic;
using GameConfig.item;

namespace GameLogic
{
    [Window(UILayer.UI)]
    class LevelCommon : UIWindow
    {
        private List<int> m_itemList = new List<int>();  // 存储所有道具ID
        private int m_currentIndex = 0;  // 当前显示的起始索引

        #region 脚本工具生成的代码
		private Button m_btnUpArrow;
		private Image m_imgItem1;
		private Image m_imgItem2;
		private Button m_btnDownArrow;
		protected override void ScriptGenerator()
		{
			m_btnUpArrow = FindChildComponent<Button>("m_btnUpArrow");
			m_imgItem1 = FindChildComponent<Image>("item1/m_imgItem1");
			m_imgItem2 = FindChildComponent<Image>("item2/m_imgItem2");
			m_btnDownArrow = FindChildComponent<Button>("m_btnDownArrow");
			m_btnUpArrow.onClick.AddListener(OnClickUpArrowBtn);
			m_btnDownArrow.onClick.AddListener(OnClickDownArrowBtn);
		}
		#endregion

        #region 事件
        private void OnClickUpArrowBtn()
        {
            if (m_currentIndex > 0)
            {
                m_currentIndex--;
                UpdateItemDisplay();
            }
        }

        private void OnClickDownArrowBtn()
        {
            if (m_currentIndex < m_itemList.Count - 2)
            {
                m_currentIndex++;
                UpdateItemDisplay();
            }
        }
        #endregion

        protected override void RegisterEvent()
        {
            AddUIEvent<int>(ClientEventID.AddItem,OnAddItem);
        }

        private void UpdateItemDisplay()
        {
            // 更新第一个道具显示
            if (m_currentIndex < m_itemList.Count)
            {
                SetItemImage(m_imgItem1, m_itemList[m_currentIndex]);
            }
            else
            {
                SetItemDefault(m_imgItem1);
            }

            // 更新第二个道具显示
            if (m_currentIndex + 1 < m_itemList.Count)
            {
                SetItemImage(m_imgItem2, m_itemList[m_currentIndex + 1]);
            }
            else
            {
                SetItemDefault(m_imgItem2);
            }

            // 更新箭头按钮状态
            //m_btnUpArrow.interactable = m_currentIndex > 0;
            //m_btnDownArrow.interactable = m_currentIndex < m_itemList.Count - 2;
        }

        private void OnAddItem(int itemID)
        {
            //Debug.LogError(itemID);
            m_itemList=BagManager.Instance.GetItemList();
            UpdateItemDisplay();
        }

        // 假设这个方法用于设置道具图片
        private void SetItemImage(Image imgComponent, int itemID)
        {
           Item tbItem = ConfigLoader.Instance.Tables.TbItem.Get(itemID);
            // 在这里实现根据itemID设置图片的逻辑
            imgComponent.sprite=GameModule.Resource.LoadAsset<Sprite>(tbItem.Icon);
        }

        private void SetItemDefault(Image imgComponent)
        {
            imgComponent.sprite=GameModule.Resource.LoadAsset<Sprite>($"defaultItem");
        }
    }
}