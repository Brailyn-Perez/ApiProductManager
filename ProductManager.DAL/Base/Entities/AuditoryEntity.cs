namespace ProductManager.DAL.Base.Entities
{
    public abstract class AuditoryEntity
    {
        protected AuditoryEntity()
        {
            CreatedAt = DateTime.Now;
            Deleted = false;
        }

        public bool Deleted { get; set; }
        public DateTime DeletedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
