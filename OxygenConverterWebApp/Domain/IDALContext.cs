using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;

namespace OxygenConverterWebApp.Domain
{
    public interface IDALContext
    {
        IUserProfileRepository Users { get; }

        IVariantsRepository Variants { get; }

        IInputDataVariantsRepository InputDataVariants { get; }
    }
}
