using System;
using TicTacToe.Extensions;

namespace TicTacToe.Core
{
	public class Player
	{
		public virtual int Id { get; protected set; }
		public virtual string Name { get; protected set; }
		public virtual int Win { get; protected set; }
		public virtual int Lose { get; protected set; }
		public virtual int Draw { get; protected set; }

		protected Player()
		{
			// required by NHibernate
		}

		protected Player(string name)
		{
			Name = name;
		}

		public static Player Create(string name)
		{
			if (name.IsEmpty()) throw new ArgumentException();

			return new Player(name);
		}

		public virtual void AddWin()
			=> Win++;

		public virtual void AddLose()
			=> Lose++;

		public virtual void AddDraw()
			=> Draw++;
	}
}
