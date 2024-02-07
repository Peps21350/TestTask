using UnityEngine;
using ItemData = ItemDatas.ItemData;


public class UIFurnitureItem : PoolObject
{
    [SerializeField] private SpriteRenderer sprite_renderer = null;


    public void init( ItemData item_data )
    {
        sprite_renderer.sprite       = item_data.image;
        sprite_renderer.sortingOrder = item_data.order_in_layer;

        transform.localPosition = item_data.item_position;
        transform.localScale    = item_data.item_scale;
    }

    protected override void deinit()
    {
        base.deinit();

        sprite_renderer.sprite = null;
        sprite_renderer.sortingOrder = 0;
        
        transform.position = Vector3.zero;
        transform.localScale = Vector3.one;
    }
}
  