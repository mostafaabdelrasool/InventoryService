namespace Product.Domain.Aggregate
{
    public class Categories:Entity
    {
        public string CategoryName { get;private set; }
        public string Description { get; private set; }
        public byte[] Picture { get; private set; }
        public Categories(string categoryName, string description, byte[] picture)
        {
            Picture = picture;
            CategoryName = categoryName;
            Description = description;
        }
        protected Categories()
        {

        }
    }
}