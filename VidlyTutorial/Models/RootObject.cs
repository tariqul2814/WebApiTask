using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VidlyTutorial.Models
{
    public class RootObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }
        public MembershipType MembershipType { get; set; }
        public byte MembershipTypeId { get; set; }
        public DateTime Birth { get; set; }
    }
}