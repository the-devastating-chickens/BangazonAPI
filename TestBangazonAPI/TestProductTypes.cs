using System;
using System.Net;
using Newtonsoft.Json;
using Xunit;
using BangazonAPI.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

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
                var response = await client.GetAsync("/api/ProductTypes");


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
                var response = await client.GetAsync("/api/ProductTypes/1");


                string responseBody = await response.Content.ReadAsStringAsync();
                var productType = JsonConvert.DeserializeObject<ProductType>(responseBody);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal("Book", productType.Name);
                Assert.NotNull(productType);
            }
        }

        [Fact]
        public async Task Test_Create_And_Delete_ProductTypes()
        {
            using (var client = new APIClientProvider().Client)
            {
                ProductType productName = new ProductType
                {
                    Name = "product1"
                };
                var productNameAsJSON = JsonConvert.SerializeObject(productName);


                var response = await client.PostAsync("/api/ProductTypes",
                               new StringContent(productNameAsJSON, Encoding.UTF8, "application/json")
                );


                string responseBody = await response.Content.ReadAsStringAsync();
                var newProductName = JsonConvert.DeserializeObject<ProductType>(responseBody);

                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
                Assert.Equal("product1", newProductName.Name);

                var deleteResponse = await client.DeleteAsync($"/api/ProductTypes/{newProductName.Id}");
                Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
            }
        }

        [Fact]
        public async Task Test_Modify_ProductType()
        {
            string newName = "product1";

            using (var client = new APIClientProvider().Client)
            {
                /*
                    PUT section
                 */
                ProductType modifiedTest = new ProductType
                {
                    Id = 1,
                    Name = newName
                };
                var modifiedTestAsJSON = JsonConvert.SerializeObject(modifiedTest);

                var response = await client.PutAsync(
                    "/api/ProductTypes/1",
                    new StringContent(modifiedTestAsJSON, Encoding.UTF8, "application/json")
                );
                string responseBody = await response.Content.ReadAsStringAsync();

                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

                /*
                    GET section
                 */
                var getTest = await client.GetAsync("/api/ProductTypes/1");
                getTest.EnsureSuccessStatusCode();

                string getTestBody = await getTest.Content.ReadAsStringAsync();
                ProductType newTest = JsonConvert.DeserializeObject<ProductType>(getTestBody);

                Assert.Equal(HttpStatusCode.OK, getTest.StatusCode);
                Assert.Equal(newName, newTest.Name);
            }
        }

    }
}
