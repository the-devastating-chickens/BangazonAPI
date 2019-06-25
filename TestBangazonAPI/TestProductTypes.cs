using System;
using System.Net;
using Newtonsoft.Json;
using Xunit;
using BangazonAPI.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TestBangazonAPI
{
    public class TestProductTypes
    {
        [Fact]
        public async Task Test_Get_All_ProductTypes()
        {
            using (var client = new APIClientProvider().Client)
            {
                /*
                    ARRANGE
                */


                /*
                    ACT
                */
                var response = await client.GetAsync("/api/productTypes");


                string responseBody = await response.Content.ReadAsStringAsync();
                var productTypesList = JsonConvert.DeserializeObject<List<ProductType>>(responseBody);

                /*
                    ASSERT
                */
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.True(productTypesList.Count > 0);
            }
        }

        [Fact]
        public async Task Test_Get_Single_ProductType()
        {

            using (var client = new APIClientProvider().Client)
            {
                var response = await client.GetAsync("/productType/1");


                string responseBody = await response.Content.ReadAsStringAsync();
                var productType = JsonConvert.DeserializeObject<ProductType>(responseBody);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal("test", productType.Name);
                Assert.NotNull(productType);
            }
        }
    }
}
