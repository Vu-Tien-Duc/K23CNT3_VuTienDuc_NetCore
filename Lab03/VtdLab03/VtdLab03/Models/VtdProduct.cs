using System.Collections.Generic;

namespace VtdLab03.Models
{
    public class VtdProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }

        public List<VtdProduct> GetProductList()
        {
            List<VtdProduct> products = new List<VtdProduct>()
            {
                new VtdProduct {
                    Id = 1,
                    Name = "Lò vi sóng 1", 
                    Image = "/images/lovisong3.jpg", 
                    Price = 2350000
                },
                new VtdProduct {
                    Id = 2,
                    Name = "Lò vi sóng 2",
                    Image = "/images/lovisong3.jpg", 
                    Price = 2350000
                },
                new VtdProduct {
                    Id = 3,
                    Name = "Lò vi sóng 3",
                    Image = "/images/lovisong3.jpg", 
                    Price = 2350000
                }
            };
            return products;
        }
    }
}