USE [BankOfFiji]
GO
/****** Object:  Table [dbo].[Account_Type]    Script Date: 16-Aug-17 12:32:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Account_Type](
	[Acc_Type_ID] [int] IDENTITY(1,1) NOT NULL,
	[Acc_Type_Desc] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Account_Type] PRIMARY KEY CLUSTERED 
(
	[Acc_Type_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Bank_Account]    Script Date: 16-Aug-17 12:32:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bank_Account](
	[Acc_ID] [int] NOT NULL,
	[Cust_ID] [int] NOT NULL,
	[Acc_Type_ID] [int] NOT NULL,
	[Acc_StartDate] [datetime] NOT NULL,
	[Acc_StatusID] [int] NOT NULL,
	[Acc_EndDate] [datetime] NULL,
	[Acc_total_interest] [money] NOT NULL,
	[Acc_Amount] [money] NOT NULL,
	[Acc_TotalAmount] [money] NOT NULL,
	[Acc_NonResTax] [money] NULL,
 CONSTRAINT [PK_Bank_Account] PRIMARY KEY CLUSTERED 
(
	[Acc_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Bank_AccountStatus]    Script Date: 16-Aug-17 12:32:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Bank_AccountStatus](
	[Acc_StatusID] [int] IDENTITY(1,1) NOT NULL,
	[Acc_StatusDesc] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Bank_AccountStatus] PRIMARY KEY CLUSTERED 
(
	[Acc_StatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Bank_Interest]    Script Date: 16-Aug-17 12:32:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bank_Interest](
	[Interest_ID] [int] NOT NULL,
	[Interest_Date] [datetime] NOT NULL,
	[Interest_Amount] [money] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Customer]    Script Date: 16-Aug-17 12:32:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customer](
	[Cust_ID] [int] NOT NULL,
	[Cust_FName] [varchar](50) NOT NULL,
	[Cust_MName] [varchar](50) NOT NULL,
	[Cust_LName] [varchar](50) NOT NULL,
	[Cust_DOB] [varchar](50) NOT NULL,
	[Cust_Postal] [varchar](50) NOT NULL,
	[Cust_Email] [varchar](50) NOT NULL,
	[Cust_Mobile] [varchar](50) NOT NULL,
	[Cust_Gender] [varchar](50) NOT NULL,
	[Cust_Residential] [varchar](50) NOT NULL,
	[Cust_FNPF] [varchar](50) NOT NULL,
	[Cust_Marital] [varchar](50) NOT NULL,
	[Cust_Occupation] [varchar](50) NOT NULL,
	[Cust_Photo] [varchar](50) NOT NULL,
	[Cust_TIN] [varchar](50) NULL,
	[Passport_Num] [int] NULL,
	[Residential_Type] [varchar](50) NULL,
	[CustAcc_StatusID] [int] NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Cust_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Customer_Loans]    Script Date: 16-Aug-17 12:32:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customer_Loans](
	[Loan_ID] [int] NOT NULL,
	[Cust_ID] [int] NOT NULL,
	[Loan_Status] [varchar](50) NOT NULL,
	[Loan_Amount] [varchar](50) NOT NULL,
	[Loan_Time] [datetime] NOT NULL,
	[Loan_ApprovalDate] [datetime] NOT NULL,
	[Loan_Approver] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Loan] PRIMARY KEY CLUSTERED 
(
	[Loan_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Customer_Status]    Script Date: 16-Aug-17 12:32:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customer_Status](
	[CustAcc_StatusID] [int] IDENTITY(1,1) NOT NULL,
	[CustAcc_StatusDesc] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Customer_Status] PRIMARY KEY CLUSTERED 
(
	[CustAcc_StatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Loan_Transaction]    Script Date: 16-Aug-17 12:32:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Loan_Transaction](
	[LTrans_ID] [int] NOT NULL,
	[Cust_ID] [int] NOT NULL,
	[LTrans_Description] [nchar](10) NOT NULL,
	[LTrans_Date] [datetime] NOT NULL,
	[LTrans_Adjustment] [varchar](50) NOT NULL,
	[LTrans_Amount] [money] NOT NULL,
 CONSTRAINT [PK_Loan_Transaction] PRIMARY KEY CLUSTERED 
(
	[LTrans_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Logs]    Script Date: 16-Aug-17 12:32:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Logs](
	[Log_ID] [int] NOT NULL,
	[Log_Date] [datetime] NOT NULL,
	[Log_Description] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED 
(
	[Log_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Role]    Script Date: 16-Aug-17 12:32:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Role](
	[Role_ID] [int] NOT NULL,
	[Role_Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Role_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SubCategory]    Script Date: 16-Aug-17 12:32:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SubCategory](
	[SubCat_ID] [int] NOT NULL,
	[SubCat_Name] [varchar](50) NOT NULL,
	[Role_ID] [int] NOT NULL,
 CONSTRAINT [PK_SubCategory] PRIMARY KEY CLUSTERED 
(
	[SubCat_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Transaction_Status]    Script Date: 16-Aug-17 12:32:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Transaction_Status](
	[Trans_TypeID] [int] IDENTITY(1,1) NOT NULL,
	[Trans_TypeName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Transaction_Status] PRIMARY KEY CLUSTERED 
(
	[Trans_TypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Transaction_Type]    Script Date: 16-Aug-17 12:32:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Transaction_Type](
	[Transac_Type_ID] [int] IDENTITY(1,1) NOT NULL,
	[Transac_Type_Descript] [varchar](50) NOT NULL,
	[Transac_Type] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Transaction_Type] PRIMARY KEY CLUSTERED 
(
	[Transac_Type_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 16-Aug-17 12:32:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Transactions](
	[Trans_ID] [int] IDENTITY(1,1) NOT NULL,
	[Trans_Date] [datetime] NOT NULL,
	[Trans_Amount] [money] NOT NULL,
	[Trans_Adjustment] [varchar](50) NOT NULL,
	[Acc_ID] [int] NOT NULL,
	[Transac_Type_ID] [int] NOT NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[Trans_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User_Type]    Script Date: 16-Aug-17 12:32:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User_Type](
	[UserType_ID] [int] IDENTITY(1,1) NOT NULL,
	[USerType_Desc] [varchar](50) NOT NULL,
 CONSTRAINT [PK_User_Type] PRIMARY KEY CLUSTERED 
(
	[UserType_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserAccount_Status]    Script Date: 16-Aug-17 12:32:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserAccount_Status](
	[UserAcc_StatusID] [int] IDENTITY(1,1) NOT NULL,
	[UserAcc_StatusDesc] [varchar](50) NOT NULL,
 CONSTRAINT [PK_UserAccount_Status] PRIMARY KEY CLUSTERED 
(
	[UserAcc_StatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserAccounts]    Script Date: 16-Aug-17 12:32:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserAccounts](
	[Cust_ID] [int] NOT NULL,
	[Role_ID] [int] NOT NULL,
	[User_FName] [varchar](50) NOT NULL,
	[User_LName] [varchar](50) NOT NULL,
	[User_Email] [varchar](50) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[User_Password] [varchar](50) NOT NULL,
	[UserType_ID] [int] NOT NULL,
	[UserAcc_StatusID] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Cust_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Account_Type] ON 

INSERT [dbo].[Account_Type] ([Acc_Type_ID], [Acc_Type_Desc]) VALUES (1, N'Simple Access')
INSERT [dbo].[Account_Type] ([Acc_Type_ID], [Acc_Type_Desc]) VALUES (2, N'Savings')
SET IDENTITY_INSERT [dbo].[Account_Type] OFF
INSERT [dbo].[Bank_Account] ([Acc_ID], [Cust_ID], [Acc_Type_ID], [Acc_StartDate], [Acc_StatusID], [Acc_EndDate], [Acc_total_interest], [Acc_Amount], [Acc_TotalAmount], [Acc_NonResTax]) VALUES (10001, 1001, 1, CAST(N'2011-11-11 00:00:00.000' AS DateTime), 1, CAST(N'2011-11-11 00:00:00.000' AS DateTime), 1000.0000, 1000.0000, 1000.0000, 1000.0000)
INSERT [dbo].[Bank_Account] ([Acc_ID], [Cust_ID], [Acc_Type_ID], [Acc_StartDate], [Acc_StatusID], [Acc_EndDate], [Acc_total_interest], [Acc_Amount], [Acc_TotalAmount], [Acc_NonResTax]) VALUES (10002, 1002, 1, CAST(N'2011-11-11 00:00:00.000' AS DateTime), 1, CAST(N'2011-11-11 00:00:00.000' AS DateTime), 1000.0000, 1000.0000, 1000.0000, 1000.0000)
SET IDENTITY_INSERT [dbo].[Bank_AccountStatus] ON 

INSERT [dbo].[Bank_AccountStatus] ([Acc_StatusID], [Acc_StatusDesc]) VALUES (1, N'Active')
SET IDENTITY_INSERT [dbo].[Bank_AccountStatus] OFF
INSERT [dbo].[Customer] ([Cust_ID], [Cust_FName], [Cust_MName], [Cust_LName], [Cust_DOB], [Cust_Postal], [Cust_Email], [Cust_Mobile], [Cust_Gender], [Cust_Residential], [Cust_FNPF], [Cust_Marital], [Cust_Occupation], [Cust_Photo], [Cust_TIN], [Passport_Num], [Residential_Type], [CustAcc_StatusID]) VALUES (1001, N'William', N'Kolinio', N'Joe', N'11/11/1994', N'302', N'123@asd', N'123123123', N'Male', N'123 St', N'123123123', N'Single', N'Engineer', N'123123123', N'123123123', 123123123, N'1', 1)
INSERT [dbo].[Customer] ([Cust_ID], [Cust_FName], [Cust_MName], [Cust_LName], [Cust_DOB], [Cust_Postal], [Cust_Email], [Cust_Mobile], [Cust_Gender], [Cust_Residential], [Cust_FNPF], [Cust_Marital], [Cust_Occupation], [Cust_Photo], [Cust_TIN], [Passport_Num], [Residential_Type], [CustAcc_StatusID]) VALUES (1002, N'Ryder', N'Waisavu', N'Joe', N'11/11/1994', N'302', N'123@asd', N'123123123', N'Male', N'123 St', N'123123123', N'Single', N'Engineer', N'123123123', N'123123123', 123123123, N'1', 1)
SET IDENTITY_INSERT [dbo].[Customer_Status] ON 

INSERT [dbo].[Customer_Status] ([CustAcc_StatusID], [CustAcc_StatusDesc]) VALUES (1, N'Active')
SET IDENTITY_INSERT [dbo].[Customer_Status] OFF
INSERT [dbo].[Role] ([Role_ID], [Role_Name]) VALUES (1, N'Individual')
SET IDENTITY_INSERT [dbo].[Transaction_Status] ON 

INSERT [dbo].[Transaction_Status] ([Trans_TypeID], [Trans_TypeName]) VALUES (1, N'Individual')
INSERT [dbo].[Transaction_Status] ([Trans_TypeID], [Trans_TypeName]) VALUES (2, N'Company')
INSERT [dbo].[Transaction_Status] ([Trans_TypeID], [Trans_TypeName]) VALUES (3, N'System')
SET IDENTITY_INSERT [dbo].[Transaction_Status] OFF
SET IDENTITY_INSERT [dbo].[Transaction_Type] ON 

INSERT [dbo].[Transaction_Type] ([Transac_Type_ID], [Transac_Type_Descript], [Transac_Type]) VALUES (1, N'Transfer', N'Customer')
INSERT [dbo].[Transaction_Type] ([Transac_Type_ID], [Transac_Type_Descript], [Transac_Type]) VALUES (2, N'Bill Payments', N'Customer')
INSERT [dbo].[Transaction_Type] ([Transac_Type_ID], [Transac_Type_Descript], [Transac_Type]) VALUES (3, N'Interest', N'System')
INSERT [dbo].[Transaction_Type] ([Transac_Type_ID], [Transac_Type_Descript], [Transac_Type]) VALUES (4, N'Maintinence Fee', N'System')
SET IDENTITY_INSERT [dbo].[Transaction_Type] OFF
SET IDENTITY_INSERT [dbo].[Transactions] ON 

INSERT [dbo].[Transactions] ([Trans_ID], [Trans_Date], [Trans_Amount], [Trans_Adjustment], [Acc_ID], [Transac_Type_ID]) VALUES (5, CAST(N'2017-08-15 21:44:07.170' AS DateTime), 12.0000, N'DR', 10002, 1)
INSERT [dbo].[Transactions] ([Trans_ID], [Trans_Date], [Trans_Amount], [Trans_Adjustment], [Acc_ID], [Transac_Type_ID]) VALUES (6, CAST(N'2017-08-15 21:44:07.170' AS DateTime), 12.0000, N'CR', 10001, 1)
INSERT [dbo].[Transactions] ([Trans_ID], [Trans_Date], [Trans_Amount], [Trans_Adjustment], [Acc_ID], [Transac_Type_ID]) VALUES (7, CAST(N'2017-08-16 00:13:23.453' AS DateTime), 300.0000, N'DR', 10001, 1)
INSERT [dbo].[Transactions] ([Trans_ID], [Trans_Date], [Trans_Amount], [Trans_Adjustment], [Acc_ID], [Transac_Type_ID]) VALUES (8, CAST(N'2017-08-16 00:13:23.453' AS DateTime), 300.0000, N'CR', 10002, 1)
INSERT [dbo].[Transactions] ([Trans_ID], [Trans_Date], [Trans_Amount], [Trans_Adjustment], [Acc_ID], [Transac_Type_ID]) VALUES (9, CAST(N'2017-08-16 00:20:52.977' AS DateTime), 30.0000, N'DR', 10001, 1)
INSERT [dbo].[Transactions] ([Trans_ID], [Trans_Date], [Trans_Amount], [Trans_Adjustment], [Acc_ID], [Transac_Type_ID]) VALUES (10, CAST(N'2017-08-16 00:20:52.977' AS DateTime), 30.0000, N'CR', 10002, 1)
INSERT [dbo].[Transactions] ([Trans_ID], [Trans_Date], [Trans_Amount], [Trans_Adjustment], [Acc_ID], [Transac_Type_ID]) VALUES (11, CAST(N'2017-08-16 00:21:48.653' AS DateTime), 30.0000, N'DR', 10002, 1)
INSERT [dbo].[Transactions] ([Trans_ID], [Trans_Date], [Trans_Amount], [Trans_Adjustment], [Acc_ID], [Transac_Type_ID]) VALUES (12, CAST(N'2017-08-16 00:21:48.653' AS DateTime), 30.0000, N'CR', 10001, 1)
SET IDENTITY_INSERT [dbo].[Transactions] OFF
SET IDENTITY_INSERT [dbo].[User_Type] ON 

INSERT [dbo].[User_Type] ([UserType_ID], [USerType_Desc]) VALUES (1, N'Individual')
SET IDENTITY_INSERT [dbo].[User_Type] OFF
SET IDENTITY_INSERT [dbo].[UserAccount_Status] ON 

INSERT [dbo].[UserAccount_Status] ([UserAcc_StatusID], [UserAcc_StatusDesc]) VALUES (1, N'Active')
SET IDENTITY_INSERT [dbo].[UserAccount_Status] OFF
INSERT [dbo].[UserAccounts] ([Cust_ID], [Role_ID], [User_FName], [User_LName], [User_Email], [UserName], [User_Password], [UserType_ID], [UserAcc_StatusID]) VALUES (1001, 1, N'William', N'Joe', N'123@asd', N'123', N'123', 1, 1)
INSERT [dbo].[UserAccounts] ([Cust_ID], [Role_ID], [User_FName], [User_LName], [User_Email], [UserName], [User_Password], [UserType_ID], [UserAcc_StatusID]) VALUES (1002, 1, N'Ryder', N'Joe', N'123@asd', N'1212', N'1212', 1, 1)
ALTER TABLE [dbo].[Bank_Account]  WITH CHECK ADD  CONSTRAINT [FK_Bank_Account_Account_Type] FOREIGN KEY([Acc_Type_ID])
REFERENCES [dbo].[Account_Type] ([Acc_Type_ID])
GO
ALTER TABLE [dbo].[Bank_Account] CHECK CONSTRAINT [FK_Bank_Account_Account_Type]
GO
ALTER TABLE [dbo].[Bank_Account]  WITH CHECK ADD  CONSTRAINT [FK_Bank_Account_Bank_AccountStatus] FOREIGN KEY([Acc_StatusID])
REFERENCES [dbo].[Bank_AccountStatus] ([Acc_StatusID])
GO
ALTER TABLE [dbo].[Bank_Account] CHECK CONSTRAINT [FK_Bank_Account_Bank_AccountStatus]
GO
ALTER TABLE [dbo].[Bank_Account]  WITH CHECK ADD  CONSTRAINT [FK_Bank_Account_Customer] FOREIGN KEY([Cust_ID])
REFERENCES [dbo].[Customer] ([Cust_ID])
GO
ALTER TABLE [dbo].[Bank_Account] CHECK CONSTRAINT [FK_Bank_Account_Customer]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Customer_Status] FOREIGN KEY([CustAcc_StatusID])
REFERENCES [dbo].[Customer_Status] ([CustAcc_StatusID])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_Customer_Status]
GO
ALTER TABLE [dbo].[Customer_Loans]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Loans_Customer] FOREIGN KEY([Cust_ID])
REFERENCES [dbo].[Customer] ([Cust_ID])
GO
ALTER TABLE [dbo].[Customer_Loans] CHECK CONSTRAINT [FK_Customer_Loans_Customer]
GO
ALTER TABLE [dbo].[Loan_Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Loan_Transaction_Loan_Transaction] FOREIGN KEY([Cust_ID])
REFERENCES [dbo].[Customer] ([Cust_ID])
GO
ALTER TABLE [dbo].[Loan_Transaction] CHECK CONSTRAINT [FK_Loan_Transaction_Loan_Transaction]
GO
ALTER TABLE [dbo].[SubCategory]  WITH CHECK ADD  CONSTRAINT [FK_SubCategory_Role] FOREIGN KEY([Role_ID])
REFERENCES [dbo].[Role] ([Role_ID])
GO
ALTER TABLE [dbo].[SubCategory] CHECK CONSTRAINT [FK_SubCategory_Role]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_Customer] FOREIGN KEY([Acc_ID])
REFERENCES [dbo].[Bank_Account] ([Acc_ID])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_Customer]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_Transaction_Type] FOREIGN KEY([Transac_Type_ID])
REFERENCES [dbo].[Transaction_Type] ([Transac_Type_ID])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_Transaction_Type]
GO
ALTER TABLE [dbo].[UserAccounts]  WITH CHECK ADD  CONSTRAINT [FK_UserAccounts_Role1] FOREIGN KEY([Role_ID])
REFERENCES [dbo].[Role] ([Role_ID])
GO
ALTER TABLE [dbo].[UserAccounts] CHECK CONSTRAINT [FK_UserAccounts_Role1]
GO
ALTER TABLE [dbo].[UserAccounts]  WITH CHECK ADD  CONSTRAINT [FK_UserAccounts_Role2] FOREIGN KEY([Cust_ID])
REFERENCES [dbo].[Customer] ([Cust_ID])
GO
ALTER TABLE [dbo].[UserAccounts] CHECK CONSTRAINT [FK_UserAccounts_Role2]
GO
ALTER TABLE [dbo].[UserAccounts]  WITH CHECK ADD  CONSTRAINT [FK_UserAccounts_User_Type] FOREIGN KEY([UserType_ID])
REFERENCES [dbo].[User_Type] ([UserType_ID])
GO
ALTER TABLE [dbo].[UserAccounts] CHECK CONSTRAINT [FK_UserAccounts_User_Type]
GO
ALTER TABLE [dbo].[UserAccounts]  WITH CHECK ADD  CONSTRAINT [FK_UserAccounts_UserAccounts] FOREIGN KEY([UserAcc_StatusID])
REFERENCES [dbo].[UserAccount_Status] ([UserAcc_StatusID])
GO
ALTER TABLE [dbo].[UserAccounts] CHECK CONSTRAINT [FK_UserAccounts_UserAccounts]
GO
