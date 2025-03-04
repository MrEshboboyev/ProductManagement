using ProductManagement.Domain.Exceptions;

namespace ProductManagement.Domain.ValueObjects
{
    public class ProductId : ValueObject
    {
        public string Value { get; private set; }

        private ProductId(string value)
        {
            Value = value;
        }

        public static ProductId Create(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new DomainException("Product ID cannot be empty");

            return new ProductId(id);
        }

        public static ProductId CreateUnique()
        {
            return new ProductId(Guid.NewGuid().ToString());
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }
}