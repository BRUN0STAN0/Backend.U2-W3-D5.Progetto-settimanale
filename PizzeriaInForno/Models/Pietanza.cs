namespace PizzeriaInForno.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Pietanza")]
    public partial class Pietanza
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pietanza()
        {
            DettaglioOrdine = new HashSet<DettaglioOrdine>();
        }

        [Key]
        public int ID_Pietanza { get; set; }

        [StringLength(50)]
        public string Nome { get; set; }

        [Column(TypeName = "money")]
        public decimal? Prezzo { get; set; }

        public int? MinutiConsegna { get; set; }
        [NotMapped()]
        public int QuantitaAggiuntaAlCarrello { get; set; }
        public string Ingredienti { get; set; }
        [NotMapped()]
        public HttpPostedFileBase FileFoto { get; set; }
        public string Foto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DettaglioOrdine> DettaglioOrdine { get; set; }
    }
}
