using System.Collections.Generic;

namespace SquadForger.Repository
{
	public interface IRandomPicker
	{
		List<string> GetRandom(List<string> input, int amount);
	}
}
