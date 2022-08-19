using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnfallPortal.Shared.Entities
{
    public class MandantUnfall
    {
        public int Id { get; set; } 
        
        public int MandantId { get; set; }  
        public virtual Mandant MandantRef { get; set; }


        public virtual ICollection<MandantUnfallDokumente> MandantUnfallDokumente { get; set; }

        public string UnfallName { get; set; }




    }
}
