namespace Valiasr.Domain.Repositories
{
    using Valiasr.Domain.Model;

    public interface IGeneralAccountRepository:IRepository<GeneralAccount>
    {
        void AddIndexAccount(IndexAccount indexAccount);
    }
}
