using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OxygenConverterWebApp.Domain
{
    public interface IVariantsRepository
    {
        IQueryable<Variants> All { get; }

        Variants CurrentVariant { get; }

        void InsertOrUpdate(Variants variants);

        void Remove(Variants variants);

        void Save();
    }
}
