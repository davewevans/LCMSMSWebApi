
namespace LCMSMSWebApi.Models
{
    public class PasswordHashModel
    {
        public string Hash { get; set; }

        public byte[] Salt { get; set; }
    }
}
