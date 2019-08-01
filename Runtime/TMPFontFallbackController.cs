using UnityEngine;
using System.Collections;
using TMPro;

namespace Loju.Localisation.TMPro
{
    public sealed class TMPFontFallbackController : MonoBehaviour
    {

        [SerializeField] private TMPFallbackMap[] fonts = null;

        private IEnumerator Start()
        {
            LocalisationController.instance.EventLanguageChanged += HandleLanguageChanged;

            while (!LocalisationController.instance.IsLoaded) yield return null;

            HandleLanguageChanged(LocalisationController.instance, LocalisationController.instance.currentLanguage);
        }

        private void OnDestroy()
        {
            LocalisationController.instance.EventLanguageChanged -= HandleLanguageChanged;
        }

        private void HandleLanguageChanged(LocalisationController sender, string currentLanguage)
        {
            int i = 0, l = fonts.Length;
            for (; i < l; ++i)
            {
                TMPFallbackMap map = fonts[i];
                TMP_FontAsset font = map.fontAsset;
                font.fallbackFontAssetTable.Clear();

                int j = 0, k = map.fallbacks.Length;
                for (; j < k; ++j)
                {
                    TMPFallbackMapLanguage fallback = map.fallbacks[j];
                    if (fallback.languageCode == currentLanguage)
                    {
                        font.fallbackFontAssetTable.Add(fallback.fontAsset);
                        //Debug.LogFormat("Set fallback {0} for {1}, language {2}", fallback.fontAsset, font, currentLanguage);
                        break;
                    }
                }
            }

        }

        [System.Serializable]
        public sealed class TMPFallbackMap
        {

            public TMP_FontAsset fontAsset;
            public TMPFallbackMapLanguage[] fallbacks;

        }

        [System.Serializable]
        public sealed class TMPFallbackMapLanguage
        {

            public string languageCode;
            public TMP_FontAsset fontAsset;

        }

    }
}