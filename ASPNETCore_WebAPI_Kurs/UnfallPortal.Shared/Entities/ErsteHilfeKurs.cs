using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnfallPortal.Shared.Entities
{
    public class ErsteHilfeKurs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ort { get; set; }

        public DateTime Datum { get; set; }

        public virtual ICollection<MandantKurs> MandantKurs { get; set; }

    }
}
