namespace PizzeriaInForno.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DettaglioOrdine")]
    public partial class DettaglioOrdine
    {
        [Key]
        public int ID_DettaglioOrdine { get; set; }

        public int ID_Pietanza { get; set; }

        [NotMapped()]
        public string Nome { get; set; }


        public int Quantita { get; set; }

        public int ID_Ordine { get; set; }

        public virtual Ordine Ordine { get; set; }

        public virtual Pietanza Pietanza { get; set; }

        [NotMapped()]
        public static List<DettaglioOrdine> ListaDettagliOrdini = new List<DettaglioOrdine>();
    }
}
