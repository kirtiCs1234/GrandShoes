using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace POS.Models
{
    public class ViewDistribution
    {
        public ViewDistribution()
        {
            Childs = new List<ViewDistribution>();

        }
        public int ID { get; set; }

        //Cat Name  
        public string Name { get; set; }

        //Cat Description  
        public string Description { get; set; }

        //represnts Parent ID and it's nullable  
        public int? Pid { get; set; }
        [ForeignKey("Pid")]
        public virtual ViewDistribution Parent { get; set; }
        public virtual IEnumerable<ViewDistribution> Childs { get; set; }

    }
}