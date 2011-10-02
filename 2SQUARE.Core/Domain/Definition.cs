using System.ComponentModel.DataAnnotations;

namespace _2SQUARE.Core.Domain
{
    public class Definition : DomainObject
    {
        public Definition()
        {
            IsActive = true;
        }

        [Required]
        public string Description { get; set; }
        public string Source { get; set; }
        public bool IsActive { get; set; }
        
        [Required]
        public Term Term { get; set; }
    }
}
