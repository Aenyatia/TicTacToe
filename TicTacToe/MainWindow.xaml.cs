using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TicTacToe.Core;
using Color = TicTacToe.Resources.Color;

namespace TicTacToe
{
	//todo set players depending on user input
	//todo create database to save players score

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly MarkType[] _board;

		private Player _crossPlayer;
		private Player _circlePlayer;

		private bool _isGameOver;
		private bool _isCrossTurn;

		public MainWindow()
		{
			_board = new MarkType[9];

			InitializeComponent();
			SetPlayers();
			Start();
		}

		private void SetPlayers()
		{
			_crossPlayer = new Player { Name = "Geralt" };
			_circlePlayer = new Player { Name = "Yennefer" };
		}

		private void Start()
		{
			_isGameOver = false;
			_isCrossTurn = true;

			ClearBoard();
		}

		private void ClearBoard()
		{
			for (var i = 0; i < _board.Length; i++)
				_board[i] = MarkType.Empty;

			Container.Children.Cast<Button>().ToList().ForEach(button =>
			{
				button.Content = string.Empty;

				button.FontSize = 210;
				button.Background = Brushes.White;
				button.BorderThickness = new Thickness(0.3);
			});
		}

		private void CheckWinner()
		{
			#region Horizontal

			if (_board[0] != MarkType.Empty && (_board[0] & _board[1] & _board[2]) == _board[0])
			{
				_isGameOver = true;

				Button00.Background = Button10.Background = Button20.Background = Color.Win;
			}

			if (_board[3] != MarkType.Empty && (_board[3] & _board[4] & _board[5]) == _board[3])
			{
				_isGameOver = true;

				Button01.Background = Button11.Background = Button21.Background = Color.Win;
			}

			if (_board[6] != MarkType.Empty && (_board[6] & _board[7] & _board[8]) == _board[6])
			{
				_isGameOver = true;

				Button02.Background = Button12.Background = Button22.Background = Color.Win;
			}

			#endregion

			#region Vertical

			if (_board[0] != MarkType.Empty && (_board[0] & _board[3] & _board[6]) == _board[0])
			{
				_isGameOver = true;

				Button00.Background = Button01.Background = Button02.Background = Color.Win;
			}

			if (_board[1] != MarkType.Empty && (_board[1] & _board[4] & _board[7]) == _board[1])
			{
				_isGameOver = true;

				Button10.Background = Button11.Background = Button12.Background = Color.Win;
			}

			if (_board[2] != MarkType.Empty && (_board[2] & _board[5] & _board[8]) == _board[2])
			{
				_isGameOver = true;

				Button20.Background = Button21.Background = Button22.Background = Color.Win;
			}

			#endregion

			#region Diagonal

			if (_board[0] != MarkType.Empty && (_board[0] & _board[4] & _board[8]) == _board[0])
			{
				_isGameOver = true;

				Button00.Background = Button11.Background = Button22.Background = Color.Win;
			}

			if (_board[2] != MarkType.Empty && (_board[2] & _board[4] & _board[6]) == _board[2])
			{
				_isGameOver = true;

				Button20.Background = Button11.Background = Button02.Background = Color.Win;
			}

			#endregion

			#region Draw

			if (!_isGameOver && _board.All(f => f != MarkType.Empty))
			{
				_isGameOver = true;

				Container.Children.Cast<Button>().ToList().ForEach(b =>
				{
					b.Background = Color.Draw;
				});
			}

			#endregion
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (_isGameOver)
				Start();

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
				button.Foreground = Color.Cross;
			}
			else
			{
				_board[index] = MarkType.Circle;
				button.Content = "O";
				button.Foreground = Color.Circle;
			}

			_isCrossTurn ^= true;

			CheckWinner();
		}
	}
}
