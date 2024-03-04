using System.Collections;
using TMPro;
using UnityEngine;

namespace DemoURP.UI
{
    public class FPSCounter : MonoBehaviour
    {
        #region serialize fields

        [SerializeField] private TextMeshProUGUI CounterLabel;

        #endregion

        #region fields

        private string _counterLabelFormat;
        private Coroutine _updateCounterCoroutine;
        private float _deltaTime;

        #endregion

        #region engine metods

        private void Awake()
        {
            _counterLabelFormat = CounterLabel.text;
        }

        private void Start()
        {
            _updateCounterCoroutine = StartCoroutine(OnUpdateCounter());
        }

        private void OnDestroy()
        {
            StopCoroutine(_updateCounterCoroutine);
        }

        #endregion

        #region private methods

        private IEnumerator OnUpdateCounter()
        {
            while (true)
            {
                _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
                int fpsValue = Mathf.FloorToInt(1.0f / _deltaTime);
                CounterLabel.text = string.Format(_counterLabelFormat, fpsValue);

                yield return new WaitForSeconds(1f);
            }

            yield return null;
        }
    }

    #endregion
}