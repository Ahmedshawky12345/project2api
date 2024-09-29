namespace project2api.DTO
{
    public class cartDTO
    {
        public int id {  get; set; }
        public string name { get; set; }
        public ICollection<CartiemDTO> cartiemdto { get; set; } = new List<CartiemDTO>();

    }
}
