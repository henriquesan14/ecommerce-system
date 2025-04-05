﻿using ECommerceSystem.Domain.ValueObjects;
using ECommerceSystem.Shared.Base;

namespace ECommerceSystem.Domain.Entities
{
    public class Product : Aggregate<ProductId>
    {
        public string Name { get; private set; } = default!;
        public decimal Price { get; private set; } = default!;

        public static Product Create(ProductId id, string name, decimal price)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            var product = new Product
            {
                Id = id,
                Name = name,
                Price = price
            };

            return product;
        }
    }
}
