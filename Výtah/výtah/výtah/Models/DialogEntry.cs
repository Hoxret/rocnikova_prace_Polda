namespace Vytah.Models
{
    public class DialogEntry
    {
        public string Speaker { get; set; }   // "Paní", "Muž", "Holčička", "" = narativ
        public string Text { get; set; }
        public string NextSceneId { get; set; }  // volitelné: přejdi do jiné scény po dialogu
    }
}