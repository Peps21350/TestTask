using System;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public static event Action onApplicationQuit = ( ) => { };
    
    private void OnApplicationQuit()
        => onApplicationQuit?.Invoke();

    protected override void onCreateService()
    {
        init();
    }
    
    private void init()
    {
        viewManager.addMiniGameView();
    }
}