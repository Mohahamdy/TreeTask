using Microsoft.AspNetCore.Components.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreeTask.Models
{
    public class AssetsTree
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Number { get; set; } = string.Empty;

        public bool IsLast { get; set; }

        public double Money { get; set; }

        [ForeignKey(nameof(Parent))]
        public int? ParentId { get; set; }

        public AssetsTree Parent { get; set; }

        public ICollection<AssetsTree> assetsTrees { get; set; }
    }
}
