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
      Enumerable.Range( 0, 10 )
    , ( a, b ) => a.init( getProgress(b), data[0].item_datas.item_datas.Length, "" )
    , false
    );

    int getProgress( int idx )
    {
      if ( idx == 0 )
        return 20;

      return 0;
    }

    view.scrollSpawner.runSpawn();
    view.scrollSpawner.scrollToItem( 9 );
  }
}