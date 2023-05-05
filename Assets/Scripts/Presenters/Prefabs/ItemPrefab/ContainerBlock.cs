using Entity;
using Presenter.Prefab.Element;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Presenter.Prefab
{
    public class ContainerBlock : ItemPrefab
    {
        [SerializeField] FloatValueBlock maxWeightBlock;
        [SerializeField] FloatValueBlock maxVolumeBlock;

        public void SetUp(ContainerEntity item)
        {
            weightBlock.SetValue(item.Weight);
            volumeBlock.SetValue(item.Volume);
            descriptionText.text = item.ItemDescription;
            maxWeightBlock.SetValue(item.MaxWeight);
            maxVolumeBlock.SetValue(item.MaxVolume);
        }
    }
}