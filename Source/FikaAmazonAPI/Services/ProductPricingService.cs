﻿using FikaAmazonAPI.AmazonSpApiSDK.Models.ProductPricing;
using FikaAmazonAPI.Parameter.ProductPricing;
using FikaAmazonAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FikaAmazonAPI.Services
{
    public class ProductPricingService : RequestService
    {
        public ProductPricingService(AmazonCredential amazonCredential) : base(amazonCredential)
        {

        }


        public IList<Price> GetPricing(ParameterGetPricing parameterGetPricing) =>
            Task.Run(() => GetPricingAsync(parameterGetPricing)).ConfigureAwait(false).GetAwaiter().GetResult();
        public async Task<IList<Price>> GetPricingAsync(ParameterGetPricing parameterGetPricing, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(parameterGetPricing.MarketplaceId))
            {
                parameterGetPricing.MarketplaceId = AmazonCredential.MarketPlace.ID;
            }

            var param = parameterGetPricing.getParameters();

            await CreateAuthorizedRequestAsync(ProductPricingApiUrls.GetPricing, RestSharp.Method.Get, param, cancellationToken: cancellationToken);
            var response = await ExecuteRequestAsync<GetPricingResponse>(RateLimitType.ProductPricing_GetPricing, cancellationToken);
            if (response != null && response.Payload != null)
                return response.Payload;
            return null;
        }

        public IList<Price> GetCompetitivePricing(ParameterGetCompetitivePricing parameterGetCompetitivePricing) =>
            Task.Run(() => GetCompetitivePricingAsync(parameterGetCompetitivePricing)).ConfigureAwait(false).GetAwaiter().GetResult();
        public async Task<IList<Price>> GetCompetitivePricingAsync(ParameterGetCompetitivePricing parameterGetCompetitivePricing, CancellationToken cancellationToken = default)
        {
            if (parameterGetCompetitivePricing.Skus != null && parameterGetCompetitivePricing.Skus.Count > 0)
            {
                parameterGetCompetitivePricing.Skus = parameterGetCompetitivePricing.Skus.Select(a => Uri.EscapeDataString(a)).ToList();
            }

            var param = parameterGetCompetitivePricing.getParameters();

            await CreateAuthorizedRequestAsync(ProductPricingApiUrls.GetCompetitivePricing, RestSharp.Method.Get, param, cancellationToken: cancellationToken);
            var response = await ExecuteRequestAsync<GetPricingResponse>(RateLimitType.ProductPricing_GetCompetitivePricing, cancellationToken);
            if (response != null && response.Payload != null)
                return response.Payload;
            return null;
        }

        public GetOffersResult GetListingOffers(ParameterGetListingOffers parameterGetListingOffers) =>
            Task.Run(() => GetListingOffersAsync(parameterGetListingOffers)).ConfigureAwait(false).GetAwaiter().GetResult();
        public async Task<GetOffersResult> GetListingOffersAsync(ParameterGetListingOffers parameterGetListingOffers, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(parameterGetListingOffers.MarketplaceId))
                parameterGetListingOffers.MarketplaceId = AmazonCredential.MarketPlace.ID;

            var param = parameterGetListingOffers.getParameters();

            await CreateAuthorizedRequestAsync(ProductPricingApiUrls.GetListingOffersBySellerSku(parameterGetListingOffers.SellerSKU), RestSharp.Method.Get, param, cancellationToken: cancellationToken);
            var response = await ExecuteRequestAsync<GetOffersResponse>(RateLimitType.ProductPricing_GetListingOffers, cancellationToken);
            if (response != null && response.Payload != null)
                return response.Payload;
            return null;
        }

        public GetOffersResult GetItemOffers(ParameterGetItemOffers parameterGetItemOffers) =>
            Task.Run(() => GetItemOffersAsync(parameterGetItemOffers)).ConfigureAwait(false).GetAwaiter().GetResult();
        public async Task<GetOffersResult> GetItemOffersAsync(ParameterGetItemOffers parameterGetItemOffers, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(parameterGetItemOffers.MarketplaceId))
            {
                parameterGetItemOffers.MarketplaceId = AmazonCredential.MarketPlace.ID;
            }

            var param = parameterGetItemOffers.getParameters();

            await CreateAuthorizedRequestAsync(ProductPricingApiUrls.GetItemOffers(parameterGetItemOffers.Asin), RestSharp.Method.Get, param, cancellationToken: cancellationToken);
            var response = await ExecuteRequestAsync<GetOffersResponse>(RateLimitType.ProductPricing_GetItemOffers, cancellationToken);
            if (response != null && response.Payload != null)
                return response.Payload;
            return null;
        }

        public GetBatchOffersResponse GetItemOffersBatch(ParameterGetItemOffersBatchRequest parameterGetItemOffersBatchRequest) =>
    Task.Run(() => GetItemOffersBatchAsync(parameterGetItemOffersBatchRequest)).ConfigureAwait(false).GetAwaiter().GetResult();
        public async Task<GetBatchOffersResponse> GetItemOffersBatchAsync(ParameterGetItemOffersBatchRequest parameterGetItemOffersBatchRequest, CancellationToken cancellationToken = default)
        {
            /*
             "requests": [
                {
                    "uri": "/products/pricing/v0/items/{ASIN}/offers",
                    "method": "GET",
                    "queryParams": {
                        "MarketplaceId": "ATVPDKIKX0DER",
                        "ItemCondition": "New",
                        "CustomerType": "Consumer"
                    }
                }
            ]
             */

            await CreateAuthorizedRequestAsync(ProductPricingApiUrls.GetBatchItemOffers, RestSharp.Method.Post, postJsonObj: parameterGetItemOffersBatchRequest, cancellationToken: cancellationToken);
            return await ExecuteRequestAsync<GetBatchOffersResponse>(RateLimitType.ProductPricing_GetItemOffersBatch, cancellationToken);
        }


        public GetBatchOffersResponse GetListingOffersBatch(ParameterGetListingOffersBatchRequest parameterGetItemOffersBatchRequest) =>
Task.Run(() => GetListingOffersBatchAsync(parameterGetItemOffersBatchRequest)).ConfigureAwait(false).GetAwaiter().GetResult();
        public async Task<GetBatchOffersResponse> GetListingOffersBatchAsync(ParameterGetListingOffersBatchRequest parameterGetItemOffersBatchRequest, CancellationToken cancellationToken = default)
        {
            /*
             "requests": [
                {
                    "uri": "/products/pricing/v0/listings/{SellerSKU}/offers",
                    "method": "GET",
                    "queryParams": {
                        "MarketplaceId": "ATVPDKIKX0DER",
                        "ItemCondition": "New",
                        "CustomerType": "Consumer"
                    }
                }
            ]
             */

            await CreateAuthorizedRequestAsync(ProductPricingApiUrls.GetBatchListingOffers, RestSharp.Method.Post, postJsonObj: parameterGetItemOffersBatchRequest, cancellationToken: cancellationToken);
            return await ExecuteRequestAsync<GetBatchOffersResponse>(RateLimitType.ProductPricing_GetListingOffersBatch, cancellationToken);
        }

    }
}
