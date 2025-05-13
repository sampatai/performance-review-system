global using OfficePerformanceReview.WebAPI.MinimalApi.Abstractions;

global using MediatR;
global using Microsoft.AspNetCore.Mvc;
global using System.Net;
global using OfficePerformanceReview.Application.CQRS.Command.User;
global using OfficePerformanceReview.Application.Common.Model;
global using OfficePerformanceReview.Application.CQRS.Query.Login;
global using System.Security.Claims;
global using Microsoft.AspNetCore.Authorization;