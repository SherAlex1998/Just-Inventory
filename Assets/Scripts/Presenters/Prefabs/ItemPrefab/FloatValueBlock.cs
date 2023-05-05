using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Presenter.Prefab.Element
{
    public class FloatValueBlock : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI valueText;

        private float currentValue;

        public void SetValue(float value)
        {
            try
            {
                currentValue = value;
                valueText.text = value.ToString();
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        public void AddtoValue(float adder)
        {
            currentValue += adder;
            try
            {
                valueText.text = currentValue.ToString();
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        public void SubtractFromTheValue(float subtrahend)
        {
            currentValue -= subtrahend;
            try
            {
                valueText.text = currentValue.ToString();
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }
    }
}

