using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIScrollSpawner : UIPrefabSpawner
{
  [SerializeField] public ScrollRect scroll_rect = null;
  
  public new void spawnItems<TContainer, TData>(
    IEnumerable<TData> items
  , Action<TContainer, TData> init_method
  , bool despawn_items = true
  , int spawn_prefab_id = 0
  ) where TContainer : PoolObject
  {
    base.spawnItems( items, init_method, despawn_items, spawn_prefab_id );
  }

  public void scrollToItem( int idx )
  {
    Canvas.ForceUpdateCanvases();
    
    Vector2 view   = scroll_rect.viewport.localPosition;
    Vector2 target = getSpawnObject( idx ).rectTransform.localPosition;

    Vector2 new_position = new( 0,
      0 - ( view.y + target.y ) + scroll_rect.viewport.rect.height / 2 - getSpawnObject( idx ).rectTransform.rect.height / 2 );

    rootParent.localPosition = new_position;
  }
}
  