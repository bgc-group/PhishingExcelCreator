using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Lib.ListExtensions
{
  public static class ListExtensions
  {
    /// <summary>
    /// https://stackoverflow.com/questions/273313/randomize-a-listt#1262619
    /// </summary>
    public static void Shuffle<T>(this List<T> list)
    {
      RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
      int n = list.Count;
      while (n > 1)
      {
        byte[] box = new byte[1];
        do provider.GetBytes(box);
        while (!(box[0] < n * (Byte.MaxValue / n)));
        int k = (box[0] % n);
        n--;
        T value = list[k];
        list[k] = list[n];
        list[n] = value;
      }
    }
  }
}