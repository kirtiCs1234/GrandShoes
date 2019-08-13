using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ViewDistribution
    {
        public ViewDistribution()
        {
            Childs = new List<ViewDistribution>();

        }
        public int ID { get; set; }

        public string Name { get; set; }
 
        public int? Pid { get; set; }
        public string TransactionDate { get; set; }
        [ForeignKey("Pid")]
        
        public virtual List<ViewDistribution> Childs { get; set; }
    }
}
