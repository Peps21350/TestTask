using System.Collections.Generic;
using LocationData = LocationsData.LocationData;


public class LocationSelectionModel : LocationModel
{
  public List<LocationData> getAllLocationData()
  {
    if ( locations_data != null )
      return locations_data.getSortedLocationsData();

    getLocationData();
    
    return locations_data.getSortedLocationsData();
  }

  public (int, int) getCurAmountInstalledFurnitureAndLocationIdx( int location_idx )
  {
    if ( location_idx == game_state.cur_location_idx )
      return (game_state.cur_furniture_amount, game_state.cur_location_idx);

    return game_state.locations_status.Count < location_idx + 1
      ? (0, game_state.cur_location_idx )
      : (game_state.locations_status[location_idx].installed_furniture, game_state.cur_location_idx );
  }

  public bool isLocationOpened( int location_idx )
  {
    int prev_location_index = location_idx - 1;
    
    if ( prev_location_index < 0 )
      return true;

    if ( game_state.locations_status.Count < prev_location_index + 1 )
      return false;
    
    return game_state.locations_status[prev_location_index].installed_furniture == locations_data.getSortedLocationsData()[prev_location_index].getItemDatasAmount();
  }
}