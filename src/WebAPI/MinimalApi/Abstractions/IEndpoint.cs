﻿namespace OfficePerformanceReview.WebAPI.MinimalApi.Abstractions
{
    public interface IEndpoint
    {
        void MapEndpoint(IEndpointRouteBuilder app);
    }
}
