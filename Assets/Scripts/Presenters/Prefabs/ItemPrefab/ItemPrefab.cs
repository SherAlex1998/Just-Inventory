using Entity;
using Presenter.Prefab.Element;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Presenter.Prefab
{
    public class ItemPrefab : MonoBehaviour
    {
        [SerializeField] protected FloatValueBlock weightBlock;
        [SerializeField] protected FloatValueBlock volumeBlock;
        [SerializeField] protected TextMeshProUGUI descriptionText;

        public void SetUp(ItemEntity item)
        {
            weightBlock.SetValue(item.Weight);
            volumeBlock.SetValue(item.Volume);
            descriptionText.text = item.ItemDescription;
        }
    }
}
