using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class AuxiliaryMethods 
{
	public static float fontSizeMultiplier;

	/// <summary>
	/// Randomizes a list
	/// </summary>
	/// <returns></returns>
	/// <param name="sequence"></param>
	/// <typeparam name="T"></typeparam>
	public static IEnumerable<T> OrderRandomly<T>(this IEnumerable<T> source)
	{
		System.Random random = new System.Random ();
		T [] copy = source.ToArray ();
		
		for (int i = copy.Length - 1; i >= 0; i--) {
			int index = random.Next (i + 1);
			yield return copy [index];
			copy [index] = copy [i];
		}
	}
}
