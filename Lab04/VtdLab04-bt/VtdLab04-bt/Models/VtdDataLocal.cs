namespace VtdLab04_bt.Models
{
    public class VtdDataLocal
    {
        //Danh mục
        public static List<VtdCategory> Categories = new List<VtdCategory>()
        {
            new VtdCategory(){Id=1,Name="iPhone"},
            new VtdCategory(){Id=2,Name="Samsung"},
            new VtdCategory(){Id=3,Name="Xiaomi"},
            new VtdCategory(){Id=4,Name="Oppo"},
            new VtdCategory(){Id=5,Name="Vivo"}
        };

        //Sản phẩm
        public static List<VtdProduct> Products = new List<VtdProduct>()
        {
            new VtdProduct()
            {
                Id=1,
                Name="iPhone 15 Pro Max",
                Price=32000000,
                SalePrice=29990000,
                Status=true,
                CreatedDate=DateTime.Now,
                Image="/images/product/iphone15.jpg",
                CategoryId=1,
                Description="iPhone 15 Pro Max 256GB"
            },

            new VtdProduct()
            {
                Id=2,
                Name="Samsung Galaxy S24 Ultra",
                Price=31000000,
                SalePrice=28500000,
                Status=true,
                CreatedDate=DateTime.Now,
                Image="/images/product/s24.jpg",
                CategoryId=2,
                Description="Galaxy S24 Ultra 256GB"
            },

            new VtdProduct()
            {
                Id=3,
                Name="Xiaomi 14 Ultra",
                Price=26000000,
                SalePrice=23990000,
                Status=true,
                CreatedDate=DateTime.Now,
                Image="/images/product/xiaomi14.jpg",
                CategoryId=3,
                Description="Xiaomi 14 Ultra"
            }
        };
    }
}
