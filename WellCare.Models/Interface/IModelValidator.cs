using System;
using System.Collections.Generic;
using System.Text;

namespace WellCare.Models.Interface
{
    public interface IModelValidator
    {
        Status status { get; set; }

        bool IsValid();
    }
}
