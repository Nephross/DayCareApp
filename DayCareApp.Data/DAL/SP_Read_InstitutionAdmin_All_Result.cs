//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DayCareApp.Data.DAL
{
    using System;
    
    public partial class SP_Read_InstitutionAdmin_All_Result
    {
        public int InstitutionAdminId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FK_ApplicationUserId { get; set; }
        public int FK_InstitutionId { get; set; }
        public Nullable<int> PhoneNumber { get; set; }
        public string Email { get; set; }
        public int FK_AddressId { get; set; }
    }
}