using UnityEngine;
using ItemData = ItemDatas.ItemData;


public class UIFurnitureItem : PoolObject
{
  [SerializeField] private SpriteRenderer sprite_renderer = null;

  //private const float DEFAULT_SCREEN_WIDTH  = 1080f;
  //private const float DEFAULT_SCREEN_HEIGHT = 1920f;

  public void init( ItemData item_data )
  {
    sprite_renderer.sprite = spriteManager.atlas_furniture.getSprite( item_data.image_name );
    sprite_renderer.sortingOrder = item_data.order_in_layer;

    transform.localPosition = item_data.item_position;
    transform.localScale = item_data.item_scale;

    //updateItemScale();
    //updateItemPosition();
  }

  /*private void updateItemScale()
  {
    float scaleX = Screen.width / DEFAULT_SCREEN_WIDTH;
    float scaleY = Screen.height / DEFAULT_SCREEN_HEIGHT;

    transform.localScale = new Vector3( transform.localScale.x * scaleX, transform.localScale.y * scaleY, 1 );
  }

  private void updateItemPosition()
  {
    float scaleX = Screen.width / DEFAULT_SCREEN_WIDTH;
    float scaleY = Screen.height / DEFAULT_SCREEN_HEIGHT;

    transform.localPosition = new Vector3( getPosition(), getPosition( false ), 1 );
    return;

    float getPosition( bool isX = true )
    {
      float position = isX ? transform.localPosition.x : transform.localPosition.y;
      float scale    = isX ? scaleX : scaleY;

      if (scale > 1)
        return position / scale;

      return position * scale;
    }
  }*/

  protected override void deinit()
  {
    base.deinit();

    sprite_renderer.sprite = null;
    sprite_renderer.sortingOrder = 0;

    transform.position = Vector3.zero;
    transform.localScale = Vector3.one;
  }
}