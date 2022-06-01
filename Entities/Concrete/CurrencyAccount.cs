﻿using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class CurrencyAccount : IEntity
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Adres { get; set; }
        public string? TaxDepartment { get; set; } 
        public string? TaxIdNumber { get; set; }
        public string? IdentityNumber { get; set; }
        public string? Email { get; set; }
        public string? Authorized { get; set; }
        public DateTime AddedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
