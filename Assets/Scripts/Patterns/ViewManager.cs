﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LocationStatus = GameState.LocationStatus;


public class ViewManager : MonoBehaviourSingleton<ViewManager>
{
  [SerializeField] private RectTransform root_transform_views = null;

  private readonly List<PoolObject> opened_views = new List<PoolObject>();
  
  public Coroutine addLocationView( LocationStatus location_status = null )
  {
    return addView<LocationView>( d => d.init( location_status ) );
  }
  
  public Coroutine addLocationSelectionView()
  {
    return addView<LocationSelectionView>( d => d.init() );
  }
  
  private Coroutine addView<TContainer>( Func<TContainer, IEnumerator> init_method ) 
    where TContainer : PoolObject 
  {
    return StartCoroutine( spawn() );

    IEnumerator spawn()
    {
      TContainer container = null;
      yield return loadContainer( item => container = item );

      if ( init_method != null )
        yield return init_method( container );

      tryCloseAnotherView();
      opened_views.Add( container );
    }

    IEnumerator loadContainer( Action<TContainer> callback )
    {
      callback( poolManager.spawnItem<TContainer>( root_transform_views ) );
      yield return null;
    }

    void tryCloseAnotherView()
    {
      if ( opened_views.Count > 0 )
      {
        PoolObject prev_view = opened_views.firstOrDefault();
        
        prev_view.despawn();

        opened_views.Remove( prev_view );
      }
    }
  }
}
