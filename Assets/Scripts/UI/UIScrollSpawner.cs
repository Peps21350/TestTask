using System;
using System.Collections.Generic;


public class UIScrollSpawner : UIPrefabSpawner
{
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
    rootParent.anchoredPosition = getSpawnPrefab( getSpawnData( idx ).prefabID ).rectTransform.position;
  }
}
  