using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu( fileName = "ItemDatas", menuName = "ScriptableObjects/Data/ItemDatas", order = 1 )]
public class ItemDatas : ScriptableObject
{
  #region Serialize Fields
  [SerializeField] public ItemData[] item_data_array = null;
  #endregion
  
  #region Public Methods
  public List<ItemData> getSortedItemDatas() 
    => item_data_array.OrderBy( t => t.order_in_layer ).ToList();
  #endregion
  
  [Serializable]
  public struct ItemData
  {
    public string image_name;
    
    [Tooltip("Things that can be placed on top of others must have a higher order (number)")]
    [Range(1,100)]
    public short order_in_layer;

    public Vector2 item_position;
    public Vector2 item_scale;
  }

  [Serializable]
  public enum ItemType
  {
    NONE,

    TABLE,
    CHAIR,
    FLOWERPOT,
    PAINTING,
    LAMP,
    CLOCK,
    CARPET,
    TOY,
    ARMCHAIR,
    FIREPLACE,
    CHRISTMAS_TREE
  }
}