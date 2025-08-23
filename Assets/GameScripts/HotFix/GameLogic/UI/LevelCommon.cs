using UnityEngine;
using UnityEngine.UI;
using TEngine;
using System.Collections.Generic;
using GameConfig.item;
using UnityEngine.EventSystems;
using AudioType = TEngine.AudioType;

namespace GameLogic
{
    [Window(UILayer.Top,hideTimeToClose:0)]
    public class LevelCommon : UIWindow
    {
        private List<int> m_itemList = new List<int>(); // 存储所有道具ID
        private int m_currentIndex = 0; // 当前显示的起始索引
        private Vector2 m_item1OriginalPos; // 存储Item1的原始位置
        private Vector2 m_item2OriginalPos; // 存储Item2的原始位置

        #region 脚本工具生成的代码
        private Button m_btnUpArrow;
        private GameObject m_go_bg1;
        private Image m_imgItem1;
        private GameObject m_go_textBg1;
        private Text m_textTitle1;
        private GameObject m_go_bg2;
        private Image m_imgItem2;
        private GameObject m_go_textBg2;
        private Text m_textTitle2;
        private Button m_btnDownArrow;
        protected override void ScriptGenerator()
        {
            m_btnUpArrow = FindChildComponent<Button>("m_btnUpArrow");
            m_go_bg1 = FindChild("item1/m_go_bg1").gameObject;
            m_imgItem1 = FindChildComponent<Image>("item1/m_imgItem1");
            m_go_textBg1 = FindChild("item1/m_go_textBg1").gameObject;
            m_textTitle1 = FindChildComponent<Text>("item1/m_go_textBg1/m_textTitle1");
            m_go_bg2 = FindChild("item2/m_go_bg2").gameObject;
            m_imgItem2 = FindChildComponent<Image>("item2/m_imgItem2");
            m_go_textBg2 = FindChild("item2/m_go_textBg2").gameObject;
            m_textTitle2 = FindChildComponent<Text>("item2/m_go_textBg2/m_textTitle2");
            m_btnDownArrow = FindChildComponent<Button>("m_btnDownArrow");
            m_btnUpArrow.onClick.AddListener(OnClickUpArrowBtn);
            m_btnDownArrow.onClick.AddListener(OnClickDownArrowBtn);
        }
        #endregion

        #region 事件

        protected override void OnCreate()
        {
            m_imgItem1.gameObject.AddComponent<ItemDragHandler>().Init(this, 0);
            m_imgItem2.gameObject.AddComponent<ItemDragHandler>().Init(this, 1);
            m_go_textBg1.SetActive(false);
            m_go_textBg2.SetActive(false);
            // 保存原始位置
            m_item1OriginalPos = m_imgItem1.rectTransform.anchoredPosition;
            m_item2OriginalPos = m_imgItem2.rectTransform.anchoredPosition;
        }

        private void OnClickUpArrowBtn()
        {
            GameModule.Audio.Play(AudioType.UISound, "Menu1A");
            if (m_currentIndex > 0)
            {
                m_currentIndex--;
                UpdateItemDisplay();
            }
        }

        private void OnClickDownArrowBtn()
        {
            GameModule.Audio.Play(AudioType.UISound, "Menu1A");
            if (m_currentIndex < m_itemList.Count - 2)
            {
                m_currentIndex++;
                UpdateItemDisplay();
            }
        }

        #endregion

        protected override void RegisterEvent()
        {
            AddUIEvent<int>(ClientEventID.AddItem, OnRefreshItem);
            AddUIEvent<int>(ClientEventID.UseItem, OnRefreshItem);
        }

        private void UpdateItemDisplay()
        {
            // 更新第一个道具显示
            if (m_currentIndex < m_itemList.Count)
            {
                m_go_bg1.SetActive(true);
                SetItemImage(m_imgItem1,m_go_textBg1,m_textTitle1, m_itemList[m_currentIndex]);
            }
            else
            {
                m_go_bg1.SetActive(false);
                SetItemDefault(m_imgItem1,m_go_textBg1);
            }

            // 更新第二个道具显示
            if (m_currentIndex + 1 < m_itemList.Count)
            {
                m_go_bg2.SetActive(true);
                SetItemImage(m_imgItem2,m_go_textBg2,m_textTitle2, m_itemList[m_currentIndex + 1]);
            }
            else
            {
                m_go_bg2.SetActive(false);
                SetItemDefault(m_imgItem2,m_go_textBg2);
            }

            // 更新箭头按钮状态
            //m_btnUpArrow.interactable = m_currentIndex > 0;
            //m_btnDownArrow.interactable = m_currentIndex < m_itemList.Count - 2;
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            OnRefreshItem(0);
        }

        private void OnRefreshItem(int itemID)
        {
            //Debug.LogError(itemID);
            m_itemList = BagManager.Instance.GetItemList();
            UpdateItemDisplay();
        }

        // 假设这个方法用于设置道具图片
        private void SetItemImage(Image imgComponent,GameObject obj,Text txt, int itemID)
        {
            Item tbItem = ConfigLoader.Instance.Tables.TbItem.Get(itemID);
            // 在这里实现根据itemID设置图片的逻辑
            imgComponent.sprite = GameModule.Resource.LoadAsset<Sprite>(tbItem.Icon);
            obj.SetActive(true);
            txt.text = LocalizationManager.Instance.GetText(tbItem.Name);
        }

        private void SetItemDefault(Image imgComponent,GameObject obj)
        {
            obj.SetActive(false);
            imgComponent.sprite = GameModule.Resource.LoadAsset<Sprite>($"defaultItem");
        }

        // 新增：处理物品拖拽结束
        public void OnItemDragEnd(int itemIndex, PointerEventData eventData)
        {
            Image targetImage = itemIndex == 0 ? m_imgItem1 : m_imgItem2;
            Vector2 originalPos = itemIndex == 0 ? m_item1OriginalPos : m_item2OriginalPos;

            int itemID=BagManager.Instance.GetItemIDByIndex(itemIndex);

            // 检查是否触碰到带特定Tag的UI
            Debug.Log($"DragEnd itemIndex:{itemIndex},itemID:{itemID}");
            if (itemID!=-1)
            {
                GameEvent.Send(ClientEventID.UseItem, itemID);
            }
            
            /*
            if (IsPointerOverTaggedUI(eventData, Global.TRIGGER_TAG_1))
            {
                Debug.Log($"tag:{Global.TRIGGER_TAG_1},id:{Global.Cfg_Item_Sticker}");
                GameEvent.Send(ClientEventID.UseItem, itemIndex);
            }
            else if (IsPointerOverTaggedUI(eventData, Global.TRIGGER_TAG_2))
            {
                Debug.Log($"tag:{Global.TRIGGER_TAG_2},id:{Global.Cfg_Item_Gloves}");
                GameEvent.Send(ClientEventID.UseItem, itemIndex);
            }
            else if (IsPointerOverTaggedUI(eventData, Global.TRIGGER_TAG_3))
            {
                Debug.Log($"tag:{Global.TRIGGER_TAG_3},id:{Global.Cfg_Item_Brick}");
                GameEvent.Send(ClientEventID.UseItem, itemIndex);
            }
            else if (IsPointerOverTaggedUI(eventData, Global.TRIGGER_TAG_4))
            {
                Debug.Log($"tag:{Global.TRIGGER_TAG_4},id:{Global.Cfg_Item_Doorknob}");
                GameEvent.Send(ClientEventID.UseItem, itemIndex);
            }
            */

            // 无论如何都要返回原位
            targetImage.rectTransform.anchoredPosition = originalPos;
        }

        private void TriggerItemUse(int itemId)
        {
            BagManager.Instance.UseItem(itemId);
            Log.Debug("UseItem:" + itemId);
        }

        private bool IsPointerOverTaggedUI(PointerEventData eventData, int Layer)
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