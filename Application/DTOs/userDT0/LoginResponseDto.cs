public class LoginResponseDto
{
    public string user_name { get; set; }
    public string user_id { get; set; }
    public string? ErrorMessage { get; set; }
    public string? SuccessMessage { get; set; }
    public string? Token{ get; set; }
}