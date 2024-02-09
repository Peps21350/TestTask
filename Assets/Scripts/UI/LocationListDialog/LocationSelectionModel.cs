using System;
using System.Collections.Generic;
using LocationData = LocationDatas.LocationData;


public class LocationSelectionModel : LocationModel
{
  private List<LocationData> locations_data = default;
  
  public LocationSelectionModel()
  {
    locations_data = getAllLocationData();
  }
  
  public List<LocationData> getAllLocationData()
  {
    if ( location_data == null )
      location_data = cached_location_datas.loadResources();
    
    return location_data.getAllLocationData();
  }
  
  public Tuple<int, int> getLocationFurnitureData( int location_idx )
  {
    if ( location_idx == game_state.cur_location_idx )
      return new Tuple<int, int>( game_state.cur_furniture_amount, locations_data[location_idx].item_datas.item_data_array.Length );
    
    return game_state.locations_status.Count < location_idx + 1 
      ? new Tuple<int, int>( 0, locations_data[location_idx].item_datas.item_data_array.Length ) 
      : new Tuple<int, int>( game_state.locations_status[location_idx].installed_furniture, locations_data[location_idx].item_datas.item_data_array.Length );
  }
}