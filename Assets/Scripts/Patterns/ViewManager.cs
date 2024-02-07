using System;
using System.Collections;
using UnityEngine;


public class ViewManager : MonoBehaviourSingleton<ViewManager>
{
  public RectTransform root_transform_views = null;
  
  public Coroutine addMiniGameView()
  {
    return addDialog<LocationView>( d => d.init() );
  }
  
  private Coroutine addDialog<TContainer>( Func<TContainer, IEnumerator> init_method ) 
    where TContainer : PoolObject 
  {
    return StartCoroutine( spawn() );

    IEnumerator spawn()
    {
      TContainer container = null;
      yield return loadContainer( item => container = item );

      if ( init_method != null )
        yield return init_method( container );
    }

    IEnumerator loadContainer( Action<TContainer> callback )
    {
      callback( poolManager.spawnItem<TContainer>( root_transform_views ) );
      yield return null;
    }
  }
}
