using BangazonAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestBangazonAPI
{
    public class TestProducts
    {
        /*This Tests that you can get all Products*/
        [Fact]
        public async Task Test_Get_All_Products()
        {

            using (var client = new APIClientProvider().Client)
            {
                var response = await client.GetAsync("/api/PaymentTypes");

                string responseBody = await response.Content.ReadAsStringAsync();
                var paymentTypesList = JsonConvert.DeserializeObject<List<Products>>(responseBody);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.True(paymentTypesList.Count > 0);
            }
        }

        /*This Tests that you can get one single Product*/
        [Fact]
        public async Task Test_Get_Single_Products()
        {

            using (var client = new APIClientProvider().Client)
            {
                var response = await client.GetAsync("/api/Products/1");


                string responseBody = await response.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<Products>(responseBody);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal(1, product.ProductTypeId);
                Assert.Equal(1, product.CustomerId);
                Assert.Equal((decimal)10.0000, product.Price);
                Assert.Equal("Lord of the Rings", product.Title);
                Assert.Equal("A book about a ring and an epic journey to a volcano.", product.Description);
                Assert.Equal(5, product.Quantity);
                Assert.NotNull(product);
            }
        }

        /*This tests that you can create and then delete one single Products*/
        [Fact]
        public async Task Test_Create_And_Delete_Products()
        {
            using (var client = new APIClientProvider().Client)
            {
                Products BusterSword = new Products
                {
                    ProductTypeId = 4,
                    CustomerId = 2,
                    Price = (decimal)1000.0000,
                    Title = "Buster Sword",
                    Description = "A big ass sword.",
                    Quantity = 1
                };
                var BusterSwordAsJSON = JsonConvert.SerializeObject(BusterSword);


                var response = await client.PostAsync(
                    "/api/Products",
                    new StringContent(BusterSwordAsJSON, Encoding.UTF8, "application/json")
                );


                string responseBody = await response.Content.ReadAsStringAsync();
                var newBusterSword = JsonConvert.DeserializeObject<Products>(responseBody);

                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
                Assert.Equal(4, newBusterSword.ProductTypeId);
                Assert.Equal(2, newBusterSword.CustomerId);
                Assert.Equal((decimal)1000.0000, newBusterSword.Price);
                Assert.Equal("A big ass sword.", newBusterSword.Description);
                Assert.Equal(1, newBusterSword.Quantity);


                var deleteResponse = await client.DeleteAsync($"/api/Products/{newBusterSword.Id}");
                Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
            }
        }
    }
}
