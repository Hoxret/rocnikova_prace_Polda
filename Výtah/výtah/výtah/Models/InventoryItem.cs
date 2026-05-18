namespace Vytah.Models
{
    public class InventoryItem
    {
        public string Id { get; set; }           // "pojistka", "karta", "klic"
        public string Name { get; set; }          // Zobrazovaný název
        public string Description { get; set; }   // Tooltip
        public string IconPath { get; set; }      // Cesta k ikoně v Assets/UI/

        public InventoryItem(string id, string name, string description, string iconPath = null)
        {
            Id = id;
            Name = name;
            Description = description;
            IconPath = iconPath ?? $"Assets/UI/item_{id}.png";
        }
    }
}