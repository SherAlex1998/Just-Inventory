using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Entity;

namespace Presenter.Prefab
{
    public class SpaceItemPrefab : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI titleText;

        private SpaceEntity currentSpaceIntity;

        public void SetUp(SpaceEntity spaceIntity)
        {
            currentSpaceIntity = spaceIntity;
            titleText.text = currentSpaceIntity.Name.ToString();
        }
    }
}

