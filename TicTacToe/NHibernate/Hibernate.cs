using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace TicTacToe.NHibernate
{
	public class Hibernate
	{
		private const bool ResetDatabase = false;
		private const bool DropDatabase = false;
		private const string ConnectionString = @"Data source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=DATABASE_PATH;Integrated Security=True";

		private static ISessionFactory _sessionFactory;

		private Hibernate() { }

		public static ISessionFactory GetSessionFactory() => _sessionFactory ?? (_sessionFactory = CreateSessionFactory());

		private static ISessionFactory CreateSessionFactory()
		{
			var sqlConfiguration = MsSqlConfiguration.MsSql2012
				.ConnectionString(ConnectionString)
				.ShowSql();

			void Config(Configuration cfg)
				=> new SchemaExport(cfg)
					.Execute(true, ResetDatabase, DropDatabase);

			return Fluently.Configure()
				.Database(sqlConfiguration)
				.Mappings(m => m.FluentMappings.AddFromAssemblyOf<Hibernate>())
				.ExposeConfiguration(Config)
				.BuildSessionFactory();
		}
	}
}
