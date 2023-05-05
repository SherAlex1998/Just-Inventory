using Entity;
using ModestTree;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Utils;

namespace Entity
{
    public class ContainerEntity : ItemBaseEntity
    {
        [JsonProperty] private float maxWeight;
        [JsonProperty] private float maxVolume;
        [JsonProperty] private float netWeight;
        [JsonProperty] private float netVolume;

        [JsonProperty] private bool isContainerable;

        [JsonConverter(typeof(ItemListConverter))]
        [JsonProperty] protected List<ItemBaseEntity> items;

        [JsonIgnore] public bool IsContainerable { get => isContainerable; set => isContainerable = value; }

        private ContainerEntity parent;

        [JsonIgnore]
        public ContainerEntity Parent
        {
            get => parent;
            set
            {
                parent = value;
                parentName = parent.ItemName;
            }
        }

        [JsonProperty] private string parentName;
        public float MaxWeight { get => maxWeight; set => maxWeight = value; }
        public float MaxVolume { get => maxVolume; set => maxVolume = value; }
        public float NetWeight { get => netWeight; set => netWeight = value; }
        public float NetVolume { get => netVolume; set => netVolume = value; }
        [JsonIgnore] public string ParentName { get => parentName; set => parentName = value; }

        public void SetContainerData(ContainerData data)
        {
            itemName = data.ContainerName;
            itemDescription = data.ContainerDescription;
            weight = data.Weight;
            volume = data.Volume;
            maxWeight = data.MaxWeight;
            maxVolume = data.MaxVolume;
            netWeight = data.NetWeight;
            netVolume = data.NetVolume;
            isContainerable = data.IsContainerable;
            items = data.Items;
        }

        public void AddItem(ItemBaseEntity item)
        {
            try
            {
                UpdateWeightAndVolume(item);
                items.Add(item); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RemoveItem(ItemBaseEntity item)
        {
            UpdateWeightAndVolume(item, remooved: true);
            items.Remove(item);
        }

        public void UpdateWeightAndVolume(ItemBaseEntity newItem, bool remooved)
        {
            if (parent != null)
            {
                foreach (var item in parent.items)
                {
                    if (item is ContainerEntity)
                    {
                        (item as ContainerEntity).parent.UpdateWeightAndVolume(newItem, remooved: true);
                    }
                }
            }
            netWeight -= newItem.Weight;
            netVolume -= newItem.Volume;
        }

        public void UpdateWeightAndVolume(ItemBaseEntity newItem)
        {
            if (parent != null)
            {
                foreach (var item in parent.items)
                {
                    if (item is ContainerEntity)
                    {
                        (item as ContainerEntity).parent.UpdateWeightAndVolume(newItem);
                    }
                }
            }
            bool weightCheck = netWeight + newItem.Weight <= maxWeight;
            bool volumeCheck = netVolume + newItem.Volume <= maxVolume;
            if (!weightCheck)
            {
                throw new System.Exception("Weight exceeding");
            }
            if (!volumeCheck)
            {
                throw new System.Exception("Volume exceeding");
            }
            netWeight += newItem.Weight;
            netVolume += newItem.Volume;
            //items.Add(newItem);
        }

        public ItemBaseEntity[] GetItems()
        {
            return items.ToArray();
        }
    }

    public struct ContainerData
    {
        // Base attributes
        private string containerName;
        private string containerDescription;
        private float weight;
        private float volume;
        // Container attributes
        private float maxWeight;
        private float maxVolume;
        private float netWeight;
        private float netVolume;
        private bool isContainerable;
        List<ItemBaseEntity> items;

        public string ContainerName { get => containerName; set => containerName = value; }
        public string ContainerDescription { get => containerDescription; set => containerDescription = value; }
        public float Weight { get => weight; set => weight = value; }
        public float Volume { get => volume; set => volume = value; }
        public float MaxWeight { get => maxWeight; set => maxWeight = value; }
        public float MaxVolume { get => maxVolume; set => maxVolume = value; }
        public float NetWeight { get => netWeight; set => netWeight = value; }
        public float NetVolume { get => netVolume; set => netVolume = value; }
        public bool IsContainerable { get => isContainerable; set => isContainerable = value; }
        public List<ItemBaseEntity> Items { get => items; set => items = value; }
    }
}

