using System;
using System.Collections.Generic;

#nullable disable

namespace Logistics.Domain.Entities
{
    public partial class Pedido
    {
        public Pedido()
        {
            Ocorrencia = new HashSet<Ocorrencia>();
        }

        public int Id { get; set; }
        public int? NumeroPedido { get; set; }
        public DateTime? HoraPedido { get; set; }
        public bool IndCancelado { get; set; }
        public bool IndConcluido { get; set; }

        public virtual ICollection<Ocorrencia> Ocorrencia { get; set; }
    }
}
