using TestAPI.Models.Dto;

namespace TestAPI.Data
{
    public static class ValueStore
    {
        public static List<ValuesDTO> valueList = new List<ValuesDTO>
        {
            new ValuesDTO { Id = 1, Name = "Pool", Sqft = 100, Occupancy = 4 },
            new ValuesDTO { Id = 2, Name = "Jacuzzi" ,Sqft = 300, Occupancy = 3 }

        };
    }
}
