namespace todoapi.Dtos
{
    public class CreateTodoDto
    {
        public string Name { get; set; } = string.Empty;
    }

    public class UpdateTodoStatusDto
    {
        public string Status { get; set; } = "Pending";
    }
}
