using System.Linq;
using UnityEngine;
using ItemData = ItemDatas.ItemData;


public class LocationPresenter : BasePresenter<LocationModel, LocationView>
{
  public bool canInstallNextFurniture => model.canInstallNextFurniture;
  
  public void onInstallFurnitureClick()
  {
    spawnData();

    model.tryIncrementFurnitureNumber();
    
    if ( !canInstallNextFurniture )
      view.setActiveBtnInstallFurniture( false );
  }

  public void setLocationSprite()
  {
    view.setLocationSprite( model.getLocationData().image_name );
  }

  private void spawnData()
  {
    ItemData? data = model.getCurFurniture();

    if ( data == null )
    {
      Debug.LogError( $"Data is null- {nameof(spawnData)}" );
      return;
    }
    
    view.prefabSpawner.spawnItems<UIFurnitureItem, int>(
      Enumerable.Range( 0, 1 )
    , ( a, b ) => a.init( data.Value )
    , false
    );

    view.prefabSpawner.runSpawn();
  }
}