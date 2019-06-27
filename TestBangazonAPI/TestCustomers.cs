using System;
using System.Net;
using Newtonsoft.Json;
using Xunit;
using BangazonAPI.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TestBangazonAPI
{
    public class TestCustomers
    {
        [Fact]
        public async Task Test_Get_All_Customers()
        {
            using (var client = new APIClientProvider().Client)
            {
                /*
                    ARRANGE
                */


                /*
                    ACT
                */
                var response = await client.GetAsync("/api/customers");


                string responseBody = await response.Content.ReadAsStringAsync();
                var customers = JsonConvert.DeserializeObject<List<Customer>>(responseBody);

                /*
                    ASSERT
                */
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.True(customers.Count > 0);
            }
        }

        [Fact]
        public async Task Test_Get_Customer_By_Id()
        {
            using (var client = new APIClientProvider().Client)
            {

                /*
                    ACT
                */
                var response = await client.GetAsync("/api/customers/1");

                string responseBody = await response.Content.ReadAsStringAsync();
                var customer = JsonConvert.DeserializeObject<Customer>(responseBody);

                /*
                    ASSERT
                */
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal("Jonathan", customer.FirstName);
                Assert.Equal("Schaffer", customer.LastName);
                Assert.NotNull(customer);
            }
        }

        //[Fact]
        //public async Task Test_Get_Customers_Product_By_Id()
        //{
        //    using (var client = new APIClientProvider().Client)
        //    {
        //        /*
        //             ARRANGE
        //         */


        //        /*
        //            ACT
        //        */
        //        var response = await client.GetAsync("/api/customers/1?_include=products");

        //        string responseBody = await response.Content.ReadAsStringAsync();
        //        var customer = JsonConvert.DeserializeObject<Customer>(responseBody);
        //        var productList = JsonConvert.DeserializeObject<Product>(responseBody);

        //        /*
        //            ASSERT
        //        */
        //        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //        Assert.Equal("Jonathan", customer.FirstName);
        //        Assert.Equal("Schaffer", customer.LastName);
        //        Assert.Equal("Lord of the Rings", productList.Title);
        //        Assert.Equal("A book about a ring and an epic journey to a volcano.", productList.Description);
        //        Assert.Equal(10, productList.Price);
        //        Assert.Equal(5, productList.Quantity);
        //        Assert.NotNull(customer);
        //        Assert.NotNull(productList);
        //    }
        //}
    }
}
