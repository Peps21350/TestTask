using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class UIPrefabSpawner : MonoBehaviourBase
{
  #region Serialize Fields
  [SerializeField] protected PoolObject[]  spawn_prefabs  = null;
  [SerializeField] protected RectTransform root_transform = null;
  #endregion

  #region Protected Fields
  protected RectTransform rootParent => root_transform;
  
  protected readonly List<SpawnData>        spawn_data        = new List<SpawnData>();
  private readonly   LinkedList<PoolObject> poolable_list     = new LinkedList<PoolObject>();
  private            int                    current_spawn_idx = -1;
  #endregion

  #region Public Fields
  public int dataCount => spawn_data.Count;
  #endregion
  

  #region Public Methods
  public void spawnItems<TContainer, TData>(
      IEnumerable<TData>        items
    , Action<TContainer, TData> init_method
    , bool                      despawn_items     = true
    , int                       spawn_prefab_id   = 0
  ) where TContainer : PoolObject
  {
    if ( items == null )
    {
      Debug.LogError( $"{nameof(spawnItems)} Collection of type '{typeof(TData).Name}' is null" );
      return;
    }

    initComponents();

    if ( despawn_items )
      despawnItems();

    foreach ( TData it in items )
    {
      if ( canSpawnItem( it ) )
        spawn_data.Add( new SpawnData( spawn_prefab_id, () => spawnInit( it ) ) );
    }

    if ( despawn_items )
      runSpawn();

    Transform spawnInit( TData item )
    {
      TContainer container = getOrSpawnItem<TContainer>( spawn_prefab_id );

      container.setDataIdx( current_spawn_idx );
      init_method( container, item );

      return container.transform;
    }
  }

  public void despawnItems( DespawnType despawn_type = DespawnType.RELEASE )
  {
    initComponents();

    while ( poolable_list.Count > 0 )
      despawnItem( poolable_list.First.Value, despawn_type );

    spawn_data.Clear();
    poolable_list.Clear();
    current_spawn_idx = -1;
  }

  public void runSpawn()
  { 
    startOrContinueSpawn( current_spawn_idx );
  }
  #endregion

  #region Protected Methods
  protected void startOrContinueSpawn( int current_spawn_idx )
  {
    this.current_spawn_idx = current_spawn_idx;
    
     StartCoroutine( spawnItemsCoroutine() );
  }

  protected SpawnData getSpawnData( int data_idx )
  {
    return spawn_data[data_idx];
  }

  protected PoolObject getSpawnPrefab( int prefab_id )
  {
    return spawn_prefabs[prefab_id];
  }

  protected virtual T tryGetPoolable<T>( int spawn_prefab_id )
    where T : PoolObject
  {
    T poolable = poolManager.spawnItem<T>( getSpawnPrefab( spawn_prefab_id ), root_transform );
    poolable.setSpawnPrefabId( spawn_prefab_id );
    
    poolable_list.AddLast( poolable );

    return poolable;
  }

  protected void despawnItem( PoolObject poolable_item, DespawnType despawn_type = DespawnType.RELEASE )
  {
    if ( poolable_item == null )
      return;

    poolable_list.Remove( poolable_item );
    poolable_item.despawn( despawn_type );
  }
  #endregion

  #region Private Methods
  private bool canSpawnItem<T>( T item )
  {
    if ( item == null )
    {
      Debug.LogWarning( $"Attempt to spawn null item of type '{typeof(T).Name}'" );
      return false;
    }

    return true;
  }

  private IEnumerator spawnItemsCoroutine()
  {
    if ( dataCount == 0 )
      yield break;
    
    for ( int i = 0; i < dataCount; i++ )
    {
      int next_spawn_idx = current_spawn_idx + 1;
      
      if ( !next_spawn_idx.isInRange( 0, dataCount ) )
        yield break;
      
      current_spawn_idx++;
      
      getSpawnData( current_spawn_idx ).funcAction();
    }
  }

  private T getOrSpawnItem<T>( int spawn_prefab_id )
    where T : PoolObject
  {
    return tryGetPoolable<T>( spawn_prefab_id );
  }
  #endregion

  #region OnDestroy
  protected override void OnDestroy()
  {
    base.OnDestroy();

    spawn_data.Clear();
  }
  #endregion
  
  #region Protected Class
  protected class SpawnData
  {
    #region Public Fields
    public int             prefabID   { get; private set; } = 0;
    public Func<Transform> funcAction { get; private set; } = null;
    #endregion


    #region Public Constructor
    public SpawnData( int prefab_id, Func<Transform> func_action )
    {
      this.prefabID   = prefab_id;
      this.funcAction = func_action;
    }
    #endregion
  }
  #endregion
}