using System.Collections;
using System.Collections.Generic;
using GameBase;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class BagManager : Singleton<BagManager>
    {
        public List<int> itemList = new List<int>();
        public Dictionary<int, bool> useItemDict = new Dictionary<int, bool>();

        public BagManager()
        {
        }

        public void AddItem(int id)
        {
            if (!itemList.Contains(id)&&!IsItemUsed(id))
            {
                itemList.Add(id);
                useItemDict[id] = false;  // 添加物品时，初始化为未使用状态
                GameEvent.Send(ClientEventID.AddItem, id);
            }
        }

        public List<int> GetItemList()
        {
            return itemList;
        }

        public void UseItem(int itemId)
        {
            if (HasItem(itemId) && !IsItemUsed(itemId))
            {
                itemList.Remove(itemId);
                useItemDict[itemId] = true;  // 标记物品为已使用
                GameEvent.Send(ClientEventID.UseItem, itemId);
            }
        }

        public int GetItemIDByIndex(int index)
        {
            if (index<itemList.Count)
            {
                return itemList[index];
            }

            return -1;
        }

        public bool HasItem(int itemId)
        {
            return itemList.Contains(itemId);
        }

        public bool IsItemUsed(int itemId)
        {
            return useItemDict.ContainsKey(itemId) && useItemDict[itemId];
        }

        public bool IsCanAdd(int itemId)
        {
            return !itemList.Contains(itemId) && !IsItemUsed(itemId);
        }
    }
}
