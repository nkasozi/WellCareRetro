using System;
using System.Collections.Generic;
using System.Text;
using WellCare.Models.Interface;
using System.ComponentModel.DataAnnotations;

namespace WellCare.Models
{
    public class BaseModel : IModelValidator
    {
        public Status status { get; set; }



        public bool IsValid()
        {
            //use data annotation attributes within the class to validate the model
            var context = new ValidationContext(this, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(this, context, results);

            //object is valid
            if (isValid)
            {
                this.status = Status.SUCCESS;
                return true;
            }

            //oops errors
            status = Status.FAILURE;

            //get errors in the status desc
            foreach (var validationResult in results)
            {
               status.StatusDesc += $",{validationResult.ErrorMessage}";
            }

            //remove trailing comma
            status.StatusDesc = status.StatusDesc.Trim(new char[] { ',' });
            status.StatusCode = Status.FAILURE_STATUS_CODE;
            return false;
        }
    }
}
