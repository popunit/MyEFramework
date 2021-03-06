﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17626
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eCommerce.Services.WcfClient
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName = "IUserService")]
    public interface IUserService
    {
        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IUserService/GetUserByName", ReplyAction = "http://tempuri.org/IUserService/GetUserByNameResponse")]
        eCommerce.Services.WcfClient.Entities.User GetUserByName(string userName);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IUserService/GetUserByName", ReplyAction = "http://tempuri.org/IUserService/GetUserByNameResponse")]
        System.Threading.Tasks.Task<eCommerce.Services.WcfClient.Entities.User> GetUserByNameAsync(string userName);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IUserService/GetUserByEmail", ReplyAction = "http://tempuri.org/IUserService/GetUserByEmailResponse")]
        eCommerce.Services.WcfClient.Entities.User GetUserByEmail(string email);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IUserService/GetUserByEmail", ReplyAction = "http://tempuri.org/IUserService/GetUserByEmailResponse")]
        System.Threading.Tasks.Task<eCommerce.Services.WcfClient.Entities.User> GetUserByEmailAsync(string email);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IUserService/GetUserByGuid", ReplyAction = "http://tempuri.org/IUserService/GetUserByGuidResponse")]
        eCommerce.Services.WcfClient.Entities.User GetUserByGuid(System.Guid guid);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IUserService/GetUserByGuid", ReplyAction = "http://tempuri.org/IUserService/GetUserByGuidResponse")]
        System.Threading.Tasks.Task<eCommerce.Services.WcfClient.Entities.User> GetUserByGuidAsync(System.Guid guid);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IUserService/GetUserRolesBySystemName", ReplyAction = "http://tempuri.org/IUserService/GetUserRolesBySystemNameResponse")]
        eCommerce.Services.WcfClient.Entities.UserRole[] GetUserRolesBySystemName(string systemRoleName);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IUserService/GetUserRolesBySystemName", ReplyAction = "http://tempuri.org/IUserService/GetUserRolesBySystemNameResponse")]
        System.Threading.Tasks.Task<eCommerce.Services.WcfClient.Entities.UserRole[]> GetUserRolesBySystemNameAsync(string systemRoleName);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IUserService/AddUserRole", ReplyAction = "http://tempuri.org/IUserService/AddUserRoleResponse")]
        bool AddUserRole(eCommerce.Services.WcfClient.Entities.UserRole userRole);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IUserService/AddUserRole", ReplyAction = "http://tempuri.org/IUserService/AddUserRoleResponse")]
        System.Threading.Tasks.Task<bool> AddUserRoleAsync(eCommerce.Services.WcfClient.Entities.UserRole userRole);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IUserService/CreateGuest", ReplyAction = "http://tempuri.org/IUserService/CreateGuestResponse")]
        eCommerce.Services.WcfClient.Entities.User CreateGuest();

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IUserService/CreateGuest", ReplyAction = "http://tempuri.org/IUserService/CreateGuestResponse")]
        System.Threading.Tasks.Task<eCommerce.Services.WcfClient.Entities.User> CreateGuestAsync();

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IUserService/UpdateCustomerRole", ReplyAction = "http://tempuri.org/IUserService/UpdateCustomerRoleResponse")]
        bool UpdateCustomerRole(eCommerce.Services.WcfClient.Entities.UserRole userRole);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IUserService/UpdateCustomerRole", ReplyAction = "http://tempuri.org/IUserService/UpdateCustomerRoleResponse")]
        System.Threading.Tasks.Task<bool> UpdateCustomerRoleAsync(eCommerce.Services.WcfClient.Entities.UserRole userRole);
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUserServiceChannel : IUserService, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UserServiceClient : System.ServiceModel.ClientBase<IUserService>, IUserService
    {

        public UserServiceClient()
        {
        }

        public UserServiceClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public UserServiceClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public UserServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public UserServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        public eCommerce.Services.WcfClient.Entities.User GetUserByName(string userName)
        {
            return base.Channel.GetUserByName(userName);
        }

        public System.Threading.Tasks.Task<eCommerce.Services.WcfClient.Entities.User> GetUserByNameAsync(string userName)
        {
            return base.Channel.GetUserByNameAsync(userName);
        }

        public eCommerce.Services.WcfClient.Entities.User GetUserByEmail(string email)
        {
            return base.Channel.GetUserByEmail(email);
        }

        public System.Threading.Tasks.Task<eCommerce.Services.WcfClient.Entities.User> GetUserByEmailAsync(string email)
        {
            return base.Channel.GetUserByEmailAsync(email);
        }

        public eCommerce.Services.WcfClient.Entities.User GetUserByGuid(System.Guid guid)
        {
            return base.Channel.GetUserByGuid(guid);
        }

        public System.Threading.Tasks.Task<eCommerce.Services.WcfClient.Entities.User> GetUserByGuidAsync(System.Guid guid)
        {
            return base.Channel.GetUserByGuidAsync(guid);
        }

        public eCommerce.Services.WcfClient.Entities.UserRole[] GetUserRolesBySystemName(string systemRoleName)
        {
            return base.Channel.GetUserRolesBySystemName(systemRoleName);
        }

        public System.Threading.Tasks.Task<eCommerce.Services.WcfClient.Entities.UserRole[]> GetUserRolesBySystemNameAsync(string systemRoleName)
        {
            return base.Channel.GetUserRolesBySystemNameAsync(systemRoleName);
        }

        public bool AddUserRole(eCommerce.Services.WcfClient.Entities.UserRole userRole)
        {
            return base.Channel.AddUserRole(userRole);
        }

        public System.Threading.Tasks.Task<bool> AddUserRoleAsync(eCommerce.Services.WcfClient.Entities.UserRole userRole)
        {
            return base.Channel.AddUserRoleAsync(userRole);
        }

        public eCommerce.Services.WcfClient.Entities.User CreateGuest()
        {
            return base.Channel.CreateGuest();
        }

        public System.Threading.Tasks.Task<eCommerce.Services.WcfClient.Entities.User> CreateGuestAsync()
        {
            return base.Channel.CreateGuestAsync();
        }

        public bool UpdateCustomerRole(eCommerce.Services.WcfClient.Entities.UserRole userRole)
        {
            return base.Channel.UpdateCustomerRole(userRole);
        }

        public System.Threading.Tasks.Task<bool> UpdateCustomerRoleAsync(eCommerce.Services.WcfClient.Entities.UserRole userRole)
        {
            return base.Channel.UpdateCustomerRoleAsync(userRole);
        }
    }
}
