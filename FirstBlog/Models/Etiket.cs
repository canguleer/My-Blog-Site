//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FirstBlog.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Etiket
    {
        public int EtiketId { get; set; }
        public string EtiketAdi { get; set; }
        public Nullable<int> MakaleId { get; set; }
    
        public virtual Makale Makale { get; set; }
    }
}
