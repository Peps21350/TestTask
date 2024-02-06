using System;
using System.Collections.Generic;


public class EventAction
{
  private LinkedList<Action> delegates = null;


  public int count => delegates?.Count ?? 0;
  public bool any  => count > 0;


  public void add( Action action )
  {
    if ( delegates == null )
      delegates = new LinkedList<Action>();

    delegates.AddLast( action );
  }

  public void remove( Action action )
  {
    if ( delegates == null )
      return;

    LinkedListNode<Action> node = delegates.First;
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

  public void call()
  {
    if ( delegates == null )
      return;

    LinkedListNode<Action> node = delegates.First;
    while ( node != null )
    {
      node.Value();
      node = node.Next;
    }
  }
}

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

public class EventAction<T1, T2>
{
  private LinkedList<Action<T1, T2>> delegates = null;


  public int count => delegates?.Count ?? 0;
  public bool any  => count > 0;


  public void add( Action<T1, T2> action )
  {
    if ( delegates == null )
      delegates = new LinkedList<Action<T1, T2>>();

    delegates.AddLast( action );
  }

  public void remove( Action<T1, T2> action )
  {
    if ( delegates == null )
      return;

    LinkedListNode<Action<T1, T2>> node = delegates.First;
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

  public void call( T1 value_1, T2 value_2 )
  {
    if ( delegates == null )
      return;

    LinkedListNode<Action<T1, T2>> node = delegates.First;
    while ( node != null )
    {
      node.Value( value_1, value_2 );
      node = node.Next;
    }
  }
}

public class EventAction<T1, T2, T3>
{
  private LinkedList<Action<T1, T2, T3>> delegates = null;


  public int count => delegates?.Count ?? 0;
  public bool any  => count > 0;


  public void add( Action<T1, T2, T3> action )
  {
    if ( delegates == null )
      delegates = new LinkedList<Action<T1, T2, T3>>();

    delegates.AddLast( action );
  }

  public void remove( Action<T1, T2, T3> action )
  {
    if ( delegates == null )
      return;

    LinkedListNode<Action<T1, T2, T3>> node = delegates.First;
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

  public void call( T1 value_1, T2 value_2, T3 value_3 )
  {
    if ( delegates == null )
      return;

    LinkedListNode<Action<T1, T2, T3>> node = delegates.First;
    while ( node != null )
    {
      node.Value( value_1, value_2, value_3 );
      node = node.Next;
    }
  }
}
