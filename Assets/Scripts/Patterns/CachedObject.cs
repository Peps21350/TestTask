using UnityEngine;
using System;
using System.Collections;


[Serializable]
public class CachedObject<T>
  where T : UnityEngine.Object
{
  #region Serialize Fields
  [SerializeField] private T cached_obj = null;
  #endregion

  #region Private Fields
  private bool   is_resources_asset = false; // for asset in Resources folder
  private string cached_name        = null;
  #endregion

  #region Public Fields
  public readonly Type type = typeof( T );

  public T    cachedObj        => cached_obj;
  public bool isResourcesAsset => is_resources_asset;
  #endregion


  #region Public Methods
  public T loadResources()
  {
    is_resources_asset = true;

    if ( cached_obj == null )
      cached_obj = Resources.Load<T>( cached_name );

    return cached_obj;
  }

  public IEnumerator loadResourcesAsync( Action<T> callback )
  {
    is_resources_asset = true;

    if ( cached_obj == null )
    {
      ResourceRequest resource_request = Resources.LoadAsync<T>( cached_name );
      yield return resource_request;

      if ( resource_request.asset == null )
        throw new ResourceLoadingException( cached_name );

      cached_obj = resource_request.asset as T;
    }

    callback?.Invoke( cached_obj );
  }

  public void destroy()
  {
    onRelease( cached_obj );
    cached_obj = null;
  }
  #endregion

  #region Protected Methods
  protected virtual void onRelease( T asset ) { }
  #endregion

  #region Public Constructor
  public CachedObject( string cached_name )
  {
    this.cached_name = cached_name;
  }
  #endregion

  #region Public Class
  public class ResourceLoadingException : Exception
  {
    public ResourceLoadingException( string msg ) : base( msg ) { }
  }
  #endregion
}