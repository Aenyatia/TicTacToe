using NHibernate;
using System.Linq;
using System.Windows;
using TicTacToe.Core;
using TicTacToe.Extensions;
using TicTacToe.NHibernate;

namespace TicTacToe
{
	public partial class SetPlayers
	{
		private readonly ISessionFactory _sessionFactory;

		public SetPlayers()
		{
			InitializeComponent();

			_sessionFactory = DataBase.GetSessionFactory();
		}

		private Player GetPlayer(string name)
		{
			using (var session = _sessionFactory.OpenSession())
			using (var transaction = session.BeginTransaction())
			{

				var player = session.Query<Player>().SingleOrDefault(p => p.Name == name);
				if (player != null)
					return player;

				player = Player.Create(name);
				session.SaveOrUpdate(player);
				transaction.Commit();

				return player;
			}

		}

		private void StartGame_Click(object sender, RoutedEventArgs e)
		{
			var crossPlayerName = CrossPlayerName.Text.ToLowerInvariant();
			var circlePlayerName = CirclePlayerName.Text.ToLowerInvariant();

			if (crossPlayerName.IsEmpty())
				return;

			if (circlePlayerName.IsEmpty())
				return;

			if (crossPlayerName == circlePlayerName)
				return;

			var crossPlayer = GetPlayer(crossPlayerName);
			var circlePlayer = GetPlayer(circlePlayerName);

			var game = new MainWindow(crossPlayer, circlePlayer);
			game.Show();

			Close();
		}

	}
}
