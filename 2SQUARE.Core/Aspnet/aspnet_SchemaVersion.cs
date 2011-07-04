using System.ComponentModel.DataAnnotations;

namespace _2SQUARE.Core.Aspnet
{
    public class aspnet_SchemaVersion
    {
        [StringLength(128)]
        [Required]
        public string Feature { get; set; }
        [StringLength(128)]
        [Required]
        public string CompatibleSchemaVersion { get; set; }

        public bool IsCurrentVersion { get; set; }
    }
}
