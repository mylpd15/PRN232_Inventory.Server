namespace Inventory.Domain
{
    public class InventoryEntryDto
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public InventoryEntryType EntryType { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
