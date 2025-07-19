namespace WareSync.Api.DTOs
{
    public class ChangePasswordDto
    {
        public string Old_Password { get; set; } = string.Empty;
        public string New_Password { get; set; } = string.Empty;
        public string Confirm_Password { get; set; } = string.Empty;
    }

}
