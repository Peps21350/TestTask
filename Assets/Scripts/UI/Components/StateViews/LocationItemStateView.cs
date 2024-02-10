using UnityEngine.UI;


namespace UI
{

  public class LocationItemStateView : StateView<LocationItemStateView.LocationItemStateViewEnum>
  {
    public void initLocationItemState( LocationItemStateViewEnum location_item_state_view )
    {
      setState( location_item_state_view );
    }

    public enum LocationItemStateViewEnum
    {
      OPENED,
      CLOSED,
      CURRENT_REAL,
      CURRENT_FAKE
    }
  }
}