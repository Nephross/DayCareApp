//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DayCareApp.Web
{
    using System;
    using System.Collections.Generic;
    
    public partial class DayRegistration
    {
        public int DayRegistrationId { get; set; }
        public Nullable<System.DateTime> CheckInTime { get; set; }
        public Nullable<System.DateTime> CheckOutTime { get; set; }
        public Nullable<System.DateTime> ExpectedCheckOutTime { get; set; }
        public int FK_ChildId { get; set; }
        public int FK_InstitutionId { get; set; }
        public int FK_ExpectedCheckOutParentId { get; set; }
        public int FK_CheckInParentId { get; set; }
        public int FK_CheckInEmployeeId { get; set; }
        public Nullable<int> FK_CheckOutParentId { get; set; }
        public Nullable<int> FK_CheckOutEmployeeId { get; set; }
    
        public virtual Child Child { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Parent Parent { get; set; }
        public virtual Employee Employee1 { get; set; }
        public virtual Parent Parent1 { get; set; }
        public virtual Parent Parent2 { get; set; }
        public virtual Institution Institution { get; set; }
    }
}
