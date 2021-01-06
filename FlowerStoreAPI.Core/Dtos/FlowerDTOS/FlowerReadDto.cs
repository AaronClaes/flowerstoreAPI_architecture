namespace FlowerStoreAPI.Dtos.FlowerDTOS
{
    // Includes all parameters that will be shown with GET request. This is useful when some data is not supposed to be available.
    public class FlowerReadDto
    {
      
        public int Id { get; set; }
      
        public string Name { get; set; }
       
        public string Color { get; set; }

        public double Price { get; set; }
        
    }
}