﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KHBPA.Models
{
    public class Member
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime MembershipEnrollmentDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string LicenseNumber { get; set; }
        public bool IsOwner { get; set; }
        public bool IsTrainer { get; set; }
        public bool IsOwnerAndTrainer { get; set; }
        public bool AgreedToTerms { get; set; }
        public string Affiliation { get; set; }
        public string ManagingPartner { get; set; }
    }
}