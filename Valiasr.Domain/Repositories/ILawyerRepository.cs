namespace Valiasr.Domain.Repositories
{
    using System;

    using Valiasr.Domain.Model;

    public interface ILawyerRepository:IRepository<Lawyer>
    {
        void AddLawyer(string melliIdentity , DateTime startDate);
    }
}
