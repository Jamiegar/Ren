using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ren.Items
{
    
    [CreateAssetMenu(fileName = "New Item Sprite Data", menuName = "Item Data/Sprite Data")]
    public class ItemSpriteData : ScriptableObject
    {
        public Sprite ItemWorldSprite;
        [SortingLayer] public string LayerName;
        public int OrderInLayer;

        public void SetupSprite(SpriteRenderer Renderer)
        {
            Renderer.sprite = ItemWorldSprite;
            Renderer.sortingLayerName = LayerName;
            Renderer.sortingOrder = OrderInLayer;
        }
    }
}
