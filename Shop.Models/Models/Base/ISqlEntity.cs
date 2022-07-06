using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models.Models.Base
{
    public abstract class ISqlEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public long LastModifiedBy { get; set; }
    }
}
