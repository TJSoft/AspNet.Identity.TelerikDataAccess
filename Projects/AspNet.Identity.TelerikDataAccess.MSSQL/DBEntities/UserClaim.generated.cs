#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the ClassGenerator.ttinclude code generation file.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Common;
using System.Collections.Generic;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Data.Common;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.OpenAccess.Metadata.Fluent.Advanced;
using AspNet.Identity.TelerikDataAccess.MSSQL.DBEntities;

namespace AspNet.Identity.TelerikDataAccess.MSSQL.DBEntities	
{
	public partial class UserClaim
	{
		private int _id;
		public virtual int Id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}
		
		private int _userId;
		public virtual int UserId
		{
			get
			{
				return this._userId;
			}
			set
			{
				this._userId = value;
			}
		}
		
		private string _claimType;
		public virtual string ClaimType
		{
			get
			{
				return this._claimType;
			}
			set
			{
				this._claimType = value;
			}
		}
		
		private string _claimValue;
		public virtual string ClaimValue
		{
			get
			{
				return this._claimValue;
			}
			set
			{
				this._claimValue = value;
			}
		}
		
		private User _user;
		public virtual User User
		{
			get
			{
				return this._user;
			}
			set
			{
				this._user = value;
			}
		}
		
	}
}
#pragma warning restore 1591