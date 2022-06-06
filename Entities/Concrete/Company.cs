﻿using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Company : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string TaxDepartment { get; set; }
        public string? TaxIdNumber { get; set; }
        public string? IdentityNumber { get; set; }
        public DateTime AddedAt { get; set; }
        public bool IsActive { get; set; }
      
    }
}
