using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
namespace TEngine
{
    [RequireComponent(typeof(CanvasGroup))]
    public class AutoFade : MonoBehaviour
    {
        public float fadeInDuration = 1f;  // 淡入时间
        public float fadeOutDuration = 1f; // 淡出时间

        private CanvasGroup canvasGroup;

        void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup=this.gameObject.AddComponent<CanvasGroup>();
            } 
            canvasGroup.alpha = 0;
        }

        void OnEnable()
        {
            DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1, fadeInDuration).OnComplete(delegate()
            {
                //Debug.Log("透明度变化完成");
            });
        }

        void OnDisable()
        {
            DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0, fadeOutDuration).OnComplete(delegate()
            {
                //Debug.Log("透明度变化完成");
            });
        }
    }
}
