namespace AspNetIdentityOnly.Models
{
    public class IdentityResultDTO
    {
        public bool Success () => Errors == null || !Errors.Any ();

        public IEnumerable<string> Errors { get; set; }

        public IEnumerable<string> Infos { get; set;}
    }
}
