using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Layer.Helpers
{
    internal class Extenstions
    {
        public static int CalculateAge(DateOnly dob)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            int age = today.Year - dob.Year;
            if (dob.DayOfYear > today.DayOfYear)
                age--;
            return age;
        }

    }
}
