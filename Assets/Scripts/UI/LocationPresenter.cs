using System.Linq;
using UnityEngine;
using ItemData = ItemDatas.ItemData;


public class LocationPresenter : BasePresenter<LocationModel, LocationView>
{
  public void onInstallFurnitureClick()
  {
    spawnData();
  }
  
  public void onLocationSelectionClick()
  {
    Debug.LogError( "GG" );
  }

  private void spawnData()
  {
    ItemData data = model.getCurFurniture();
    
    view.prefabSpawner.spawnItems<UIFurnitureItem, int>(
      Enumerable.Range( 0, 1 )
    , ( a, b ) => a.init( data )
    , false
    );

    view.prefabSpawner.runSpawn();
  }
}