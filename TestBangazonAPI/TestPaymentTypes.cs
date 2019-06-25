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
    public class TestPaymentTypes
    {
        /*This Tests that you can get all PaymentTypes*/
        [Fact]
        public async Task Test_Get_All_PaymentTypes()
        {

            using (var client = new APIClientProvider().Client)
            {
                var response = await client.GetAsync("/api/PaymentTypes");

                string responseBody = await response.Content.ReadAsStringAsync();
                var paymentTypesList = JsonConvert.DeserializeObject<List<PaymentType>>(responseBody);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.True(paymentTypesList.Count > 0);
            }
        }
        /*This Tests that you can get One Single PaymentTypes*/
        [Fact]
        public async Task Test_Get_Single_PaymentTypes()
        {

            using (var client = new APIClientProvider().Client)
            {
                var response = await client.GetAsync("/api/PaymentTypes/1");


                string responseBody = await response.Content.ReadAsStringAsync();
                var paymentType = JsonConvert.DeserializeObject<PaymentType>(responseBody);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal(34542, paymentType.AcctNumber);
                Assert.Equal("Visa", paymentType.Name);
                Assert.Equal(1, paymentType.CustomerId);
                Assert.NotNull(paymentType);
            }
        }
        /*This Tests that you can Create and then Delete  one Single PaymentType*/
        [Fact]
        public async Task Test_Create_And_Delete_PaymentTypes()
        {
            using (var client = new APIClientProvider().Client)
            {
                PaymentType DinersClub = new PaymentType 
                {
                    AcctNumber = 34343,
                    Name  = "Diners Club",
                    CustomerId = 1,
                };
                var dinerClubAsJSON = JsonConvert.SerializeObject(DinersClub);


                var response = await client.PostAsync(
                    "/api/PaymentTypes",
                    new StringContent(dinerClubAsJSON, Encoding.UTF8, "application/json")
                );


                string responseBody = await response.Content.ReadAsStringAsync();
                var newDinersClub = JsonConvert.DeserializeObject<PaymentType>(responseBody);

                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
                Assert.Equal(34343, newDinersClub.AcctNumber);
                Assert.Equal("Diners Club", newDinersClub.Name);
                Assert.Equal(1, newDinersClub.CustomerId);


                var deleteResponse = await client.DeleteAsync($"/api/PaymentTypes/{newDinersClub.Id}");
                Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
            }
        }
        /*This Tests that you can Update a single PaymentTypes*/
        [Fact]
        public async Task Test_Modify_PaymentTypes()
        {
            // New Accout Number Updating
            int newAccountNumber = 34542;

            using (var client = new APIClientProvider().Client)
            {
               
                PaymentType ModPaymentType = new PaymentType
                {
                    AcctNumber = newAccountNumber,
                    Name = "MasterCard",
                    CustomerId = 2
                };
                var modifiedPaymnentTypeAsJSON = JsonConvert.SerializeObject(ModPaymentType);

                var response = await client.PutAsync(
                    "/api/PaymentTypes/2",
                    new StringContent(modifiedPaymnentTypeAsJSON, Encoding.UTF8, "application/json")
                );
                string responseBody = await response.Content.ReadAsStringAsync();

                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);


 
        var getPaymentType = await client.GetAsync("/api/PaymentTypes/1");
                getPaymentType.EnsureSuccessStatusCode();

                string getPaymentTypeBody = await getPaymentType.Content.ReadAsStringAsync();
        PaymentType newPaymentType = JsonConvert.DeserializeObject<PaymentType>(getPaymentTypeBody);

        Assert.Equal(HttpStatusCode.OK, getPaymentType.StatusCode);
                Assert.Equal(newAccountNumber, newPaymentType.AcctNumber);
            }
         }
    }
}
