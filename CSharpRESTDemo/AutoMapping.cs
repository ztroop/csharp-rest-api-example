using AutoMapper;
using CSharpRESTDemo.Dtos;
using CSharpRESTDemo.Entities;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<Customer, CustomerDto>();
        CreateMap<CustomerDto, Customer>();

        CreateMap<Customer, CustomerCreateDto>();
        CreateMap<CustomerCreateDto, Customer>();

        CreateMap<Customer, CustomerUpdateDto>();
        CreateMap<CustomerUpdateDto, Customer>();
    }
}