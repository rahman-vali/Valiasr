namespace Valiasr.DataAccess
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.ModelConfiguration;

    public class CustomDatabaseInitializer<T> : IDatabaseInitializer<T>
        where T : DbContext
    {
        public void InitializeDatabase(T context)
        {
            if (context.Database.Exists())
            {
                string tablesScript = this.TablesScript(context);
                context.Database.ExecuteSqlCommand(tablesScript);
            }
            if (!context.Database.CompatibleWithModel(false))
            {
                throw new ModelValidationException();
            }
        }

        private string TablesScript(T context)
        {
            IObjectContextAdapter adapter = context;
            string tablesScript = adapter.ObjectContext.CreateDatabaseScript();
            return tablesScript;
        }

    }
}