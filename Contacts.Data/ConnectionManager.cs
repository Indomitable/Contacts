using System.Data;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Contacts.Data
{
    public static class ConnectionManager
    {
        private static ISessionFactory sessionFactory;
        private static readonly Configuration configuration;

        static ConnectionManager()
        {
            configuration = new Configuration();
            configuration.AddAssembly(Assembly.GetExecutingAssembly());
            configuration.Configure();
            configuration.SessionFactoryName("CONNECTION_FACTORY");
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStr"].ConnectionString;
            configuration.DataBaseIntegration(db =>
            {
                db.ConnectionProvider<NHibernate.Connection.DriverConnectionProvider>();
                db.Dialect<NHibernate.Dialect.MsSql2012Dialect>();
                db.Driver<NHibernate.Driver.SqlClientDriver>();
                db.IsolationLevel = IsolationLevel.ReadCommitted;
                db.ConnectionString = connectionString;
                db.PrepareCommands = true;
                db.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                db.Timeout = 200;
#if DEBUG
                db.LogSqlInConsole = true;
                db.BatchSize = 0;
                db.LogFormattedSql = true;
                db.AutoCommentSql = false;
#endif
            });
            SchemaMetadataUpdater.QuoteTableAndColumns(configuration);
        }

        public static ISession OpenSession()
        {
            if (sessionFactory == null)
                sessionFactory = configuration.BuildSessionFactory();
            return sessionFactory.OpenSession();
        }

        public static IStatelessSession OpenStatelessSession()
        {
            if (sessionFactory == null)
                sessionFactory = configuration.BuildSessionFactory();
            return sessionFactory.OpenStatelessSession();
        }

        public static void Close()
        {
            if (sessionFactory != null)
            {
                sessionFactory.Close();
                sessionFactory.Dispose();
                sessionFactory = null;
            }
        }
    }
}
