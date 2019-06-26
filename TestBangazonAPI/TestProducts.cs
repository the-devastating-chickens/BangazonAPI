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
    }
}
