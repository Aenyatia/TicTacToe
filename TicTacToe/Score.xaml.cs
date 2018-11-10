using NHibernate;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Core;
using TicTacToe.NHibernate;
using TicTacToe.ViewModels;

namespace TicTacToe
{
	public partial class Score
	{
		private readonly ISessionFactory _sessionFactory;

		public Score()
		{
			_sessionFactory = DataBase.GetSessionFactory();

			InitializeComponent();

			ScoreList.ItemsSource = GetBestScore();
		}

		private IEnumerable<ScoreViewModel> GetBestScore()
		{
			using (var session = _sessionFactory.OpenSession())
			using (var transaction = session.BeginTransaction())
			{
				var result = session.Query<Player>()
					.OrderByDescending(p => p.Win)
					.Take(10)
					.Select(p => new ScoreViewModel
					{
						Name = p.Name,
						Win = p.Win,
						Lose = p.Lose,
						Draw = p.Draw
					})
					.ToList();

				transaction.Commit();

				return result;
			}
		}
	}
}
