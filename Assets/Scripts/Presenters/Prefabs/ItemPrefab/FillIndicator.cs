using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Presenter.Prefab.Element
{
    [RequireComponent(typeof(Image))]
    public class FillIndicator : MonoBehaviour
    {
        [SerializeField] private Image fillingImage;
        [SerializeField] private TextMeshProUGUI indicatorText;
        [SerializeField] private string measure;
        [SerializeField] private float maxValue;
        [SerializeField] private float currentValue;

        public void SetMaxValue(float maxValue)
        {
            this.maxValue = maxValue;
            UpdateFilling();
        }

        public void SetCurrentValue(float currentValue)
        {
            this.currentValue = currentValue;
            UpdateFilling();
        }

        private void UpdateFilling()
        {
            float fraction = currentValue / maxValue;
            fillingImage.fillAmount = fraction;
            indicatorText.text = $"{currentValue}/{maxValue} {measure}";
        }
    }
}
