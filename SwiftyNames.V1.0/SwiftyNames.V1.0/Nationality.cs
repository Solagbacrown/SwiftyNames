//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SwiftyNames.V1._0
{
    using System;
    using System.Collections.Generic;
    
    public partial class Nationality
    {
        public Nationality()
        {
            this.Forms = new HashSet<Form>();
        }
    
        public int NationalityId { get; set; }
        public string Country { get; set; }
        public string Abbreviation { get; set; }
        public string Adjective { get; set; }
        public string Person { get; set; }
    
        public virtual ICollection<Form> Forms { get; set; }
    }
}
