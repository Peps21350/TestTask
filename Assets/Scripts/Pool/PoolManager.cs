using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Pool;
using UnityEngine;


public class PoolManager : MonoBehaviourSingleton<PoolManager>
{
  public GameObject[] objectsPrefab = null;

  #region Private Fields
  private readonly Dictionary<Type, ObjectPool<PoolObject>> pool_holders = new Dictionary<Type, ObjectPool<PoolObject>>();

  private PoolObject cur_game_object = null;

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
    
    cur_game_object = pool_object;

    T cur_object = getOrCreatePoolHolder( cur_game_object ).Get().GetComponent<T>();

    return cur_object;
  }
  
  public T spawnItem<T>() where T : PoolObject
  {
    T cur_game_object = objectsPrefab?.FirstOrDefault( t => t.name.ToLower().Equals( typeof(T).ToString().ToLower() ) )?.GetComponent<T>();

    return spawnItem<T>( cur_game_object );
  }

  public void release( PoolObject pool_object )
  {
    if ( pool_object == null )
      return;
    
    getOrCreatePoolHolder( pool_object ).Release( pool_object );
  }
  
  private PoolObject OnObjectCreate()
  {
    PoolObject newObject = Instantiate( cur_game_object, transform_root != null ? transform_root : transform );
    
    return newObject;
  }

  private ObjectPool<PoolObject> getOrCreatePoolHolder( PoolObject pool_item )
  {
    ObjectPool<PoolObject> pool_holder = getPoolHolder( pool_item ) ?? createPoolHolder( pool_item );

    return pool_holder;
  }
  
  private ObjectPool<PoolObject> createPoolHolder( PoolObject pool_item )
  {
    ObjectPool<PoolObject> new_pool_holder = new( OnObjectCreate, OnTake, OnRelease, OnObjectDestroy );
    
    pool_holders.Add( pool_item.GetType(), new_pool_holder );

    return new_pool_holder;
  }
  
  private ObjectPool<PoolObject> getPoolHolder( PoolObject pool_item )
  {
    if ( pool_item.my_pool != null )
      return pool_item.my_pool;

    return pool_holders.TryGetValue( pool_item.GetType(), out ObjectPool<PoolObject> pool_holder ) ? pool_holder : null;
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