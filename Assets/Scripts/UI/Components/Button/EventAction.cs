using System;
using System.Collections.Generic;


public class EventAction<T>
{
  private LinkedList<Action<T>> delegates = null;


  public int count => delegates?.Count ?? 0;
  public bool any  => count > 0;


  public void add( Action<T> action )
  {
    if ( delegates == null )
      delegates = new LinkedList<Action<T>>();

    delegates.AddLast( action );
  }

  public void remove( Action<T> action )
  {
    if ( delegates == null )
      return;

    LinkedListNode<Action<T>> node = delegates.First;
    while ( node != null )
    {
      if ( Equals( node.Value, action ) )
      {
        delegates.Remove( node );
        return;
      }

      node = node.Next;
    }
  }

  public void clear()
  {
    if ( delegates == null )
      return;

    delegates.Clear();
  }

  public void call( T value )
  {
    if ( delegates == null )
      return;

    LinkedListNode<Action<T>> node = delegates.First;
    while ( node != null )
    {
      node.Value( value );
      node = node.Next;
    }
  }
}
