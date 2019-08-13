using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{ 
    public class ManageTemplate
    {
        public TemplateModel DefaultTemplate { get; set; }
        public TemplateModel MarkDownTemplate { get; set; }
        public TemplateModel CommanTemplate { get; set; }
        public List<string> Sizes{ get; set; }
    }
}
