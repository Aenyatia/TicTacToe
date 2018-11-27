using NHibernate;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TicTacToe.Core;
using TicTacToe.NHibernate;
using TicTacToe.Resources;

namespace TicTacToe
{
	public partial class GameWindow
	{
		private readonly ISessionFactory _sessionFactory;
		private readonly MarkType[] _board = new MarkType[9];

		private readonly Player _crossPlayer;
		private readonly Player _circlePlayer;

		private bool _isGameOver;
		private bool _isDraw;
		private bool _isCrossTurn;

		public GameWindow(Player crossPlayer, Player circlePlayer)
		{
			_crossPlayer = crossPlayer ?? throw new ArgumentNullException();
			_circlePlayer = circlePlayer ?? throw new ArgumentNullException();

			_sessionFactory = Hibernate.GetSessionFactory();

			InitializeComponent();
			Start();
		}

		private void Start()
		{
			_isGameOver = false;
			_isDraw = false;
			_isCrossTurn = true;

			ClearBoard();
		}

		private void ClearBoard()
		{
			for (var i = 0; i < _board.Length; i++)
				_board[i] = MarkType.Empty;

			Container.Children.Cast<Button>().ToList()
				.ForEach(button =>
				{
					button.Content = string.Empty;
					button.Background = GameColor.Empty;
				});
		}

		private void CheckWinner()
		{
			#region Horizontal

			if (_board[0] != MarkType.Empty && (_board[0] & _board[1] & _board[2]) == _board[0])
			{
				_isGameOver = true;

				Button00.Background = Button10.Background = Button20.Background = GameColor.Win;
			}

			if (_board[3] != MarkType.Empty && (_board[3] & _board[4] & _board[5]) == _board[3])
			{
				_isGameOver = true;

				Button01.Background = Button11.Background = Button21.Background = GameColor.Win;
			}

			if (_board[6] != MarkType.Empty && (_board[6] & _board[7] & _board[8]) == _board[6])
			{
				_isGameOver = true;

				Button02.Background = Button12.Background = Button22.Background = GameColor.Win;
			}

			#endregion

			#region Vertical

			if (_board[0] != MarkType.Empty && (_board[0] & _board[3] & _board[6]) == _board[0])
			{
				_isGameOver = true;

				Button00.Background = Button01.Background = Button02.Background = GameColor.Win;
			}

			if (_board[1] != MarkType.Empty && (_board[1] & _board[4] & _board[7]) == _board[1])
			{
				_isGameOver = true;

				Button10.Background = Button11.Background = Button12.Background = GameColor.Win;
			}

			if (_board[2] != MarkType.Empty && (_board[2] & _board[5] & _board[8]) == _board[2])
			{
				_isGameOver = true;

				Button20.Background = Button21.Background = Button22.Background = GameColor.Win;
			}

			#endregion

			#region Diagonal

			if (_board[0] != MarkType.Empty && (_board[0] & _board[4] & _board[8]) == _board[0])
			{
				_isGameOver = true;

				Button00.Background = Button11.Background = Button22.Background = GameColor.Win;
			}

			if (_board[2] != MarkType.Empty && (_board[2] & _board[4] & _board[6]) == _board[2])
			{
				_isGameOver = true;

				Button20.Background = Button11.Background = Button02.Background = GameColor.Win;
			}

			#endregion

			#region Draw

			if (!_isGameOver && _board.All(f => f != MarkType.Empty))
			{
				_isGameOver = true;
				_isDraw = true;

				Container.Children.Cast<Button>().ToList()
					.ForEach(b =>
					{
						b.Background = GameColor.Draw;
					});
			}

			#endregion
		}

		private void UpdateScore()
		{
			using (var session = _sessionFactory.OpenSession())
			using (var transaction = session.BeginTransaction())
			{
				if (_isDraw)
				{
					_crossPlayer.AddDraw();
					_circlePlayer.AddDraw();
				}
				else if (_isCrossTurn)
				{
					_crossPlayer.AddLose();
					_circlePlayer.AddWin();
				}
				else
				{
					_crossPlayer.AddWin();
					_circlePlayer.AddLose();
				}

				session.SaveOrUpdate(_crossPlayer);
				session.SaveOrUpdate(_circlePlayer);

				transaction.Commit();
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (_isGameOver)
				return;

			var button = (Button)sender;

			var column = Grid.GetColumn(button);
			var row = Grid.GetRow(button);
			var index = row * 3 + column;

			if (_board[index] != MarkType.Empty)
				return;

			if (_isCrossTurn)
			{
				_board[index] = MarkType.Cross;
				button.Content = "X";
				button.Foreground = GameColor.Cross;
			}
			else
			{
				_board[index] = MarkType.Circle;
				button.Content = "O";
				button.Foreground = GameColor.Circle;
			}

			_isCrossTurn ^= true;

			CheckWinner();

			if (_isGameOver)
				UpdateScore();
		}

		private void NewGame_Click(object sender, RoutedEventArgs e)
		{
			Start();
		}

		private void ChangePlayers_Click(object sender, RoutedEventArgs e)
		{
			new PlayersWindow()
				.Show();

			Close();
		}

		private void ShowScore_Click(object sender, RoutedEventArgs e)
		{
			new ScoreWindow()
				.Show();
		}
	}
}
