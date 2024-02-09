using System.Collections.Generic;
using UnityEngine;
using LocationStatus = GameState.LocationStatus;
using ItemData = ItemDatas.ItemData;
using LocationData = LocationDatas.LocationData;


public class LocationModel : BaseModel
{
  protected CachedObject<LocationDatas> cached_location_datas = new( "GameData/LocationData" );
  
  protected LocationDatas location_data = default;
  protected GameState     game_state    = null;
  
  private LocationData cur_location_data = default;
  private ItemDatas    items_data        = default;

  public int furnitureInstalledAmount => game_state.cur_furniture_amount;

  public bool canInstallNextFurniture => items_data != null && items_data.item_data_array.Length >= game_state.cur_furniture_amount + 1 && cur_location_data.location_idx == game_state.cur_location_idx;
  
  public LocationModel()
  {
    game_state        = getCurGameState();
    cur_location_data = getLocationData();
    items_data        = cur_location_data.item_datas;
  }
  
  public ItemData? getCurFurniture( int idx )
  {
    List<ItemData> data = getSortedItemsData();
    if ( data == null || data.Count < game_state.cur_furniture_amount )
      return null;
      
    return data[idx];
  }

  public void incrementFurnitureAmount()
  {
    if ( items_data == null )
      return;

    if ( canInstallNextFurniture )
      game_state.incrementAmountInstalledFurniture();

    saveCurGameState();
  }

  public LocationData getLocationData()
  {
    if ( location_data == null )
      location_data = cached_location_datas.loadResources();
    
    cur_location_data = location_data.getLocationData( game_state.cur_location_idx ) ?? new LocationData();
    return cur_location_data;
  }

  public void updateData( LocationStatus location_status )
  {
    game_state.cur_furniture_amount = location_status.installed_furniture;
    game_state.cur_location_idx = location_status.location_idx;
    
    game_state.tryAddLocationStatus( game_state.createNextLocationStatus() );
    
    saveCurGameState();
  }
  
  private List<ItemData> getSortedItemsData()
  {
    if ( items_data == null )
      items_data = cur_location_data.item_datas;
    
    return items_data.getSortedItemDatas();
  }

  private GameState getCurGameState()
  {
    GameState data = GameStateJsonParser.deserializeGameStateJson( LocalSave.curGameState );

    return data ?? new GameState( new LocationStatus() );
  }

  protected void saveCurGameState()
  {
    LocalSave.curGameState = GameStateJsonParser.serializeGameStateJson( game_state );
  }
}