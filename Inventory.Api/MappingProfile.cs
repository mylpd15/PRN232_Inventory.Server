using AutoMapper;
using WareSync.Api.DTOs;
using WareSync.Business;
using WareSync.Domain;
using WareSync.Domain.Enums;

namespace WareSync.Api;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //App Users
        CreateMap<CreateUserDto, AppUser>();
        CreateMap<UserCredentialDto, AppUser>();
        CreateMap<AppUser, CreateUserDto>();
        CreateMap<AppUser, UserDto>()
            .ForMember(dest => dest.UserRole, opt => opt.MapFrom(src => src.UserRole.ToString()));
        CreateMap<UserDto, AppUser>()
            .ForMember(dest => dest.UserRole, opt => opt.MapFrom(src => Enum.Parse<UserRole>(src.UserRole)));

        //Customer mappings
        CreateMap<Customer, CustomerDto>();
        CreateMap<CreateCustomerDto, Customer>();
        CreateMap<UpdateCustomerDto, Customer>();

        //Delivery mappings
        CreateMap<Delivery, DeliveryDto>()
            .ForMember(dest => dest.DeliveryDetails, opt => opt.MapFrom(src => src.DeliveryDetails));

        CreateMap<CreateDeliveryDto, Delivery>()
            .ForMember(dest => dest.DeliveryDetails, opt => opt.MapFrom(src => src.DeliveryDetails));

        CreateMap<UpdateDeliveryDto, Delivery>();

        CreateMap<DeliveryDto, Delivery>();

        //DeliveryDetail mappings
        CreateMap<DeliveryDetail, DeliveryDetailDto>()
         .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));

        CreateMap<DeliveryDetailDto, DeliveryDetail>();
        CreateMap<DeliveryDetailCreateDto, DeliveryDetail>();
        CreateMap<DeliveryDetailUpdateDto, DeliveryDetail>();

        CreateMap<CreateDeliveryDetailDto, DeliveryDetail>();

        //Product Mapping
        CreateMap<Product, ProductDto>()
          .ForMember(
            dest => dest.Prices,
            opt => opt.MapFrom(src => src.Prices)
          );
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();
        //CreateMap<CreateProductWithPriceDto, Product>();


        //ProductPrice Mapping
        CreateMap<ProductPrice, ProductPriceDto>();
        CreateMap<CreateProductPriceDto, ProductPrice>();
        CreateMap<UpdateProductPriceDto, ProductPrice>();

        // Inventory Mapping
        CreateMap<Inventory, InventoryDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product!.ProductName))
            .ForMember(dest => dest.WarehouseName, opt => opt.MapFrom(src => src.Warehouse!.WarehouseName));

        CreateMap<CreateInventoryDto, Inventory>();
        CreateMap<UpdateInventoryDto, Inventory>();

        // InventoryLog Mapping
        CreateMap<InventoryLog, InventoryLogDto>();
        CreateMap<CreateInventoryLogDto, InventoryLog>();

        // Provider Mapping
        CreateMap<Provider, ProviderDto>();
        CreateMap<CreateProviderDto, Provider>();
        CreateMap<UpdateProviderDto, Provider>();

        // Order Mapping
        // Order -> OrderDto
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.Provider, opt => opt.MapFrom(src => src.Provider))  // Map entire Provider object
            .ForMember(dest => dest.Warehouse, opt => opt.MapFrom(src => src.Warehouse)) // Map entire Warehouse object
            .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.RejectReason, opt => opt.MapFrom(src => src.RejectReason));
        CreateMap<WareSync.Business.CreateOrderDto, Order>()
            .ForMember(dest => dest.WarehouseID, opt => opt.MapFrom(src => src.WarehouseId));
        CreateMap<UpdateOrderDto, Order>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Status) ? OrderStatus.Pending : Enum.Parse<OrderStatus>(src.Status)))
            .ForMember(dest => dest.WarehouseID, opt => opt.MapFrom(src => src.WarehouseId))
            .ForMember(dest => dest.RejectReason, opt => opt.MapFrom(src => src.RejectReason));

        // OrderDetail Mapping
        CreateMap<OrderDetail, OrderDetailDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product!.ProductName));
        CreateMap<WareSync.Business.CreateOrderDetailDto, OrderDetail>();
        CreateMap<UpdateOrderDetailDto, OrderDetail>();

        // Transfer Mapping
        CreateMap<Transfer, TransferDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.OrderDetail!.Product!.ProductName))
            .ForMember(dest => dest.WarehouseName, opt => opt.MapFrom(src => src.Warehouse!.WarehouseName));
        CreateMap<CreateTransferDto, Transfer>();
        CreateMap<UpdateTransferDto, Transfer>();

        // WarehouseDto -> Warehouse
        CreateMap<Warehouse, WarehouseDto>()
           .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Location != null ? src.Location.LocationName : null)); 
        CreateMap<WarehouseDto, Warehouse>();

      


    }
}