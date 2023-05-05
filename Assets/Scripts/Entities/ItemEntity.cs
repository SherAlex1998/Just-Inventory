using Entity;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Entity
{
    public class ItemEntity : ItemBaseEntity, ICloneable
    {
        private ContainerEntity parent;

        [JsonIgnore] public ContainerEntity Parent 
        { get => parent; 
            set 
            { 
                parent = value;
                parentName = parent.ItemName;
            } 
        }

        public string ParentName { get => parentName; set => parentName = value; }

        [JsonProperty] private string parentName;

        public void SetItemData(ItemData data)
        {
            itemName = data.ItemName;
            itemDescription = data.ItemDescription;
            volume = data.Volume;
            weight = data.Weight;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public struct ItemData
    {
        private string itemName;
        private string itemDescription;
        private float weight;
        private float volume;

        public string ItemName { get => itemName; set => itemName = value; }
        public string ItemDescription { get => itemDescription; set => itemDescription = value; }
        public float Weight { get => weight; set => weight = value; }
        public float Volume { get => volume; set => volume = value; }
    }
}
