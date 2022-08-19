using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnfallPortal.Shared.Entities
{
    public class MandantUnfallDokumente
    {
        public int Id { get; set; }
        public int MandantUnfallId { get; set; }
        public virtual MandantUnfall MandantUnfall { get; set; }
        public string Name { get; set; }
        public Guid Dateinamen { get; set; }
    }
}
