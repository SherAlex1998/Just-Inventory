using Entity;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Animations;
using Utils;

namespace Entity
{
    public class ItemBaseEntity
    {

        [JsonProperty] protected string itemName;
        [JsonProperty] protected string itemDescription;
        [JsonProperty] protected int id;

        [JsonProperty] protected float weight;
        [JsonProperty] protected float volume;

        [JsonIgnore] public int Id { get => id; set => id = value; }
        [JsonIgnore] public string ItemName { get => itemName; set => itemName = value; }
        [JsonIgnore] public string ItemDescription { get => itemDescription; set => itemDescription = value; }
        [JsonIgnore] public float Weight { get => weight; set => weight = value; }
        [JsonIgnore] public float Volume { get => volume; set => volume = value; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
