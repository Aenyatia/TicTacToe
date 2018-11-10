using FluentNHibernate.Mapping;
using TicTacToe.Core;

namespace TicTacToe.NHibernate
{
	public class PlayerMap : ClassMap<Player>
	{
		public PlayerMap()
		{
			Id(x => x.Id);
			Map(x => x.Name);
			Map(x => x.Win);
			Map(x => x.Lose);
			Map(x => x.Draw);
		}
	}
}
