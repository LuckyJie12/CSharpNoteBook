USE [Book]
GO
/****** Object:  Table [dbo].[admins]    Script Date: 2023/5/12 17:53:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[admins](
	[admin_id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[password] [varchar](100) NOT NULL,
	[name] [varchar](50) NULL,
	[email] [varchar](100) NULL,
	[phone] [varchar](20) NULL,
	[is_superadmin] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[admin_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[book_categories]    Script Date: 2023/5/12 17:53:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[book_categories](
	[book_id] [int] NOT NULL,
	[category_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[book_id] ASC,
	[category_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[books]    Script Date: 2023/5/12 17:53:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[books](
	[book_id] [int] IDENTITY(1,1) NOT NULL,
	[title] [varchar](200) NOT NULL,
	[author] [varchar](100) NOT NULL,
	[publisher] [varchar](100) NULL,
	[publish_year] [int] NULL,
	[total_count] [int] NULL,
	[available_count] [int] NULL,
	[description] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[book_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[borrows]    Script Date: 2023/5/12 17:53:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[borrows](
	[borrow_id] [int] IDENTITY(1,1) NOT NULL,
	[reader_id] [int] NOT NULL,
	[book_id] [int] NOT NULL,
	[borrow_date] [datetime] NOT NULL,
	[return_date] [datetime] NULL,
	[is_returned] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[borrow_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[categories]    Script Date: 2023/5/12 17:53:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[categories](
	[category_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[description] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[category_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[logs]    Script Date: 2023/5/12 17:53:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[logs](
	[log_id] [int] IDENTITY(1,1) NOT NULL,
	[admin_id] [int] NOT NULL,
	[action] [varchar](200) NOT NULL,
	[created_at] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[log_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[readers]    Script Date: 2023/5/12 17:53:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[readers](
	[reader_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[email] [varchar](100) NOT NULL,
	[phone] [varchar](20) NULL,
	[address] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[reader_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[admins] ON 

INSERT [dbo].[admins] ([admin_id], [username], [password], [name], [email], [phone], [is_superadmin]) VALUES (1, N'admin', N'123456', N'超级管理员', N'admin@test.com', N'13999999999', 1)
INSERT [dbo].[admins] ([admin_id], [username], [password], [name], [email], [phone], [is_superadmin]) VALUES (2, N'test_admin', N'123456', N'测试管理员', N'test_admin@test.com', N'13666666666', 0)
SET IDENTITY_INSERT [dbo].[admins] OFF
GO
INSERT [dbo].[book_categories] ([book_id], [category_id]) VALUES (1, 1)
INSERT [dbo].[book_categories] ([book_id], [category_id]) VALUES (1, 5)
INSERT [dbo].[book_categories] ([book_id], [category_id]) VALUES (2, 1)
INSERT [dbo].[book_categories] ([book_id], [category_id]) VALUES (2, 2)
INSERT [dbo].[book_categories] ([book_id], [category_id]) VALUES (2, 5)
INSERT [dbo].[book_categories] ([book_id], [category_id]) VALUES (3, 1)
INSERT [dbo].[book_categories] ([book_id], [category_id]) VALUES (3, 2)
INSERT [dbo].[book_categories] ([book_id], [category_id]) VALUES (3, 5)
INSERT [dbo].[book_categories] ([book_id], [category_id]) VALUES (4, 1)
INSERT [dbo].[book_categories] ([book_id], [category_id]) VALUES (4, 2)
INSERT [dbo].[book_categories] ([book_id], [category_id]) VALUES (4, 4)
INSERT [dbo].[book_categories] ([book_id], [category_id]) VALUES (5, 1)
INSERT [dbo].[book_categories] ([book_id], [category_id]) VALUES (5, 2)
INSERT [dbo].[book_categories] ([book_id], [category_id]) VALUES (5, 3)
INSERT [dbo].[book_categories] ([book_id], [category_id]) VALUES (5, 5)
GO
SET IDENTITY_INSERT [dbo].[books] ON 

INSERT [dbo].[books] ([book_id], [title], [author], [publisher], [publish_year], [total_count], [available_count], [description]) VALUES (1, N'Java编程思想TestTestTest', N'Bruce Eckel', N'机械工业出版社', 2018, 10, 5, N'本书是学习Java编程的必备之书')
INSERT [dbo].[books] ([book_id], [title], [author], [publisher], [publish_year], [total_count], [available_count], [description]) VALUES (2, N'Python编程从入门到实践', N'Eric Matthes', N'人民邮电出版社', 2020, 15, 10, N'本书适合Python编程入门者')
INSERT [dbo].[books] ([book_id], [title], [author], [publisher], [publish_year], [total_count], [available_count], [description]) VALUES (3, N'C++ Primer中文版', N'Stanley B.Lippman/Clauee Lajoie/Josée Lajoie', N'电子工业出版社', 2013, 8, 2, N'本书详细介绍了C++的语法和特点')
INSERT [dbo].[books] ([book_id], [title], [author], [publisher], [publish_year], [total_count], [available_count], [description]) VALUES (4, N'机器学习实战', N'Peter Harrington', N'人民邮电出版社', 2016, 20, 18, N'本书介绍了机器学习的基本原理和实践案例')
INSERT [dbo].[books] ([book_id], [title], [author], [publisher], [publish_year], [total_count], [available_count], [description]) VALUES (5, N'JavaScript高级程序设计', N'Nicholas C. Zakas', N'人民邮电出版社', 2013, 12, 4, N'本书详细讲解了JavaScript的核心概念和高级特性')
INSERT [dbo].[books] ([book_id], [title], [author], [publisher], [publish_year], [total_count], [available_count], [description]) VALUES (7, N'西游记', N'吴承恩', N'人民邮电出版社', 2010, 10, 10, N'四大名著')
INSERT [dbo].[books] ([book_id], [title], [author], [publisher], [publish_year], [total_count], [available_count], [description]) VALUES (8, N'水浒传', N'施耐庵', N'人民出版社', 2010, 10, 10, N'四大名著')
SET IDENTITY_INSERT [dbo].[books] OFF
GO
SET IDENTITY_INSERT [dbo].[borrows] ON 

INSERT [dbo].[borrows] ([borrow_id], [reader_id], [book_id], [borrow_date], [return_date], [is_returned]) VALUES (1, 1, 1, CAST(N'2021-05-01T10:30:00.000' AS DateTime), NULL, 0)
INSERT [dbo].[borrows] ([borrow_id], [reader_id], [book_id], [borrow_date], [return_date], [is_returned]) VALUES (2, 1, 2, CAST(N'2021-05-03T14:20:00.000' AS DateTime), NULL, 0)
INSERT [dbo].[borrows] ([borrow_id], [reader_id], [book_id], [borrow_date], [return_date], [is_returned]) VALUES (3, 2, 2, CAST(N'2021-05-05T09:00:00.000' AS DateTime), NULL, 0)
INSERT [dbo].[borrows] ([borrow_id], [reader_id], [book_id], [borrow_date], [return_date], [is_returned]) VALUES (4, 3, 2, CAST(N'2021-05-10T16:45:00.000' AS DateTime), NULL, 0)
SET IDENTITY_INSERT [dbo].[borrows] OFF
GO
SET IDENTITY_INSERT [dbo].[categories] ON 

INSERT [dbo].[categories] ([category_id], [name], [description]) VALUES (1, N'计算机科学', N'与计算机技术相关的领域')
INSERT [dbo].[categories] ([category_id], [name], [description]) VALUES (2, N'数据科学', N'数据处理、分析和挖掘相关的领域')
INSERT [dbo].[categories] ([category_id], [name], [description]) VALUES (3, N'自然语言处理', N'处理人类语言的相关技术')
INSERT [dbo].[categories] ([category_id], [name], [description]) VALUES (4, N'人工智能', N'模拟人类智能行为的相关技术')
INSERT [dbo].[categories] ([category_id], [name], [description]) VALUES (5, N'编程语言', N'各种编程语言相关的书籍')
SET IDENTITY_INSERT [dbo].[categories] OFF
GO
SET IDENTITY_INSERT [dbo].[logs] ON 

INSERT [dbo].[logs] ([log_id], [admin_id], [action], [created_at]) VALUES (1, 1, N'管理员登录', CAST(N'2021-05-01T09:00:00.000' AS DateTime))
INSERT [dbo].[logs] ([log_id], [admin_id], [action], [created_at]) VALUES (2, 1, N'添加读者', CAST(N'2021-05-01T10:00:00.000' AS DateTime))
INSERT [dbo].[logs] ([log_id], [admin_id], [action], [created_at]) VALUES (3, 2, N'添加书籍', CAST(N'2021-05-02T14:00:00.000' AS DateTime))
INSERT [dbo].[logs] ([log_id], [admin_id], [action], [created_at]) VALUES (4, 1, N'修改借书记录', CAST(N'2021-05-03T11:30:00.000' AS DateTime))
INSERT [dbo].[logs] ([log_id], [admin_id], [action], [created_at]) VALUES (5, 2, N'删除读者', CAST(N'2021-05-04T15:00:00.000' AS DateTime))
INSERT [dbo].[logs] ([log_id], [admin_id], [action], [created_at]) VALUES (6, 1, N'修改管理员密码', CAST(N'2021-05-05T09:30:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[logs] OFF
GO
SET IDENTITY_INSERT [dbo].[readers] ON 

INSERT [dbo].[readers] ([reader_id], [name], [email], [phone], [address]) VALUES (1, N'张三', N'zhangsan@test.com', N'13800001111', N'北京市海淀区中关村大街1号')
INSERT [dbo].[readers] ([reader_id], [name], [email], [phone], [address]) VALUES (2, N'李四', N'lisi@test.com', N'13900002222', N'上海市浦东新区张江镇123号')
INSERT [dbo].[readers] ([reader_id], [name], [email], [phone], [address]) VALUES (3, N'王五', N'wangwu@test.com', N'13600003333', N'广州市天河区天河路789号')
INSERT [dbo].[readers] ([reader_id], [name], [email], [phone], [address]) VALUES (4, N'赵六', N'zhaoliu@test.com', N'13500004444', N'成都市高新区益州大道456号')
SET IDENTITY_INSERT [dbo].[readers] OFF
GO
ALTER TABLE [dbo].[admins] ADD  DEFAULT ('false') FOR [is_superadmin]
GO
ALTER TABLE [dbo].[borrows] ADD  DEFAULT (getdate()) FOR [borrow_date]
GO
ALTER TABLE [dbo].[borrows] ADD  DEFAULT ('false') FOR [is_returned]
GO
ALTER TABLE [dbo].[logs] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[book_categories]  WITH CHECK ADD FOREIGN KEY([book_id])
REFERENCES [dbo].[books] ([book_id])
GO
ALTER TABLE [dbo].[book_categories]  WITH CHECK ADD FOREIGN KEY([category_id])
REFERENCES [dbo].[categories] ([category_id])
GO
ALTER TABLE [dbo].[borrows]  WITH CHECK ADD FOREIGN KEY([book_id])
REFERENCES [dbo].[books] ([book_id])
GO
ALTER TABLE [dbo].[borrows]  WITH CHECK ADD FOREIGN KEY([reader_id])
REFERENCES [dbo].[readers] ([reader_id])
GO
ALTER TABLE [dbo].[logs]  WITH CHECK ADD FOREIGN KEY([admin_id])
REFERENCES [dbo].[admins] ([admin_id])
GO
