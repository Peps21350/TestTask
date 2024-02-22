using System.Collections.Generic;
using System.Linq;
using UnityEngine.Pool;
using UnityEngine;


public class PoolManager : MonoBehaviourSingleton<PoolManager>
{
  public GameObject[]  objectsPrefab = null;

  #region Private Fields
  private readonly Dictionary<PoolObject, ObjectPool<PoolObject>> pool_holders = new();
  
  private Transform transform_root = null;
  #endregion


  public T spawnItem<T>( PoolObject pool_object, Transform transform_root = null ) where T : PoolObject
  {
    if ( pool_object == null )
    {
      Debug.LogError( "Cant spawn item, item is null" );
      return null;
    }

    this.transform_root = transform_root;

    T cur_object = getOrCreatePoolHolder( pool_object ).Get().GetComponent<T>();

    return cur_object;
  }
  
  public T spawnItem<T>( Transform transform_root ) where T : PoolObject
  {
    T cur_game_object = objectsPrefab?.FirstOrDefault( t => t.name.ToLower().Equals( typeof( T ).ToString().ToLower() ) )?.GetComponent<T>();

    return spawnItem<T>( cur_game_object, transform_root );
  }

  public void release( PoolObject pool_object )
  {
    if ( pool_object == null )
      return;

    getOrCreatePoolHolder( pool_object ).Release( pool_object );
  }

  private PoolObject OnObjectCreate( PoolObject pool_item )
  {
    PoolObject newObject = Instantiate( pool_item, transform_root != null ? transform_root : transform, worldPositionStays: pool_item is UIFurnitureItem );

    return newObject;
  }

  private ObjectPool<PoolObject> getOrCreatePoolHolder( PoolObject pool_item )
  {
    ObjectPool<PoolObject> pool_holder = getPoolHolder( pool_item ) ?? createPoolHolder( pool_item );

    return pool_holder;
  }

  private ObjectPool<PoolObject> createPoolHolder( PoolObject pool_item )
  {
    ObjectPool<PoolObject> new_pool_holder = new( () => OnObjectCreate( pool_item ), OnTake, OnRelease, OnObjectDestroy );

    pool_holders.Add( pool_item, new_pool_holder );

    return new_pool_holder;
  }

  private ObjectPool<PoolObject> getPoolHolder( PoolObject pool_item )
  {
    if ( pool_item.my_pool != null )
      return pool_item.my_pool;

    return pool_holders.TryGetValue( pool_item, out ObjectPool<PoolObject> pool_holder ) ? pool_holder : null;
  }

  private void OnTake( PoolObject poolObject )
  {
    poolObject.gameObject.SetActive( true );
    poolObject.GetComponent<PoolObject>().init( getOrCreatePoolHolder( poolObject ) );
    poolObject.GetComponent<CanvasGroup>()?.setVisible( true );
  }

  private void OnRelease( PoolObject poolObject )
  {
    poolObject.gameObject.SetActive( false );
  }

  private void OnObjectDestroy( PoolObject poolObject )
  {
    Destroy( poolObject );
  }
}