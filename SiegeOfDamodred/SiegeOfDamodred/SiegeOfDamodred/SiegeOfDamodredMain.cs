using System;

namespace SiegeOfDamodred
{
#if WINDOWS || XBOX

	static class SiegeOfDamodredMain
	{
		static void Main(string[] args)
		{
			using (GameLoop game = new GameLoop())
			{
				game.Run();
			}
		}
	}
#endif
}
