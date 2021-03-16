namespace dotnetcoreAPI.Models
{
    public partial class ZarinFeedback
    {
        public string code { get; set; }
        public string message { get; set; }
        public string card_hash { get; set; }
        public string card_pan { get; set; }
        public string ref_id { get; set; }
        public string fee_type { get; set; }
        public string fee { get; set; }
    }
}
