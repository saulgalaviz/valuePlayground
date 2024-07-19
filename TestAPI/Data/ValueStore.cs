using TestAPI.Models.Dto;

namespace TestAPI.Data
{
    public static class ValueStore
    {
        public static List<ValuesDTO> valueList = new List<ValuesDTO>
        {
            new ValuesDTO { Id = 1, Name = "Pool" },
            new ValuesDTO { Id = 2, Name = "Jacuzzi" }

        };
    }
}
