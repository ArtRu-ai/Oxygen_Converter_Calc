using OxygenConverterWebApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace OxygenConverterWebApp.Infrastructure
{
    public class VariantsRepository : IVariantsRepository
    {
        OxyConverterDB _context;

        public VariantsRepository(OxyConverterDB context)
        {
            _context = context;
        }

        IQueryable<Variants> IVariantsRepository.All
        {
            get { return _context.Variants; }
        }

        Variants IVariantsRepository.CurrentVariant
        {
            get
            {
                return _context
                    .Variants
                    .Where(u => u.Owner.ID_User == WebSecurity.CurrentUserId)
                    .FirstOrDefault();
            }
        }

        void IVariantsRepository.InsertOrUpdate(Variants variants)
        {
            if (variants.ID_Variant == default(int))
            {
                _context.Variants.Add(variants);
            }
            else
            {
                _context.Entry(variants).State = System.Data.Entity.EntityState.Modified;
            }
        }

        void IVariantsRepository.Remove(Variants variants)
        {
            _context.Entry(variants).State = System.Data.Entity.EntityState.Deleted;
        }

        void IVariantsRepository.Save()
        {
            _context.SaveChanges();
        }
    }
}