﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Metric_AUTOMATION
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="ATDataBase")]
	public partial class MyLinqDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertCI_API_Run(CI_API_Run instance);
    partial void UpdateCI_API_Run(CI_API_Run instance);
    partial void DeleteCI_API_Run(CI_API_Run instance);
    partial void InsertCI_LOG_CASE(CI_LOG_CASE instance);
    partial void UpdateCI_LOG_CASE(CI_LOG_CASE instance);
    partial void DeleteCI_LOG_CASE(CI_LOG_CASE instance);
    partial void InsertCI_LOG_CaseInfo(CI_LOG_CaseInfo instance);
    partial void UpdateCI_LOG_CaseInfo(CI_LOG_CaseInfo instance);
    partial void DeleteCI_LOG_CaseInfo(CI_LOG_CaseInfo instance);
    partial void InsertCI_LOG_JOB(CI_LOG_JOB instance);
    partial void UpdateCI_LOG_JOB(CI_LOG_JOB instance);
    partial void DeleteCI_LOG_JOB(CI_LOG_JOB instance);
    #endregion
		
		public MyLinqDataContext() : 
				base(global::Metric_AUTOMATION.Properties.Settings.Default.ATDataBaseConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public MyLinqDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MyLinqDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MyLinqDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MyLinqDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<CI_API_Run> CI_API_Runs
		{
			get
			{
				return this.GetTable<CI_API_Run>();
			}
		}
		
		public System.Data.Linq.Table<CI_LOG_CASE> CI_LOG_CASEs
		{
			get
			{
				return this.GetTable<CI_LOG_CASE>();
			}
		}
		
		public System.Data.Linq.Table<CI_LOG_CaseInfo> CI_LOG_CaseInfos
		{
			get
			{
				return this.GetTable<CI_LOG_CaseInfo>();
			}
		}
		
		public System.Data.Linq.Table<CI_LOG_JOB> CI_LOG_JOBs
		{
			get
			{
				return this.GetTable<CI_LOG_JOB>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.CI_API_Run")]
	public partial class CI_API_Run : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private long _ID;
		
		private string _UserID;
		
		private string _RequestType;
		
		private string _ServerIP;
		
		private string _CallerIP;
		
		private string _ActionTime;
		
		private string _ResultCode;
		
		private System.Nullable<System.DateTime> _CallTime;
		
		private string _ENV;
		
		private System.Nullable<long> _CaseInfoID;
		
		private System.Nullable<long> _CaseID;
		
		private System.Nullable<int> _CaseResult;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(long value);
    partial void OnIDChanged();
    partial void OnUserIDChanging(string value);
    partial void OnUserIDChanged();
    partial void OnRequestTypeChanging(string value);
    partial void OnRequestTypeChanged();
    partial void OnServerIPChanging(string value);
    partial void OnServerIPChanged();
    partial void OnCallerIPChanging(string value);
    partial void OnCallerIPChanged();
    partial void OnActionTimeChanging(string value);
    partial void OnActionTimeChanged();
    partial void OnResultCodeChanging(string value);
    partial void OnResultCodeChanged();
    partial void OnCallTimeChanging(System.Nullable<System.DateTime> value);
    partial void OnCallTimeChanged();
    partial void OnENVChanging(string value);
    partial void OnENVChanged();
    partial void OnCaseInfoIDChanging(System.Nullable<long> value);
    partial void OnCaseInfoIDChanged();
    partial void OnCaseIDChanging(System.Nullable<long> value);
    partial void OnCaseIDChanged();
    partial void OnCaseResultChanging(System.Nullable<int> value);
    partial void OnCaseResultChanged();
    #endregion
		
		public CI_API_Run()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="BigInt NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public long ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserID", DbType="VarChar(50)")]
		public string UserID
		{
			get
			{
				return this._UserID;
			}
			set
			{
				if ((this._UserID != value))
				{
					this.OnUserIDChanging(value);
					this.SendPropertyChanging();
					this._UserID = value;
					this.SendPropertyChanged("UserID");
					this.OnUserIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RequestType", DbType="VarChar(200)")]
		public string RequestType
		{
			get
			{
				return this._RequestType;
			}
			set
			{
				if ((this._RequestType != value))
				{
					this.OnRequestTypeChanging(value);
					this.SendPropertyChanging();
					this._RequestType = value;
					this.SendPropertyChanged("RequestType");
					this.OnRequestTypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ServerIP", DbType="VarChar(50)")]
		public string ServerIP
		{
			get
			{
				return this._ServerIP;
			}
			set
			{
				if ((this._ServerIP != value))
				{
					this.OnServerIPChanging(value);
					this.SendPropertyChanging();
					this._ServerIP = value;
					this.SendPropertyChanged("ServerIP");
					this.OnServerIPChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CallerIP", DbType="VarChar(50)")]
		public string CallerIP
		{
			get
			{
				return this._CallerIP;
			}
			set
			{
				if ((this._CallerIP != value))
				{
					this.OnCallerIPChanging(value);
					this.SendPropertyChanging();
					this._CallerIP = value;
					this.SendPropertyChanged("CallerIP");
					this.OnCallerIPChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ActionTime", DbType="NVarChar(50)")]
		public string ActionTime
		{
			get
			{
				return this._ActionTime;
			}
			set
			{
				if ((this._ActionTime != value))
				{
					this.OnActionTimeChanging(value);
					this.SendPropertyChanging();
					this._ActionTime = value;
					this.SendPropertyChanged("ActionTime");
					this.OnActionTimeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ResultCode", DbType="VarChar(50)")]
		public string ResultCode
		{
			get
			{
				return this._ResultCode;
			}
			set
			{
				if ((this._ResultCode != value))
				{
					this.OnResultCodeChanging(value);
					this.SendPropertyChanging();
					this._ResultCode = value;
					this.SendPropertyChanged("ResultCode");
					this.OnResultCodeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CallTime", DbType="DateTime")]
		public System.Nullable<System.DateTime> CallTime
		{
			get
			{
				return this._CallTime;
			}
			set
			{
				if ((this._CallTime != value))
				{
					this.OnCallTimeChanging(value);
					this.SendPropertyChanging();
					this._CallTime = value;
					this.SendPropertyChanged("CallTime");
					this.OnCallTimeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ENV", DbType="VarChar(200)")]
		public string ENV
		{
			get
			{
				return this._ENV;
			}
			set
			{
				if ((this._ENV != value))
				{
					this.OnENVChanging(value);
					this.SendPropertyChanging();
					this._ENV = value;
					this.SendPropertyChanged("ENV");
					this.OnENVChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CaseInfoID", DbType="BigInt")]
		public System.Nullable<long> CaseInfoID
		{
			get
			{
				return this._CaseInfoID;
			}
			set
			{
				if ((this._CaseInfoID != value))
				{
					this.OnCaseInfoIDChanging(value);
					this.SendPropertyChanging();
					this._CaseInfoID = value;
					this.SendPropertyChanged("CaseInfoID");
					this.OnCaseInfoIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CaseID", DbType="BigInt")]
		public System.Nullable<long> CaseID
		{
			get
			{
				return this._CaseID;
			}
			set
			{
				if ((this._CaseID != value))
				{
					this.OnCaseIDChanging(value);
					this.SendPropertyChanging();
					this._CaseID = value;
					this.SendPropertyChanged("CaseID");
					this.OnCaseIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CaseResult", DbType="Int")]
		public System.Nullable<int> CaseResult
		{
			get
			{
				return this._CaseResult;
			}
			set
			{
				if ((this._CaseResult != value))
				{
					this.OnCaseResultChanging(value);
					this.SendPropertyChanging();
					this._CaseResult = value;
					this.SendPropertyChanged("CaseResult");
					this.OnCaseResultChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.CI_LOG_CASE")]
	public partial class CI_LOG_CASE : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private long _CaseID;
		
		private System.Nullable<long> _JobID;
		
		private System.Nullable<long> _CaseInfoID;
		
		private System.Nullable<System.DateTime> _GmtCreate;
		
		private System.Nullable<int> _Result;
		
		private string _Author;
		
		private string _Email;
		
		private string _Name;
		
		private string _Category;
		
		private string _Priority;
		
		private string _APP;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnCaseIDChanging(long value);
    partial void OnCaseIDChanged();
    partial void OnJobIDChanging(System.Nullable<long> value);
    partial void OnJobIDChanged();
    partial void OnCaseInfoIDChanging(System.Nullable<long> value);
    partial void OnCaseInfoIDChanged();
    partial void OnGmtCreateChanging(System.Nullable<System.DateTime> value);
    partial void OnGmtCreateChanged();
    partial void OnResultChanging(System.Nullable<int> value);
    partial void OnResultChanged();
    partial void OnAuthorChanging(string value);
    partial void OnAuthorChanged();
    partial void OnEmailChanging(string value);
    partial void OnEmailChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnCategoryChanging(string value);
    partial void OnCategoryChanged();
    partial void OnPriorityChanging(string value);
    partial void OnPriorityChanged();
    partial void OnAPPChanging(string value);
    partial void OnAPPChanged();
    #endregion
		
		public CI_LOG_CASE()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CaseID", AutoSync=AutoSync.OnInsert, DbType="BigInt NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public long CaseID
		{
			get
			{
				return this._CaseID;
			}
			set
			{
				if ((this._CaseID != value))
				{
					this.OnCaseIDChanging(value);
					this.SendPropertyChanging();
					this._CaseID = value;
					this.SendPropertyChanged("CaseID");
					this.OnCaseIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_JobID", DbType="BigInt")]
		public System.Nullable<long> JobID
		{
			get
			{
				return this._JobID;
			}
			set
			{
				if ((this._JobID != value))
				{
					this.OnJobIDChanging(value);
					this.SendPropertyChanging();
					this._JobID = value;
					this.SendPropertyChanged("JobID");
					this.OnJobIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CaseInfoID", DbType="BigInt")]
		public System.Nullable<long> CaseInfoID
		{
			get
			{
				return this._CaseInfoID;
			}
			set
			{
				if ((this._CaseInfoID != value))
				{
					this.OnCaseInfoIDChanging(value);
					this.SendPropertyChanging();
					this._CaseInfoID = value;
					this.SendPropertyChanged("CaseInfoID");
					this.OnCaseInfoIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GmtCreate", DbType="DateTime")]
		public System.Nullable<System.DateTime> GmtCreate
		{
			get
			{
				return this._GmtCreate;
			}
			set
			{
				if ((this._GmtCreate != value))
				{
					this.OnGmtCreateChanging(value);
					this.SendPropertyChanging();
					this._GmtCreate = value;
					this.SendPropertyChanged("GmtCreate");
					this.OnGmtCreateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Result", DbType="Int")]
		public System.Nullable<int> Result
		{
			get
			{
				return this._Result;
			}
			set
			{
				if ((this._Result != value))
				{
					this.OnResultChanging(value);
					this.SendPropertyChanging();
					this._Result = value;
					this.SendPropertyChanged("Result");
					this.OnResultChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Author", DbType="VarChar(200)")]
		public string Author
		{
			get
			{
				return this._Author;
			}
			set
			{
				if ((this._Author != value))
				{
					this.OnAuthorChanging(value);
					this.SendPropertyChanging();
					this._Author = value;
					this.SendPropertyChanged("Author");
					this.OnAuthorChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Email", DbType="VarChar(200)")]
		public string Email
		{
			get
			{
				return this._Email;
			}
			set
			{
				if ((this._Email != value))
				{
					this.OnEmailChanging(value);
					this.SendPropertyChanging();
					this._Email = value;
					this.SendPropertyChanged("Email");
					this.OnEmailChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="VarChar(200)")]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Category", DbType="VarChar(200)")]
		public string Category
		{
			get
			{
				return this._Category;
			}
			set
			{
				if ((this._Category != value))
				{
					this.OnCategoryChanging(value);
					this.SendPropertyChanging();
					this._Category = value;
					this.SendPropertyChanged("Category");
					this.OnCategoryChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Priority", DbType="VarChar(200)")]
		public string Priority
		{
			get
			{
				return this._Priority;
			}
			set
			{
				if ((this._Priority != value))
				{
					this.OnPriorityChanging(value);
					this.SendPropertyChanging();
					this._Priority = value;
					this.SendPropertyChanged("Priority");
					this.OnPriorityChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_APP", DbType="VarChar(200)")]
		public string APP
		{
			get
			{
				return this._APP;
			}
			set
			{
				if ((this._APP != value))
				{
					this.OnAPPChanging(value);
					this.SendPropertyChanging();
					this._APP = value;
					this.SendPropertyChanged("APP");
					this.OnAPPChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.CI_LOG_CaseInfo")]
	public partial class CI_LOG_CaseInfo : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private long _CaseInfoID;
		
		private System.Nullable<System.DateTime> _GmtCreate;
		
		private System.Nullable<System.DateTime> _GmtModified;
		
		private System.Nullable<int> _Result;
		
		private string _ProjectName;
		
		private string _ClassName;
		
		private string _MethodName;
		
		private System.Nullable<long> _LatestCaseID;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnCaseInfoIDChanging(long value);
    partial void OnCaseInfoIDChanged();
    partial void OnGmtCreateChanging(System.Nullable<System.DateTime> value);
    partial void OnGmtCreateChanged();
    partial void OnGmtModifiedChanging(System.Nullable<System.DateTime> value);
    partial void OnGmtModifiedChanged();
    partial void OnResultChanging(System.Nullable<int> value);
    partial void OnResultChanged();
    partial void OnProjectNameChanging(string value);
    partial void OnProjectNameChanged();
    partial void OnClassNameChanging(string value);
    partial void OnClassNameChanged();
    partial void OnMethodNameChanging(string value);
    partial void OnMethodNameChanged();
    partial void OnLatestCaseIDChanging(System.Nullable<long> value);
    partial void OnLatestCaseIDChanged();
    #endregion
		
		public CI_LOG_CaseInfo()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CaseInfoID", AutoSync=AutoSync.OnInsert, DbType="BigInt NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public long CaseInfoID
		{
			get
			{
				return this._CaseInfoID;
			}
			set
			{
				if ((this._CaseInfoID != value))
				{
					this.OnCaseInfoIDChanging(value);
					this.SendPropertyChanging();
					this._CaseInfoID = value;
					this.SendPropertyChanged("CaseInfoID");
					this.OnCaseInfoIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GmtCreate", DbType="DateTime")]
		public System.Nullable<System.DateTime> GmtCreate
		{
			get
			{
				return this._GmtCreate;
			}
			set
			{
				if ((this._GmtCreate != value))
				{
					this.OnGmtCreateChanging(value);
					this.SendPropertyChanging();
					this._GmtCreate = value;
					this.SendPropertyChanged("GmtCreate");
					this.OnGmtCreateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GmtModified", DbType="DateTime")]
		public System.Nullable<System.DateTime> GmtModified
		{
			get
			{
				return this._GmtModified;
			}
			set
			{
				if ((this._GmtModified != value))
				{
					this.OnGmtModifiedChanging(value);
					this.SendPropertyChanging();
					this._GmtModified = value;
					this.SendPropertyChanged("GmtModified");
					this.OnGmtModifiedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Result", DbType="Int")]
		public System.Nullable<int> Result
		{
			get
			{
				return this._Result;
			}
			set
			{
				if ((this._Result != value))
				{
					this.OnResultChanging(value);
					this.SendPropertyChanging();
					this._Result = value;
					this.SendPropertyChanged("Result");
					this.OnResultChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ProjectName", DbType="NVarChar(100)")]
		public string ProjectName
		{
			get
			{
				return this._ProjectName;
			}
			set
			{
				if ((this._ProjectName != value))
				{
					this.OnProjectNameChanging(value);
					this.SendPropertyChanging();
					this._ProjectName = value;
					this.SendPropertyChanged("ProjectName");
					this.OnProjectNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ClassName", DbType="NVarChar(100)")]
		public string ClassName
		{
			get
			{
				return this._ClassName;
			}
			set
			{
				if ((this._ClassName != value))
				{
					this.OnClassNameChanging(value);
					this.SendPropertyChanging();
					this._ClassName = value;
					this.SendPropertyChanged("ClassName");
					this.OnClassNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MethodName", DbType="NVarChar(100)")]
		public string MethodName
		{
			get
			{
				return this._MethodName;
			}
			set
			{
				if ((this._MethodName != value))
				{
					this.OnMethodNameChanging(value);
					this.SendPropertyChanging();
					this._MethodName = value;
					this.SendPropertyChanged("MethodName");
					this.OnMethodNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LatestCaseID", DbType="BigInt")]
		public System.Nullable<long> LatestCaseID
		{
			get
			{
				return this._LatestCaseID;
			}
			set
			{
				if ((this._LatestCaseID != value))
				{
					this.OnLatestCaseIDChanging(value);
					this.SendPropertyChanging();
					this._LatestCaseID = value;
					this.SendPropertyChanged("LatestCaseID");
					this.OnLatestCaseIDChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.CI_LOG_JOB")]
	public partial class CI_LOG_JOB : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private long _JobID;
		
		private System.Nullable<System.DateTime> _GmtCreate;
		
		private System.Nullable<long> _RunID;
		
		private string _Env;
		
		private string _Browser;
		
		private System.Nullable<int> _Status;
		
		private System.Nullable<System.DateTime> _GmtEnd;
		
		private string _JobName;
		
		private string _slaveIP;
		
		private string _slaveName;
		
		private string _projectName;
		
		private System.Nullable<int> _CaseCount;
		
		private System.Nullable<int> _CaseSuccess;
		
		private System.Nullable<int> _CaseFail;
		
		private System.Nullable<int> _Type;
		
		private System.Nullable<int> _CaseWarn;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnJobIDChanging(long value);
    partial void OnJobIDChanged();
    partial void OnGmtCreateChanging(System.Nullable<System.DateTime> value);
    partial void OnGmtCreateChanged();
    partial void OnRunIDChanging(System.Nullable<long> value);
    partial void OnRunIDChanged();
    partial void OnEnvChanging(string value);
    partial void OnEnvChanged();
    partial void OnBrowserChanging(string value);
    partial void OnBrowserChanged();
    partial void OnStatusChanging(System.Nullable<int> value);
    partial void OnStatusChanged();
    partial void OnGmtEndChanging(System.Nullable<System.DateTime> value);
    partial void OnGmtEndChanged();
    partial void OnJobNameChanging(string value);
    partial void OnJobNameChanged();
    partial void OnslaveIPChanging(string value);
    partial void OnslaveIPChanged();
    partial void OnslaveNameChanging(string value);
    partial void OnslaveNameChanged();
    partial void OnprojectNameChanging(string value);
    partial void OnprojectNameChanged();
    partial void OnCaseCountChanging(System.Nullable<int> value);
    partial void OnCaseCountChanged();
    partial void OnCaseSuccessChanging(System.Nullable<int> value);
    partial void OnCaseSuccessChanged();
    partial void OnCaseFailChanging(System.Nullable<int> value);
    partial void OnCaseFailChanged();
    partial void OnTypeChanging(System.Nullable<int> value);
    partial void OnTypeChanged();
    partial void OnCaseWarnChanging(System.Nullable<int> value);
    partial void OnCaseWarnChanged();
    #endregion
		
		public CI_LOG_JOB()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_JobID", AutoSync=AutoSync.OnInsert, DbType="BigInt NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public long JobID
		{
			get
			{
				return this._JobID;
			}
			set
			{
				if ((this._JobID != value))
				{
					this.OnJobIDChanging(value);
					this.SendPropertyChanging();
					this._JobID = value;
					this.SendPropertyChanged("JobID");
					this.OnJobIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GmtCreate", DbType="DateTime")]
		public System.Nullable<System.DateTime> GmtCreate
		{
			get
			{
				return this._GmtCreate;
			}
			set
			{
				if ((this._GmtCreate != value))
				{
					this.OnGmtCreateChanging(value);
					this.SendPropertyChanging();
					this._GmtCreate = value;
					this.SendPropertyChanged("GmtCreate");
					this.OnGmtCreateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RunID", DbType="BigInt")]
		public System.Nullable<long> RunID
		{
			get
			{
				return this._RunID;
			}
			set
			{
				if ((this._RunID != value))
				{
					this.OnRunIDChanging(value);
					this.SendPropertyChanging();
					this._RunID = value;
					this.SendPropertyChanged("RunID");
					this.OnRunIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Env", DbType="NVarChar(50)")]
		public string Env
		{
			get
			{
				return this._Env;
			}
			set
			{
				if ((this._Env != value))
				{
					this.OnEnvChanging(value);
					this.SendPropertyChanging();
					this._Env = value;
					this.SendPropertyChanged("Env");
					this.OnEnvChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Browser", DbType="NVarChar(50)")]
		public string Browser
		{
			get
			{
				return this._Browser;
			}
			set
			{
				if ((this._Browser != value))
				{
					this.OnBrowserChanging(value);
					this.SendPropertyChanging();
					this._Browser = value;
					this.SendPropertyChanged("Browser");
					this.OnBrowserChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Status", DbType="Int")]
		public System.Nullable<int> Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				if ((this._Status != value))
				{
					this.OnStatusChanging(value);
					this.SendPropertyChanging();
					this._Status = value;
					this.SendPropertyChanged("Status");
					this.OnStatusChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GmtEnd", DbType="DateTime")]
		public System.Nullable<System.DateTime> GmtEnd
		{
			get
			{
				return this._GmtEnd;
			}
			set
			{
				if ((this._GmtEnd != value))
				{
					this.OnGmtEndChanging(value);
					this.SendPropertyChanging();
					this._GmtEnd = value;
					this.SendPropertyChanged("GmtEnd");
					this.OnGmtEndChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_JobName", DbType="NVarChar(200)")]
		public string JobName
		{
			get
			{
				return this._JobName;
			}
			set
			{
				if ((this._JobName != value))
				{
					this.OnJobNameChanging(value);
					this.SendPropertyChanging();
					this._JobName = value;
					this.SendPropertyChanged("JobName");
					this.OnJobNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_slaveIP", DbType="NVarChar(50)")]
		public string slaveIP
		{
			get
			{
				return this._slaveIP;
			}
			set
			{
				if ((this._slaveIP != value))
				{
					this.OnslaveIPChanging(value);
					this.SendPropertyChanging();
					this._slaveIP = value;
					this.SendPropertyChanged("slaveIP");
					this.OnslaveIPChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_slaveName", DbType="NVarChar(100)")]
		public string slaveName
		{
			get
			{
				return this._slaveName;
			}
			set
			{
				if ((this._slaveName != value))
				{
					this.OnslaveNameChanging(value);
					this.SendPropertyChanging();
					this._slaveName = value;
					this.SendPropertyChanged("slaveName");
					this.OnslaveNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_projectName", DbType="NVarChar(100)")]
		public string projectName
		{
			get
			{
				return this._projectName;
			}
			set
			{
				if ((this._projectName != value))
				{
					this.OnprojectNameChanging(value);
					this.SendPropertyChanging();
					this._projectName = value;
					this.SendPropertyChanged("projectName");
					this.OnprojectNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CaseCount", DbType="Int")]
		public System.Nullable<int> CaseCount
		{
			get
			{
				return this._CaseCount;
			}
			set
			{
				if ((this._CaseCount != value))
				{
					this.OnCaseCountChanging(value);
					this.SendPropertyChanging();
					this._CaseCount = value;
					this.SendPropertyChanged("CaseCount");
					this.OnCaseCountChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CaseSuccess", DbType="Int")]
		public System.Nullable<int> CaseSuccess
		{
			get
			{
				return this._CaseSuccess;
			}
			set
			{
				if ((this._CaseSuccess != value))
				{
					this.OnCaseSuccessChanging(value);
					this.SendPropertyChanging();
					this._CaseSuccess = value;
					this.SendPropertyChanged("CaseSuccess");
					this.OnCaseSuccessChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CaseFail", DbType="Int")]
		public System.Nullable<int> CaseFail
		{
			get
			{
				return this._CaseFail;
			}
			set
			{
				if ((this._CaseFail != value))
				{
					this.OnCaseFailChanging(value);
					this.SendPropertyChanging();
					this._CaseFail = value;
					this.SendPropertyChanged("CaseFail");
					this.OnCaseFailChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Type", DbType="Int")]
		public System.Nullable<int> Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				if ((this._Type != value))
				{
					this.OnTypeChanging(value);
					this.SendPropertyChanging();
					this._Type = value;
					this.SendPropertyChanged("Type");
					this.OnTypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CaseWarn", DbType="Int")]
		public System.Nullable<int> CaseWarn
		{
			get
			{
				return this._CaseWarn;
			}
			set
			{
				if ((this._CaseWarn != value))
				{
					this.OnCaseWarnChanging(value);
					this.SendPropertyChanging();
					this._CaseWarn = value;
					this.SendPropertyChanged("CaseWarn");
					this.OnCaseWarnChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
