using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OxygenConverterWebApp.Domain;

namespace OxygenConverterWebApp.Infrastructure
{
    public class DALContext: IDALContext
    {
        OxyConverterDB _database;
        IUserProfileRepository _users;
        IVariantsRepository _variants;
        IInputDataVariantsRepository _inputDataVariants;

        public DALContext()
        {
            _database = new OxyConverterDB();
        }

        public IUserProfileRepository Users
        {
            get
            {
                if (_users == null)
                {
                    _users = new UserRepository(_database);
                }
                return _users;
            }
        }

        public IVariantsRepository Variants
        {
            get
            {
                if (_variants == null)
                {
                    _variants = new VariantsRepository(_database);
                }
                return _variants;
            }
        }

        public IInputDataVariantsRepository InputDataVariants
        {
            get
            {
                if (_inputDataVariants == null)
                {
                    _inputDataVariants = new InputDataVariantsRepository(_database);
                }
                return _inputDataVariants;
            }
        }
    }
}