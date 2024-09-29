namespace project2api.DTO.emloyeesDto
{
    public class productforcartDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<listtask>? taskname { get; set; } = new List<listtask>();
    }
    public class listtask
    {
        public string name { get; set; }
    }
}
