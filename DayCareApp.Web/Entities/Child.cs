//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DayCareApp.Web.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Child
    {
        public int childId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public Nullable<System.DateTime> birthday { get; set; }
        public int departmentId { get; set; }
        public int parentChildId { get; set; }
        public int institutionId { get; set; }
        public int addressId { get; set; }
    }
}
