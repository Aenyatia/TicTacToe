# TicTacToe
Come and try a new Tic Tac Toe game. You can create your nickname and keep history of your games. Learn, play and climb in the ranking. Ten the best players are shown in scoreboard.

Used tools & technologies:
* WPF
* NHibernate


## Requirements
* .NET Framework 4.7.2
	
## Installation
	1. Create a new database file, e.g. TicTacToe.mdf.
	2. Update connection string (DATABASE_PATH) in NHibernate\Hibernate.cs.
	
		ConnectionString = @"...;AttachDbFilename=C:\TicTacToe.mdf;...";
	
	3. Only for the FIRST run, set reset database on true for execute SQL code to create tables; then 
	restore false or you will lose all data stored in database.
	
		FIRST RUN:		bool ResetDatabase = true;
		SECOND AND LATER: 	bool ResetDatabase = false;
	

## License
TODO: Write license.


## Example Screenshots
![alt text](https://github.com/Aenyatia/TicTacToe/blob/master/TicTacToe/Resources/game.jpg)
