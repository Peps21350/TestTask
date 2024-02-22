public static class MyMath
{
  
  public static bool isInRange( in this int value, in int inclusive_min, in int exclusive_max )
  {
    return value >= inclusive_min && value < exclusive_max;
  }
}