using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Models.Models.Auxiliary
{
    public class DateOnlyCustomConverter : ValueConverter<DateOnly, DateTime>
    {
        //First, Model to Provider.
        //Then, Provider to Model.
        public DateOnlyCustomConverter() :
            base(d => d.ToDateTime(TimeOnly.MinValue),
            d => DateOnly.FromDateTime(d))
        { }
    }
}