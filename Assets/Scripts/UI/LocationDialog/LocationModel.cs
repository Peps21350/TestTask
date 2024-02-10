using System.Collections.Generic;
using LocationStatus = GameState.LocationStatus;
using ItemData = ItemDatas.ItemData;
using LocationData = LocationsData.LocationData;


public class LocationModel : BaseModel
{
  private CachedObject<LocationsData> cached_location_datas = new( "GameData/LocationData" );
  
  protected LocationsData locations_data = default;
  protected GameState     game_state    = null;
  
  private LocationData cur_location_data = default;

  public int furnitureInstalledAmount => game_state.cur_furniture_amount;

  public bool canInstallNextFurniture 
    => cur_location_data.getItemDatasAmount() >= game_state.cur_furniture_amount + 1 
       && cur_location_data.location_idx == game_state.cur_location_idx;
  
  public LocationModel()
  {
    game_state        = getCurGameState();
    cur_location_data = getLocationData();
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
    if ( canInstallNextFurniture )
      game_state.incrementAmountInstalledFurniture();

    if ( !canInstallNextFurniture )
      updateData( new LocationStatus( game_state.cur_location_idx + 1, 0 ) );

    saveCurGameState();
  }

  public LocationData getLocationData()
  {
    if ( locations_data == null )
      locations_data = cached_location_datas.loadResources();
    
    cur_location_data = locations_data.getLocationData( game_state.cur_location_idx ) ?? new LocationData();
    return cur_location_data;
  }

  public void updateData( LocationStatus location_status )
  {
    getLocationData();
    
    if ( game_state.cur_furniture_amount == location_status.installed_furniture && game_state.cur_location_idx == location_status.location_idx )
      return;
    
    game_state.cur_furniture_amount = location_status.installed_furniture;
    game_state.cur_location_idx = location_status.location_idx;
    
    game_state.tryAddLocationStatus( game_state.createCurLocationStatus() );
    
    saveCurGameState();
  }
  
  private List<ItemData> getSortedItemsData()
  {
    return cur_location_data.item_datas.getSortedItemDatas();
  }

  private GameState getCurGameState()
  {
    GameState data = GameStateJsonParser.deserializeGameStateJson( LocalSave.curGameState );

    return data ?? new GameState( new LocationStatus() );
  }

  private void saveCurGameState()
  {
    LocalSave.curGameState = GameStateJsonParser.serializeGameStateJson( game_state );
  }
}