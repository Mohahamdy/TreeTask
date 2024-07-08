using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TreeTask.DTOs
{
    public class AssetsTreeDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Number { get; set; } = string.Empty;

        public bool IsLast { get; set; }

        public double Money { get; set; }

        public int? ParentId { get; set; }

        public IEnumerable<AssetsTreeDTO> assetsTreeDTOs { get; set; }
    }
}
