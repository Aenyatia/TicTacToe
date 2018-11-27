using NHibernate;
using System.Linq;
using System.Windows;
using TicTacToe.Core;
using TicTacToe.Extensions;
using TicTacToe.NHibernate;

namespace TicTacToe
{
	public partial class PlayersWindow
	{
		private readonly ISessionFactory _sessionFactory;

		public PlayersWindow()
		{
			_sessionFactory = Hibernate.GetSessionFactory();

			InitializeComponent();
		}

		private void StartGame_Click(object sender, RoutedEventArgs e)
		{
			var crossName = CrossName.Text.ToLowerInvariant();
			var circleName = CircleName.Text.ToLowerInvariant();

			if (crossName.IsEmpty() || circleName.Length > 20)
				return;

			if (circleName.IsEmpty() || circleName.Length > 20)
				return;

			if (crossName == circleName)
				return;

			var crossPlayer = GetPlayer(crossName);
			var circlePlayer = GetPlayer(circleName);

			new GameWindow(crossPlayer, circlePlayer)
				.Show();

			Close();
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
	}
}
