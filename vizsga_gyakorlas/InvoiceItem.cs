//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace vizsga_gyakorlas
{
    using System;
    using System.Collections.Generic;
    
    public partial class InvoiceItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> Amount { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<int> VATPercentage { get; set; }
        public Nullable<int> InvoiceID { get; set; }
        public Nullable<int> OrderItemID { get; set; }
    
        public virtual Invoice Invoice { get; set; }
        public virtual OrderItem OrderItem { get; set; }
    }
}
