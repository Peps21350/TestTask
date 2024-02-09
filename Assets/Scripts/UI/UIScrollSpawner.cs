using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIScrollSpawner : UIPrefabSpawner
{
  [SerializeField] private LayoutGroup layout_group = null;
  [SerializeField] private ScrollRect  scroll_rect  = null;


  public void spawnItems<TContainer, TData>(
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
    rootParent.anchoredPosition = getSpawnPrefab( getSpawnData( idx ).prefab_id ).rectTransform.position;
  }
}
  