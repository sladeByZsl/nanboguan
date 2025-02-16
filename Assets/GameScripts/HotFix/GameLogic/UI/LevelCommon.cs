using UnityEngine;
using UnityEngine.UI;
using TEngine;
using System.Collections.Generic;
using GameConfig.item;
using UnityEngine.EventSystems;

namespace GameLogic
{
    [Window(UILayer.Top)]
    public class LevelCommon : UIWindow
    {
        private List<int> m_itemList = new List<int>();  // 存储所有道具ID
        private int m_currentIndex = 0;  // 当前显示的起始索引
        private Vector2 m_item1OriginalPos; // 存储Item1的原始位置
    private Vector2 m_item2OriginalPos; // 存储Item2的原始位置

        #region 脚本工具生成的代码
		private Button m_btnUpArrow;
		private Image m_imgItem1;
		private Image m_imgItem2;
		private Button m_btnDownArrow;
		protected override void ScriptGenerator()
		{
			m_btnUpArrow = FindChildComponent<Button>("m_btnUpArrow");
			m_imgItem1 = FindChildComponent<Image>("item1/m_imgItem1");
            m_imgItem1.gameObject.AddComponent<ItemDragHandler>().Init(this,0);
			m_imgItem2 = FindChildComponent<Image>("item2/m_imgItem2");
             m_imgItem2.gameObject.AddComponent<ItemDragHandler>().Init(this,1);
			m_btnDownArrow = FindChildComponent<Button>("m_btnDownArrow");
			m_btnUpArrow.onClick.AddListener(OnClickUpArrowBtn);
			m_btnDownArrow.onClick.AddListener(OnClickDownArrowBtn);

             // 保存原始位置
            m_item1OriginalPos = m_imgItem1.rectTransform.anchoredPosition;
            m_item2OriginalPos = m_imgItem2.rectTransform.anchoredPosition;
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
            AddUIEvent<int>(ClientEventID.AddItem,OnRefreshItem);
            AddUIEvent<int>(ClientEventID.UseItem,OnRefreshItem);
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

        private void OnRefreshItem(int itemID)
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

       // 新增：处理物品拖拽结束
        public void OnItemDragEnd(int itemIndex, PointerEventData eventData)
        {
            Image targetImage = itemIndex == 0 ? m_imgItem1 : m_imgItem2;
            Vector2 originalPos = itemIndex == 0 ? m_item1OriginalPos : m_item2OriginalPos;
            
            // 检查是否触碰到带特定Tag的UI
            if (IsPointerOverTaggedUI(eventData,Global.TRIGGER_TAG_1))
            {
                GameEvent.Send(ClientEventID.UseItem,Global.Cfg_Item_Sticker);
                // int realItemIndex = m_currentIndex + itemIndex;
                // if (realItemIndex < m_itemList.Count)
                // {
                //     int itemId = m_itemList[realItemIndex];
                //  
                //     // 触发物品使用事件
                //     //TriggerItemUse(itemId);
                // }
            }
            else if (IsPointerOverTaggedUI(eventData,Global.TRIGGER_TAG_2))
            {
                GameEvent.Send(ClientEventID.UseItem,Global.Cfg_Item_Gloves);
            }

            // 无论如何都要返回原位
            targetImage.rectTransform.anchoredPosition = originalPos;
        }

        private void TriggerItemUse(int itemId)
        {
            BagManager.Instance.UseItem(itemId);
            Log.Debug("UseItem:"+itemId);
        }

        private bool IsPointerOverTaggedUI(PointerEventData eventData,int Layer)
        {
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            foreach (var result in results)
            {
                if (result.gameObject.activeSelf && result.gameObject.layer == Layer)
                {
                    //Debug.LogError("Point:"+result.gameObject.name+",active:"+result.gameObject.activeSelf);
                    return true;
                }
            }

            return false;
        }
    }
}