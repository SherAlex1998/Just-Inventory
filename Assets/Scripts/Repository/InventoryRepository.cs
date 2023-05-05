using Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Repository
{
    public class InventoryRepository
    {
        private SaveLoadManager saveLoadManager;

        public InventoryRepository()
        {
            saveLoadManager = new SaveLoadManager();
        }

        public List<SpaceEntity> GetSpaces()
        {
            return new List<SpaceEntity>();
        }

        public void SaveSpaces(List<SpaceEntity> spaces, Action<string> onSucces = null, Action<string> onError = null)
        {
            try
            {
                saveLoadManager.SaveSpacesLocal(spaces);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        public void LoadSpaces(Action<List<SpaceEntity>> onSucces, Action<string> onError)
        {
            List<SpaceEntity> loadedSpaces = new List<SpaceEntity>();
            try
            {
                loadedSpaces = saveLoadManager.LoadAllSpacesLocal();
                if (loadedSpaces.Count > 0)
                    onSucces?.Invoke(loadedSpaces);            
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }
    }
}

