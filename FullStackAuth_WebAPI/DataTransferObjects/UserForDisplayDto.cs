namespace FullStackAuth_WebAPI.DataTransferObjects
{
    public class UserForDisplayDto
    {
        //DTO used when displaying User linked with FK
        public string Id { get; set; }
        public string Name { get; set; }
        public string? LastName { get; set; }
        public string UserName { get; set; }
        public bool? IsWorker {  get; set; }
    }
}
