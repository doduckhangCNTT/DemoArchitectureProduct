﻿using AutoMapper;
using DemoCICD.Contract.Services.V1.Product;
using DemoCICD.Domain.Entities;

namespace DemoCICD.Application.Mapper;
public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        // V1
        CreateMap<Product, Response.ProductResponse>().ReverseMap();

        // V2
        CreateMap<Product, Contract.Services.V2.Product.Response.ProductResponse>().ReverseMap();
    }
}
