using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using Entity;
using System.IO;
using Newtonsoft.Json.Linq;
using System;

namespace Utils
{
    public class SaveLoadManager
    {
        private const string spacesDirName = "Spaces";

        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter> { new ItemBaseEntityConverter(), new ItemListConverter() }
        };

        public void SaveSpacesLocal(List<SpaceEntity> spaces)
        {
            string path = Path.Combine(Application.persistentDataPath, spacesDirName);
            Directory.CreateDirectory(path);
            foreach (SpaceEntity space in spaces)
            {
                string JSONstring = JsonConvert.SerializeObject(space, settings);
                File.WriteAllText(Path.Combine(path, $"{space.Name}.json"), JSONstring);
            }
        }

        public List<SpaceEntity> LoadAllSpacesLocal()
        {
            string path = Path.Combine(Application.persistentDataPath, spacesDirName);
            List<SpaceEntity> spaces = new List<SpaceEntity>();
            string[] fileNames = Directory.GetFiles(path);
            foreach (string fileName in fileNames)
            {
                string JSONstring = File.ReadAllText(fileName);
                SpaceEntity newSpace = JsonConvert.DeserializeObject<SpaceEntity>(JSONstring, settings);
                spaces.Add(newSpace);
            }
            RelinkParentsAndChildren(spaces);
            return spaces;
        }

        private void RelinkParentsAndChildren(List<SpaceEntity> spaces)
        {
            foreach (var space in spaces)
            {
                foreach (var container in space.GetRootContainers())
                {
                    LinkContainerWithChildren(space, container);
                }
            }
        }

        private void LinkContainerWithChildren(SpaceEntity space, ContainerEntity container)
        {
            foreach (var item in container.GetItems())
            {
                if (item is ContainerEntity)
                {
                    LinkContainerWithChildren(space, item as ContainerEntity);
                    var parentContainer = FindParent(space, (item as ContainerEntity).ParentName);
                    if (parentContainer != null)
                        (item as ContainerEntity).Parent = parentContainer;
                }
                if (item is ItemEntity)
                {
                    var parentContainer = FindParent(space, (item as ItemEntity).ParentName);
                    if (parentContainer != null)
                        (item as ItemEntity).Parent = parentContainer;
                }
            }
        }

        private ContainerEntity FindParent(SpaceEntity space, string parentName)
        {
            foreach (var container in space.GetRootContainers())
            {
                if (container.ItemName == parentName) return container;
                foreach (var item in container.GetItems())
                {
                    if (item is ContainerEntity)
                    {
                        if (item.ItemName == parentName) return item as ContainerEntity;
                        return FindParent(item as ContainerEntity, parentName);
                    }
                    if (item is ItemEntity)
                    {
                        var parentContainer = FindParent(space, (item as ItemEntity).ParentName);
                        if (parentContainer != null)
                            (item as ItemEntity).Parent = parentContainer;
                    }
                }
            }
            return null;
        }

        private ContainerEntity FindParent(ContainerEntity container, string parentName)
        {
            foreach (var item in container.GetItems())
            {
                if (item is ContainerEntity)
                {
                    if (item.ItemName == parentName) return item as ContainerEntity;
                    return FindParent(item as ContainerEntity, parentName);
                }
            }
            return null;
        }
    }

    public class ItemBaseEntityConverter : JsonConverter<ItemBaseEntity>
    {
        public override bool CanWrite => true;

        public override ItemBaseEntity ReadJson(JsonReader reader, Type objectType, ItemBaseEntity existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);

            // Check if the JSON object has a property named "type"
            if (jsonObject.TryGetValue("type", out JToken typeToken) && typeToken.Type == JTokenType.String)
            {
                // Check if the value of the "type" property is "ItemEntity"
                if (typeToken.Value<string>() == "ItemEntity")
                {
                    return JsonConvert.DeserializeObject<ItemEntity>(jsonObject.ToString());
                }

                // Check if the value of the "type" property is "ContainerEntity"
                if (typeToken.Value<string>() == "ContainerEntity")
                {
                    return JsonConvert.DeserializeObject<ContainerEntity>(jsonObject.ToString());
                }
            }

            // If the "type" property is not present or its value is not recognized, deserialize as ItemBaseEntity
            return JsonConvert.DeserializeObject<ItemBaseEntity>(jsonObject.ToString());
        }

        public override void WriteJson(JsonWriter writer, ItemBaseEntity value, JsonSerializer serializer)
        {
            if (value is ItemEntity)
            {
                JObject jsonObject = JObject.FromObject(value);
                jsonObject.Add("type", "ItemEntity");
                jsonObject.WriteTo(writer);
            }
            else if (value is ContainerEntity)
            {
                //JObject jsonObject = JObject.FromObject(value);
                ContainerEntity container = (ContainerEntity)value;
                var jsonObject = SerializeContainer(container);
                jsonObject.WriteTo(writer);
            }
        }

        private JObject SerializeContainer(ContainerEntity container)
        {
            JObject jsonObject = new JObject
            {
                { "maxWeight", container.MaxWeight },
                { "maxVolume", container.MaxVolume },
                { "netWeight", container.NetWeight },
                { "netVolume", container.NetVolume },
                { "isContainerable", container.IsContainerable },
                { "parentName", container.ParentName },
                { "itemName", container.ItemName },
                { "itemDescription", container.ItemDescription },
                { "id", container.Id },
                { "weight", container.Weight },
                { "volume", container.Volume }
            };

            JArray itemsJson = new JArray();
            foreach (var item in container.GetItems())
            {
                if (item is ItemEntity)
                {
                    JObject itemJsonObject = JObject.FromObject(item);
                    itemJsonObject.Add("type", "ItemEntity");
                    itemsJson.Add(itemJsonObject);
                }
                if (item is ContainerEntity)
                {
                    var includedContainer = SerializeContainer(item as ContainerEntity);
                    itemsJson.Add(includedContainer);
                }
            }
            jsonObject.Add("items", itemsJson);

            jsonObject.Add("type", "ContainerEntity");

            return jsonObject;
        }
    }

    public class ItemListConverter : JsonConverter<List<ItemBaseEntity>>
    {
        public override List<ItemBaseEntity> ReadJson(JsonReader reader, Type objectType, List<ItemBaseEntity> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var itemList = new List<ItemBaseEntity>();
            JArray jsonArray = JArray.Load(reader);

            foreach (var item in jsonArray)
            {
                JObject jsonObject = JObject.Parse(item.ToString());

                if (jsonObject.TryGetValue("type", out JToken typeToken) && typeToken.Type == JTokenType.String)
                {
                    if (typeToken.Value<string>() == "ItemEntity")
                    {
                        itemList.Add(jsonObject.ToObject<ItemEntity>());
                    }
                    if (typeToken.Value<string>() == "ContainerEntity")
                    {
                        itemList.Add(jsonObject.ToObject<ContainerEntity>());
                    }
                }
            }

            return itemList;
        }

        public override void WriteJson(JsonWriter writer, List<ItemBaseEntity> value, JsonSerializer serializer)
        {
            JArray jsonArray = new JArray();

            foreach (var item in value)
            {
                jsonArray.Add(JObject.FromObject(item));
            }

            jsonArray.WriteTo(writer);
        }
    }
}
