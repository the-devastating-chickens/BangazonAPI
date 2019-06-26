using BangazonAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
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
                Assert.Equal(10.0000, product.Price);
                Assert.Equal("Lord of the Rings", product.Title);
                Assert.Equal("A book about a ring and an epic journey to a volcano.", product.Description);
                Assert.Equal(5, product.Quantity);
                Assert.NotNull(product);
            }
        }
    }
}
