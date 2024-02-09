using System.Linq;
using UnityEngine;
using ItemData = ItemDatas.ItemData;
using LocationStatus = GameState.LocationStatus;


public class LocationPresenter : BasePresenter<LocationModel, LocationView>
{
  public bool canInstallNextFurniture => model.canInstallNextFurniture;
  
  public void onInstallFurnitureClick()
  {
    spawnData( single_spawn: true );

    model.incrementFurnitureAmount();
    
    if ( !canInstallNextFurniture )
      view.setActiveBtnInstallFurniture( false );
  }

  public void setLocationSprite()
  {
    view.setLocationSprite( model.getLocationData().image_name );
  }

  public void updateData( LocationStatus location_status )
  {
    model.updateData( location_status );
  }

  public void spawnData( int amount_installed_furniture = 1, bool single_spawn = false )
  {
    if ( amount_installed_furniture < 0 )
      amount_installed_furniture = model.furnitureInstalledAmount;
    
    if ( amount_installed_furniture == 0 )
      return;
    
    view.prefabSpawner.spawnItems<UIFurnitureItem, int>(
      Enumerable.Range( 0, amount_installed_furniture )
    , ( a, b ) =>
      {
        ItemData? data = model.getCurFurniture( single_spawn ? model.furnitureInstalledAmount : b );
        
        if ( data == null )
        {
          Debug.LogError( $"Data is null- {nameof(spawnData)}" );
          return;
        }
        
        a.init( data.Value );
      }, false
    );

    view.prefabSpawner.runSpawn();
  }
}