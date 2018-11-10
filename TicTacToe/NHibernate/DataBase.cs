using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace TicTacToe.NHibernate
{
	public class DataBase
	{
		private static ISessionFactory _sessionFactory;

		private DataBase() { }

		public static ISessionFactory GetSessionFactory() => _sessionFactory ?? (_sessionFactory = CreateSessionFactory());

		private static ISessionFactory CreateSessionFactory()
		{
			const string connectionString = "Data source=(LocalDb)\\MSSQLLocalDB;AttachDbFilename=PATH;Database=DATABASE_NAME;Integrated Security=True";

			var sqlConfiguration = MsSqlConfiguration.MsSql2012
				.ConnectionString(connectionString)
				.ShowSql();

			void Config(Configuration cfg)
				=> new SchemaExport(cfg)
					.Execute(true, false, false);

			return Fluently.Configure()
				.Database(sqlConfiguration)
				.Mappings(m => m.FluentMappings.AddFromAssemblyOf<DataBase>())
				.ExposeConfiguration(Config)
				.BuildSessionFactory();
		}
	}
}
