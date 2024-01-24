using System;
using System.Collections.Generic;

namespace SquadForger.Repository
{
	public class DefaultChampionPicker : IRandomPicker
	{
		private static readonly Random Random = new Random();

		public List<string> GetRandom(List<string> input, int amount)
		{
			if (input.Count < amount)
			{
				throw new ArgumentException("amount is higher than input.Count, makes it impossible to give unique randoms");
			}
			List<string> pickedList = new List<string>();
			string pick;
			for (int i = 0; i < amount; ++i)
			{
				do
				{
					pick = input[Random.Next() % input.Count];
				} while (pickedList.Contains(pick));
				pickedList.Add(pick);
			}
			return pickedList;
		}
	}
}
