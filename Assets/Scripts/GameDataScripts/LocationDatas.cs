using System;
using System.Linq;
using UnityEngine;


[CreateAssetMenu( fileName = "LocationData", menuName = "ScriptableObjects/Data/LocationData", order = 1 )]
public class LocationDatas : ScriptableObject
{
  #region Serialize Fields
  [SerializeField] public LocationData[] location_datas = null;
  #endregion
  
  #region Public Methods
  public LocationData? getLocationData( int location_number ) 
    => location_datas.FirstOrDefault( it => it.location_number == location_number );

  public void setLocationCompleted( int location_number )
  {
    LocationData? location_data = location_datas.FirstOrDefault( it => it.location_number == location_number );

    if ( location_data.HasValue )
    {
      LocationData cur_data = location_data.Value;
      cur_data.is_completed = true;
      location_datas[location_number] = cur_data;
    }
  }
  #endregion

  [Serializable]
  public struct LocationData
  {
    public short        location_number;
    public LocationType location_type;
    public ItemDatas    item_datas;
    public Sprite       image;
    public string       name;
    public bool         is_completed;
    public short        furniture_progress;
  }

  [Serializable]
  public enum LocationType
  {
    REAL,
    FAKE
  }
}