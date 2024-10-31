namespace E_Commerce.DTOs
{
    public class CreateJwtTokenParametersDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public string Role {  get; set; }   
    }
}
