using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu( fileName = "ItemDatas", menuName = "ScriptableObjects/Data/ItemDatas", order = 1 )]
public class ItemDatas : ScriptableObject
{
  [SerializeField] public ItemData[] item_datas = null;
  
  
  #region Public Methods
  public List<ItemData> getSortedItemDatas() 
    => item_datas.OrderBy( t => t.order_in_layer ).ToList();
  #endregion
  
  
  [Serializable]
  public struct ItemData
  {
    public ItemType item_type;
    public Sprite   image; // todo update this part
    
    [Tooltip("Things that can be placed on top of others must have a higher order (number)")]
    [Range(1,100)]
    public short order_in_layer;

    public Vector2 item_position;
    public Vector2 item_scale;
    public bool    is_spawned;
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