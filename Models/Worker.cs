using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bilet_15.Models.Base;

namespace Bilet_15.Models
{
    public class Worker : BaseEntity
    {
        [MaxLength(100)]
        public string FullName { get; set; }
        public string Desicnation { get; set; }
        public string Image {  get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
