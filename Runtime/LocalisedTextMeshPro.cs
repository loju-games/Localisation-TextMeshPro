using UnityEngine;
using System.Collections;
using Loju.Localisation;
using TMPro;

namespace UnityEngine.UI
{

    [RequireComponent(typeof(TMP_Text))]
    [AddComponentMenu("UI/Localised TextMeshPro", 10)]
    public class LocalisedTextMeshPro : MonoBehaviour
    {

        public string Key { get { return _key; } }

        [SerializeField] private string _key = "";
#if UNITY_EDITOR
#pragma warning disable 414
        [SerializeField] private string _previewLanguage = "en";
#endif

        private TMP_Text _textTarget;

        protected void Start()
        {
            if (!LocalisationController.instance.IsLoaded) LocalisationController.instance.EventLoaded += HandleEventLanguageChanged;
            else if (!string.IsNullOrEmpty(_key)) SetKey(_key);

            LocalisationController.instance.EventLanguageChanged += HandleEventLanguageChanged;
        }

        protected void OnDestroy()
        {
            if (LocalisationController.instance != null)
            {
                LocalisationController.instance.EventLoaded -= HandleEventLanguageChanged;
                LocalisationController.instance.EventLanguageChanged -= HandleEventLanguageChanged;
            }
        }

        public void SetKey(string languageKey)
        {
            _key = languageKey;
            if (_textTarget == null)
                _textTarget = GetComponent<TMP_Text>();
            if (_textTarget != null)
                _textTarget.text = LocalisationController.instance.GetString(_key);
        }

        private void HandleEventLanguageChanged(LocalisationController sender, string currentLanguage)
        {
            SetKey(_key);
        }

    }

}