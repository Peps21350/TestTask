using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class LocationSelectionPresenter : BasePresenter<LocationSelectionModel, LocationSelectionView>
{
  public void spawnData()
  {
    List<LocationsData.LocationData> data = model.getAllLocationData();

    if ( data == null )
    {
      Debug.LogError( $"Data is null- {nameof(spawnData)}" );
      return;
    }

    int cur_location_index = -1;
    
    view.scrollSpawner.spawnItems<UILocationSelectionItem, int>(
      Enumerable.Range( 0, data.Count )
    , ( a, b ) =>
      {
        (int amount_installed_furniture, int cur_location_idx) = model.getCurAmountInstalledFurnitureAndLocationIdx( b );

        if ( cur_location_index < 0 ) 
          cur_location_index = cur_location_idx;

        a.init( data[b], amount_installed_furniture, cur_location_idx, model.isLocationOpened( b ), data.Count - 1 - b );
      }, false
    );
    
    view.scrollSpawner.runSpawn( true );
    
    view.scrollSpawner.scrollToItem( data.Count - 1 - cur_location_index );
  }
}