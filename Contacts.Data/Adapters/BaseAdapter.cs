using System;
using NHibernate;

namespace Contacts.Data.Adapters
{
    public class BaseAdapter : IDisposable
    {
        protected readonly ISession Session;
        
        private ITransaction tran = null;

        public BaseAdapter()
        {
            Session = ConnectionManager.OpenSession();
        }

        public void BeginTransaction()
        {
            tran = Session.BeginTransaction();
        }

        public void Commit()
        {
            if (tran != null)
                tran.Commit();
        }

        public void Rollback()
        {
            if (tran != null)
                tran.Rollback();
        }

        public void Dispose()
        {
            if (tran != null)
                tran.Dispose();
            Session.Close();
            Session.Dispose();
        }
    }
}
