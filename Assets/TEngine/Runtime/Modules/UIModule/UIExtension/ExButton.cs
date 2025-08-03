using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace TEngine
{
    [AddComponentMenu("UI/ExButton", 30)]
    public class ExButton : Button
    {
        [Header("Sound Settings")]
        [SerializeField]
        private string generalSoundName = "UI_Button_Click_General"; // 通用音效的名称

        [Header("按钮文本")]
        [SerializeField]
        private string btnText;
        

        [Header("按钮cd")]
        public float button_cd = 0.2f;  // 冷却时间
        [Header("CD期间提示key_new表id")]
        public string msg_key = "";  // CD期间提示信息

        [SerializeField]
        [Header("CD遮罩，fillAmount转圈[可无]")]
        private Image m_cd_mask;

        [SerializeField]
        [Header("CD倒计数文本[可无]")]
        private Text m_cd_text;

        private float cd_last_time = 0;
        private bool m_isCd = false;

        // 使得按钮在CD状态下无法点击
        public bool IsCd
        {
            get { return m_isCd; }
            set
            {
                m_isCd = value;
                if (m_cd_mask != null)
                {
                    m_cd_mask.gameObject.SetActive(value);
                    interactable = !value;
                }
                if (m_cd_text != null) 
                    m_cd_text.gameObject.SetActive(value);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            IsCd = false;
        }

        // 播放通用音效
        private void PlayGeneralSound()
        {
            GameModule.Audio.Play(AudioType.UISound,generalSoundName);
        }

        private void Press()
        {
            if (!IsActive() || !IsInteractable())
                return;

            if (IsCd || Time.unscaledTime < cd_last_time + button_cd)
            {
                if (!string.IsNullOrEmpty(msg_key))
                    TEngine.Log.Warning(msg_key);
                return;
            }

            // 播放通用音效
            PlayGeneralSound();

            // 设置冷却状态
            if (button_cd > 0)
                IsCd = true;

            // 调用原按钮的点击事件
            onClick.Invoke();

            cd_last_time = Time.unscaledTime;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            Press();
        }

        public override void OnSubmit(BaseEventData eventData)
        {
            Press();
            base.OnSubmit(eventData); // 确保按钮的默认提交行为仍然有效
        }

        private void Update()
        {
            if (!IsCd || button_cd <= 0)
                return;

            float pass_time = Time.unscaledTime - cd_last_time;
            if (pass_time < 0 || pass_time > button_cd)
            {
                IsCd = false;
                return;
            }

            float left_time = button_cd - pass_time;
            if (m_cd_mask != null)
                m_cd_mask.fillAmount = left_time / button_cd;

            if (m_cd_text != null)
            {
                m_cd_text.text = left_time > 1 ? 
                    Mathf.RoundToInt(left_time).ToString() : 
                    left_time.ToString("F1");
            }
        }
    }
}
