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
  
  public void onLocationSelectionClick()
  {
    Debug.LogError( "GG" );
  }

  public void setLocationSprite()
  {
    view.spriteRenderer.sprite = model.getLocationData().image; // todo add some fake image
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

    model.tryIncrementFurnitureNumber();
  }
}