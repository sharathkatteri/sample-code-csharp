﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers.Bases;

namespace net.authorize.sample
{
    public class ChargeCreditCard
    {
        public static void Run()
        {
            Console.WriteLine("Charge Credit Card Sample");

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            // define the merchant information (authentication / transaction id)
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = "8U6D9WxnU9t7",
                ItemElementName = ItemChoiceType.transactionKey,
                Item = "3cEYe68N69LaA2cB",
            };

            var creditCard = new creditCardType
            {
                cardNumber = "4111111111111111",        // the credit card number
                expirationDate = "0718"                 // the expiration date (must be a future date)
            };

            //standard api call to retrieve response
            var paymentType = new paymentType { Item = creditCard };

            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),    // charge the card
                amount = 5.45m,
                payment = paymentType
            };

            
            var request = new createTransactionRequest { transactionRequest = transactionRequest };
            
            // instantiate the contoller that will call the service
            var controller = new createTransactionController(request);
            controller.Execute();
            
            // get the response from the service (errors contained if any)
            var response = controller.GetApiResponse();
           
        }
    }
}
