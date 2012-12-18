﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eCommerce.Core
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="EntityBase", Namespace="http://schemas.datacontract.org/2004/07/eCommerce.Core")]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(eCommerce.Data.Domain.Users.Entities.UserRole))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(eCommerce.Data.Domain.Users.Entities.User))]
    public partial class EntityBase : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private long IdField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
    }
}
namespace eCommerce.Data.Domain.Users.Entities
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UserRole", Namespace="http://schemas.datacontract.org/2004/07/eCommerce.Data.Domain.Users.Entities")]
    public partial class UserRole : eCommerce.Core.EntityBase
    {
        
        private bool ActivedField;
        
        private bool IsSystemRoleField;
        
        private string RoleNameField;
        
        private string SystemNameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Actived
        {
            get
            {
                return this.ActivedField;
            }
            set
            {
                this.ActivedField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsSystemRole
        {
            get
            {
                return this.IsSystemRoleField;
            }
            set
            {
                this.IsSystemRoleField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string RoleName
        {
            get
            {
                return this.RoleNameField;
            }
            set
            {
                this.RoleNameField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SystemName
        {
            get
            {
                return this.SystemNameField;
            }
            set
            {
                this.SystemNameField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="User", Namespace="http://schemas.datacontract.org/2004/07/eCommerce.Data.Domain.Users.Entities")]
    public partial class User : eCommerce.Core.EntityBase
    {
        
        private bool ActivedField;
        
        private bool DeletedField;
        
        private string EmailField;
        
        private eCommerce.Data.Domain.Users.PasswordKit PasswordKitField;
        
        private System.Guid UserGuidField;
        
        private string UserNameField;
        
        private eCommerce.Data.Domain.Users.Entities.UserRole[] UserRolesField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Actived
        {
            get
            {
                return this.ActivedField;
            }
            set
            {
                this.ActivedField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Deleted
        {
            get
            {
                return this.DeletedField;
            }
            set
            {
                this.DeletedField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Email
        {
            get
            {
                return this.EmailField;
            }
            set
            {
                this.EmailField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public eCommerce.Data.Domain.Users.PasswordKit PasswordKit
        {
            get
            {
                return this.PasswordKitField;
            }
            set
            {
                this.PasswordKitField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid UserGuid
        {
            get
            {
                return this.UserGuidField;
            }
            set
            {
                this.UserGuidField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName
        {
            get
            {
                return this.UserNameField;
            }
            set
            {
                this.UserNameField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public eCommerce.Data.Domain.Users.Entities.UserRole[] UserRoles
        {
            get
            {
                return this.UserRolesField;
            }
            set
            {
                this.UserRolesField = value;
            }
        }
    }
}
namespace eCommerce.Data.Domain.Users
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PasswordKit", Namespace="http://schemas.datacontract.org/2004/07/eCommerce.Data.Domain.Users")]
    public partial class PasswordKit : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string PasswordField;
        
        private eCommerce.Data.Common.StringFormatType PasswordFormatTypeField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Password
        {
            get
            {
                return this.PasswordField;
            }
            set
            {
                this.PasswordField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public eCommerce.Data.Common.StringFormatType PasswordFormatType
        {
            get
            {
                return this.PasswordFormatTypeField;
            }
            set
            {
                this.PasswordFormatTypeField = value;
            }
        }
    }
}
namespace eCommerce.Data.Common
{
    using System.Runtime.Serialization;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="StringFormatType", Namespace="http://schemas.datacontract.org/2004/07/eCommerce.Data.Common")]
    public enum StringFormatType : int
    {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ClearText = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Hashed = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Encrypted = 2,
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName="IUserService")]
public interface IUserService
{
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetUserByName", ReplyAction="http://tempuri.org/IUserService/GetUserByNameResponse")]
    eCommerce.Data.Domain.Users.Entities.User GetUserByName(string userName);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetUserByName", ReplyAction="http://tempuri.org/IUserService/GetUserByNameResponse")]
    System.Threading.Tasks.Task<eCommerce.Data.Domain.Users.Entities.User> GetUserByNameAsync(string userName);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetUserByEmail", ReplyAction="http://tempuri.org/IUserService/GetUserByEmailResponse")]
    eCommerce.Data.Domain.Users.Entities.User GetUserByEmail(string email);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetUserByEmail", ReplyAction="http://tempuri.org/IUserService/GetUserByEmailResponse")]
    System.Threading.Tasks.Task<eCommerce.Data.Domain.Users.Entities.User> GetUserByEmailAsync(string email);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetUserByGuid", ReplyAction="http://tempuri.org/IUserService/GetUserByGuidResponse")]
    eCommerce.Data.Domain.Users.Entities.User GetUserByGuid(System.Guid guid);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetUserByGuid", ReplyAction="http://tempuri.org/IUserService/GetUserByGuidResponse")]
    System.Threading.Tasks.Task<eCommerce.Data.Domain.Users.Entities.User> GetUserByGuidAsync(System.Guid guid);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/AddUserRole", ReplyAction="http://tempuri.org/IUserService/AddUserRoleResponse")]
    bool AddUserRole(eCommerce.Data.Domain.Users.Entities.UserRole userRole);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/AddUserRole", ReplyAction="http://tempuri.org/IUserService/AddUserRoleResponse")]
    System.Threading.Tasks.Task<bool> AddUserRoleAsync(eCommerce.Data.Domain.Users.Entities.UserRole userRole);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/UpdateCustomerRole", ReplyAction="http://tempuri.org/IUserService/UpdateCustomerRoleResponse")]
    bool UpdateCustomerRole(eCommerce.Data.Domain.Users.Entities.UserRole userRole);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/UpdateCustomerRole", ReplyAction="http://tempuri.org/IUserService/UpdateCustomerRoleResponse")]
    System.Threading.Tasks.Task<bool> UpdateCustomerRoleAsync(eCommerce.Data.Domain.Users.Entities.UserRole userRole);
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
    
    public eCommerce.Data.Domain.Users.Entities.User GetUserByName(string userName)
    {
        return base.Channel.GetUserByName(userName);
    }
    
    public System.Threading.Tasks.Task<eCommerce.Data.Domain.Users.Entities.User> GetUserByNameAsync(string userName)
    {
        return base.Channel.GetUserByNameAsync(userName);
    }
    
    public eCommerce.Data.Domain.Users.Entities.User GetUserByEmail(string email)
    {
        return base.Channel.GetUserByEmail(email);
    }
    
    public System.Threading.Tasks.Task<eCommerce.Data.Domain.Users.Entities.User> GetUserByEmailAsync(string email)
    {
        return base.Channel.GetUserByEmailAsync(email);
    }
    
    public eCommerce.Data.Domain.Users.Entities.User GetUserByGuid(System.Guid guid)
    {
        return base.Channel.GetUserByGuid(guid);
    }
    
    public System.Threading.Tasks.Task<eCommerce.Data.Domain.Users.Entities.User> GetUserByGuidAsync(System.Guid guid)
    {
        return base.Channel.GetUserByGuidAsync(guid);
    }
    
    public bool AddUserRole(eCommerce.Data.Domain.Users.Entities.UserRole userRole)
    {
        return base.Channel.AddUserRole(userRole);
    }
    
    public System.Threading.Tasks.Task<bool> AddUserRoleAsync(eCommerce.Data.Domain.Users.Entities.UserRole userRole)
    {
        return base.Channel.AddUserRoleAsync(userRole);
    }
    
    public bool UpdateCustomerRole(eCommerce.Data.Domain.Users.Entities.UserRole userRole)
    {
        return base.Channel.UpdateCustomerRole(userRole);
    }
    
    public System.Threading.Tasks.Task<bool> UpdateCustomerRoleAsync(eCommerce.Data.Domain.Users.Entities.UserRole userRole)
    {
        return base.Channel.UpdateCustomerRoleAsync(userRole);
    }
}
