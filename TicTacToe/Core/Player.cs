namespace TicTacToe.Core
{
	public class Player
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public Score Score { get; set; }

		public Player()
		{
			Score = new Score();
		}
	}
}
