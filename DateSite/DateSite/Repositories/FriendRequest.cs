//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Repositories
{
    using System;
    using System.Collections.Generic;
    
    public partial class FriendRequest
    {
        public int requestid { get; set; }
        public int requesterid { get; set; }
        public int friendid { get; set; }
    
        public virtual Profiles Profiles { get; set; }
        public virtual Profiles Profiles1 { get; set; }
    }
}