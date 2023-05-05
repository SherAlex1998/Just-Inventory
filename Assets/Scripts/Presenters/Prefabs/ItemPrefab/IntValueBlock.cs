using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Presenter.Prefab.Element
{
    public class IntValueBlock : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI valueText;

        private int currentValue;

        public void SetValue(int value)
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

        public void AddtoValue(int adder)
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

        public void SubtractFromTheValue(int subtrahend)
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
