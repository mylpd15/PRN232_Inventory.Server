﻿
namespace Inventory.Domain.DTOs.CustomerDto
{
    public class CreateCustomerDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
