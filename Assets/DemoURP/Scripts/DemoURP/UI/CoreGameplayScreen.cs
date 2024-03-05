using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace DemoURP.UI
{
    public class CoreGameplayScreen : MonoBehaviour
    {
        #region serialize fields

        [SerializeField] private Volume PostProcessingBloom;
        [SerializeField] private Button ShadowsToggleButton;
        [SerializeField] private Button BloomToggleButton;

        #endregion

        #region fields

        private bool _isShadowsEnabled = true;
        private bool _isBloomEnabled = true;
        private Renderer[] _renderers;
        private Bloom _bloom;

        #endregion

        #region engine methods

        private void Awake()
        {
            _renderers = FindObjectsOfType<Renderer>();
            PostProcessingBloom.profile.TryGet(typeof(Bloom), out _bloom);
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
                item.shadowCastingMode = _isShadowsEnabled ? ShadowCastingMode.On : ShadowCastingMode.Off;
            }
        }

        private void OnBloomToggleButtonClicked()
        {
            _isBloomEnabled = !_isBloomEnabled;

            _bloom.active = _isBloomEnabled;
        }

        #endregion
    }
}