using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class LocationSelectionPresenter : BasePresenter<LocationSelectionModel, LocationSelectionView>
{
  public void spawnData()
  {
    List<LocationDatas.LocationData> data = model.getAllLocationData();

    if ( data == null )
    {
      Debug.LogError( $"Data is null- {nameof(spawnData)}" );
      return;
    }
    
    view.scrollSpawner.spawnItems<UILocationSelectionItem, int>(
      Enumerable.Range( 0, data.Count )
    , ( a, b ) =>
      {
        Tuple<int, int> init_data = model.getLocationFurnitureData( b );
        
        a.init( init_data.Item1, init_data.Item2, data[b].image_name, data[b].name, data.Count - 1 - b, b );
      }, false
    );
    
    view.scrollSpawner.runSpawn();
    
    view.scrollSpawner.scrollToItem( data.Count - 1 );
  }
}