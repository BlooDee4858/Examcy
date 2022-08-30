namespace Examcy.Data.Models
{
    public class TypesModel
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public int TypeId { get; set; }
        public int NumInEGE { get; set; }
        public virtual ModelsOfEGE ModelsOfEGE { get; set; } = new ModelsOfEGE();
        public virtual Types Types { get; set; } = new Types();
    }
}
