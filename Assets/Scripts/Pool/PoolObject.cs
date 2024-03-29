using System;
using UnityEngine.Pool;
using UnityEngine;

public class PoolObject : MonoBehaviourBase
{
    [NonSerialized] public ObjectPool<PoolObject> my_pool = null;
    
    public int dataIdx        { get; private set; }
    public int spawnPrefabID { get; private set; }

    public RectTransform rectTransform => transform as RectTransform;
    
    protected virtual void deinit() { }
    
    public void setSpawnPrefabId( int spawn_prefab_id )
    {
        spawnPrefabID = spawn_prefab_id;
    }
    
    public void setDataIdx( int data_idx )
    {
        dataIdx = data_idx;
    }
    
    public void despawn( DespawnType despawn_type = DespawnType.RELEASE )
    {
        if ( my_pool == null )
        {
            Debug.LogError( "Pool empty" );
            deinit();
            return;
        }
        
        if ( despawn_type == DespawnType.RELEASE )
            my_pool.Release( this );
        else
            my_pool.Dispose();

        deinit();
    }

    public void init( ObjectPool<PoolObject> pool )
    {
        initComponents();

        my_pool = pool;
    }
}


public enum DespawnType
{
    RELEASE,
    DESTROY
}
