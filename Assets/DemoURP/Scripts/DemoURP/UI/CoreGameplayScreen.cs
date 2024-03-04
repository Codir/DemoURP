using System;
using UnityEngine;
using UnityEngine.UI;

namespace DemoURP.UI
{
    public class CoreGameplayScreen : MonoBehaviour
    {
        #region serialize fields

        [SerializeField] private Button ShadowsToggleButton;
        [SerializeField] private Button BloomToggleButton;

        #endregion

        #region fields

        private bool _isShadowsEnabled = true;
        private bool _isBloomEnabled;
        private Renderer[] _renderers;

        #endregion

        #region engine methods

        private void Awake()
        {
            _renderers = FindObjectsOfType<Renderer>();
        }

        private void OnEnable()
        {
            ShadowsToggleButton.onClick.AddListener(OnShadowsToggleButtonClicked);
            BloomToggleButton.onClick.AddListener(OnBloomToggleButtonClicked);
        }

        private void OnDisable()
        {
            ShadowsToggleButton.onClick.RemoveListener(OnShadowsToggleButtonClicked);
            BloomToggleButton.onClick.RemoveListener(OnBloomToggleButtonClicked);
        }

        #endregion

        #region private methods

        private void OnShadowsToggleButtonClicked()
        {
            _isShadowsEnabled = !_isShadowsEnabled;

            foreach (Renderer item in _renderers)
            {
                item.shadowCastingMode = _isShadowsEnabled
                    ? UnityEngine.Rendering.ShadowCastingMode.On
                    : UnityEngine.Rendering.ShadowCastingMode.Off;
            }
        }

        private void OnBloomToggleButtonClicked()
        {
            _isBloomEnabled = !_isBloomEnabled;
        }

        #endregion
    }
}