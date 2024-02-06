using TMPro;


public class UITextMesh : TextMeshProUGUI
{
    public void setActive( bool state ) 
        => enabled = state;
    
    public void setText( string text ) 
        => SetText( text );

    public void setText( string text, bool force_auto_size ) 
        => SetText( text, force_auto_size );
}
