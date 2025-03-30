namespace ECommerceSystem.Shared.Base
{
    public class Entity
    {
        public int Id { get; protected set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UpdatedByUserId { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity))
            {
                return false;
            }

            Entity outraEntity = (Entity)obj;

            return this.Id == outraEntity.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
