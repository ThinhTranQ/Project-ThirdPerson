using System;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace GenifyLibs.ToastUI
{
    public class ToastUIScript : MonoBehaviour
    {
        public static ToastUIScript Instance { get; set; }
        [SerializeField] private GameObject goContainer;
        [SerializeField] private RectTransform rectGlow;
        [SerializeField] private TextMeshProUGUI txtContent;

        private Coroutine coroutineEndToast;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                goContainer.SetActive(false);
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

        public void ShowToast(string sToast = "")
        {
            if (Instance == null) return;

            goContainer.SetActive(false);
            txtContent.text = sToast;
            goContainer.SetActive(true);
            rectGlow.sizeDelta = new Vector2(txtContent.preferredWidth * 2.4f, txtContent.preferredHeight * 2.4f);

            if (coroutineEndToast != null)
            {
                StopCoroutine(coroutineEndToast);
                coroutineEndToast = null;
            }

            coroutineEndToast = StartCoroutine(WaitDisableToast());
            Debug.Log("ToastUI: " + sToast);
        }

        public async void ShowToast(string sToast = "", float timeDelay = 1)
        {
            if (Instance == null) return;
            await Task.Delay(TimeSpan.FromSeconds(timeDelay));
            ShowToast(sToast);
        }

        public void ShowToast(string sToast = "", Color color = default)
        {
            if (Instance == null) return;

            ShowToast($"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{sToast}</color>");
        }

        private IEnumerator WaitDisableToast()
        {
            yield return new WaitForSecondsRealtime(2.5f);
            goContainer.SetActive(false);
        }
    }
}