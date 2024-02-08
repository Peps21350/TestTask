using UnityEngine;

public class LocalSave : MonoBehaviour
{
  #region Private Fields
  private const string KEY_LOCATION_NUMBER = "cur_location_number";
  private const string KEY_LOCATION_FURNITURE_NUMBER = "cur_location_{0}_number";
  #endregion

  
  #region Public Methods
  public static int curLocationNumber
  {
    get => getInt( KEY_LOCATION_NUMBER, 0 );
    set => setInt( KEY_LOCATION_NUMBER, value );
  }

  public static int getCurLocationFurnitureNumber( int cur_location_number )
  {
    return getInt( string.Format( KEY_LOCATION_FURNITURE_NUMBER, cur_location_number ), 0 );
  }
  
  public static void setCurLocationFurnitureNumber( int cur_location_number, int value )
  {
    setInt( string.Format( KEY_LOCATION_FURNITURE_NUMBER, cur_location_number ), value );
  }
  #endregion

  #region Private Methods
  private static void setInt( string key, int value )
    => PlayerPrefs.SetInt( key, value );

  private static int getInt( string key, int value )
    => PlayerPrefs.GetInt( key, value );
  #endregion
}
