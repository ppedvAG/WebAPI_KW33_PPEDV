using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnfallPortal.Shared.Entities
{
    public  class MandantKurs
    {
        public int Id { get; set; }
        
        public int MandantId { get; set; }
        public virtual Mandant Mandant { get; set; }

        public int KursId { get; set; }
        public virtual ErsteHilfeKurs ErsteHilfeKurs { get; set; }

    }
}
