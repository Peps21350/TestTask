using System.Collections.Generic;
using Newtonsoft.Json;


public class GameState
{
  public List<LocationStatus> locations_status     = new List<LocationStatus>();
  public int                  cur_location_idx     = 0;
  public int                  cur_furniture_amount = 0;

  public GameState( LocationStatus location_status, int cur_location_idx = 0, int cur_furniture_amount = 0 )
  {
    this.cur_location_idx     = cur_location_idx;
    this.cur_furniture_amount = cur_furniture_amount;
    
    addLocationStatus( location_status );
  }
  
  public void incrementAmountInstalledFurniture()
  {
    cur_furniture_amount++;
    locations_status[cur_location_idx].incrementAmountInstalledFurniture();
  }

  public void tryAddLocationStatus( LocationStatus location_status )
  {
    if ( locations_status.Count - 1 < location_status.location_idx )
      addLocationStatus( location_status );
  }

  private void addLocationStatus( LocationStatus location_status )
  {
    if ( location_status != null )
      locations_status.Add( location_status );
  }

  public LocationStatus createCurLocationStatus()
  {
    return new LocationStatus( cur_location_idx, cur_furniture_amount );
  }


  public class LocationStatus
  {
    public int location_idx        = 0;
    public int installed_furniture = 0;

    public LocationStatus() { }

    public LocationStatus( int location_idx, int installed_furniture )
    {
      this.location_idx = location_idx;
      this.installed_furniture = installed_furniture;
    }

    public void incrementAmountInstalledFurniture()
    {
      installed_furniture++;
    }
  }
}


public static class GameStateJsonParser
{
  public static string serializeGameStateJson( GameState list_custom_offers )
    => JsonConvert.SerializeObject( list_custom_offers );

  public static GameState deserializeGameStateJson( string json_string )
    => JsonConvert.DeserializeObject<GameState>( json_string );
}