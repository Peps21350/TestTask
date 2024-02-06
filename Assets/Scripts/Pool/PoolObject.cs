using System;
using UnityEngine.Pool;
using UnityEngine;

public class PoolObject : MonoBehaviourBase
{
    [NonSerialized] public ObjectPool<PoolObject> my_pool = null;
    
    public int data_idx        { get; private set; }
    public int spawn_prefab_id { get; private set; }
    
    protected virtual void deinit() { }
    
    public void setSpawnPrefabId( int spawn_prefab_id )
    {
        this.spawn_prefab_id = spawn_prefab_id;
    }
    
    public void setDataIdx( int data_idx )
    {
        this.data_idx = data_idx;
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
