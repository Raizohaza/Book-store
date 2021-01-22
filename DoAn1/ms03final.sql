USE [MyStore]
GO
/****** Object:  User [admin]    Script Date: 22/1/2021 9:21:21 PM ******/
CREATE USER [admin] FOR LOGIN [admin] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [sale1]    Script Date: 22/1/2021 9:21:21 PM ******/
CREATE USER [sale1] FOR LOGIN [sale1] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [sale2]    Script Date: 22/1/2021 9:21:21 PM ******/
CREATE USER [sale2] FOR LOGIN [sale2] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [admin]
GO
ALTER ROLE [db_owner] ADD MEMBER [sale1]
GO
ALTER ROLE [db_owner] ADD MEMBER [sale2]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 22/1/2021 9:21:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[username] [nvarchar](50) NOT NULL,
	[rolename] [nvarchar](50) NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 22/1/2021 9:21:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [ntext] NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 22/1/2021 9:21:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Customer_Name] [nvarchar](50) NULL,
	[Tel] [nvarchar](50) NOT NULL,
	[Address] [ntext] NULL,
	[Email] [ntext] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Tel] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 22/1/2021 9:21:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CatId] [int] NULL,
	[Name] [ntext] NULL,
	[Price] [decimal](19, 4) NULL,
	[Quantity] [int] NULL,
	[Description] [ntext] NULL,
	[Image] [ntext] NULL,
	[Author] [nvarchar](50) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product_Images]    Script Date: 22/1/2021 9:21:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_Images](
	[id] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Product_Images_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Purchase]    Script Date: 22/1/2021 9:21:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Purchase](
	[Purchase_ID] [int] IDENTITY(1,1) NOT NULL,
	[Total] [decimal](19, 4) NULL,
	[Created_At] [datetime] NULL,
	[Status] [int] NULL,
	[Customer_Tel] [nvarchar](50) NULL,
 CONSTRAINT [PK_Purchase] PRIMARY KEY CLUSTERED 
(
	[Purchase_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseDetail]    Script Date: 22/1/2021 9:21:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseDetail](
	[PurchaseDetail_ID] [int] IDENTITY(1,1) NOT NULL,
	[Purchase_ID] [int] NOT NULL,
	[Product_ID] [int] NOT NULL,
	[Price] [decimal](19, 4) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Total] [int] NOT NULL,
 CONSTRAINT [PK_PurchaseDetail] PRIMARY KEY CLUSTERED 
(
	[PurchaseDetail_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseStatusEnum]    Script Date: 22/1/2021 9:21:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseStatusEnum](
	[EnumKey] [nvarchar](50) NOT NULL,
	[Value] [int] NOT NULL,
	[Description] [ntext] NULL,
 CONSTRAINT [PK_PurchaseStatusEnum] PRIMARY KEY CLUSTERED 
(
	[Value] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Category] ON 
GO
INSERT [dbo].[Category] ([Id], [Name]) VALUES (1, N'Sách văn học')
GO
INSERT [dbo].[Category] ([Id], [Name]) VALUES (2, N'Manga')
GO
INSERT [dbo].[Category] ([Id], [Name]) VALUES (3, N'Sách kinh tế')
GO
INSERT [dbo].[Category] ([Id], [Name]) VALUES (4, N'Sách tâm lý')
GO
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
INSERT [dbo].[Customer] ([Customer_Name], [Tel], [Address], [Email]) VALUES (N'', N'', NULL, NULL)
GO
INSERT [dbo].[Customer] ([Customer_Name], [Tel], [Address], [Email]) VALUES (N'er', N'123', N'', N'')
GO
INSERT [dbo].[Customer] ([Customer_Name], [Tel], [Address], [Email]) VALUES (N'Trần D', N'123123', NULL, NULL)
GO
INSERT [dbo].[Customer] ([Customer_Name], [Tel], [Address], [Email]) VALUES (N'abc', N'123456', NULL, NULL)
GO
INSERT [dbo].[Customer] ([Customer_Name], [Tel], [Address], [Email]) VALUES (N'Phạm M', N'123456123', NULL, NULL)
GO
INSERT [dbo].[Customer] ([Customer_Name], [Tel], [Address], [Email]) VALUES (N'Nguyễn Văn B', N'123456787', NULL, NULL)
GO
INSERT [dbo].[Customer] ([Customer_Name], [Tel], [Address], [Email]) VALUES (N'Phan Thị C', N'1234567875', NULL, NULL)
GO
INSERT [dbo].[Customer] ([Customer_Name], [Tel], [Address], [Email]) VALUES (N'Nguyễn Văn A', N'123456789', NULL, NULL)
GO
INSERT [dbo].[Customer] ([Customer_Name], [Tel], [Address], [Email]) VALUES (N'1', N'123456987', NULL, NULL)
GO
INSERT [dbo].[Customer] ([Customer_Name], [Tel], [Address], [Email]) VALUES (N'zz', N'123521', NULL, NULL)
GO
INSERT [dbo].[Customer] ([Customer_Name], [Tel], [Address], [Email]) VALUES (N'Hai Lua', N'13547895', NULL, NULL)
GO
INSERT [dbo].[Customer] ([Customer_Name], [Tel], [Address], [Email]) VALUES (N'Hai Lua', N'135478951', NULL, NULL)
GO
INSERT [dbo].[Customer] ([Customer_Name], [Tel], [Address], [Email]) VALUES (N'Lâm Lu', N'135478952', NULL, NULL)
GO
INSERT [dbo].[Customer] ([Customer_Name], [Tel], [Address], [Email]) VALUES (N'Nguyễn D', N'789546123', N'', N'')
GO
INSERT [dbo].[Customer] ([Customer_Name], [Tel], [Address], [Email]) VALUES (N'1', N'852123', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Product] ON 
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (1, 1, N'Dấu Chân Trên Cát ', CAST(119000.0000 AS Decimal(19, 4)), 10, N'Ngày nay, người ta biết đến triều đại các vua chúa Ai Cập thời cổ qua sách vở của người Hy Lạp. Sở dĩ các sử gia Hy Lạp biết được các chi tiết này vì họ đã học hỏi từ người Ai Cập bị đày biệt xứ tên là Sinuhe. Đây là một nhân vật lạ lùng, đã có công mang văn minh Ai Cập truyền vào Hy Lạp khi quốc gia này còn ở tình trạng kém mở mang so với Ai Cập lúc đó.', N'S01.jpg', N' Nguyên Phong')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (2, 1, N'Đảo Mộng Mơ ', CAST(39000.0000 AS Decimal(19, 4)), 12, N'Câu chuyện bắt đầu từ một đống cát, và được diễn ra theo nhân vật tôi - cu Tin. Có một hòn đảo hoang, trên đảo có Chúa đảo, phu nhân Chúa đảo, và một chàng Thứ... Bảy. Hàng ngày vợ chồng Chúa đảo và Thứ Bảy vẫn phải đi học, nhưng sau giờ học là một thế giới khác, của đảo, của biển có cá mập, và rừng có thú dữ. Hấp dẫn, đầy quyến rũ, có cãi vã, có cai trị, có yêu thương, có ẩu đả, và cả...những nụ hôn!', N'S02.jpg', N'Nguyễn Nhật Ánh')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (3, 1, N'Quân Khu Nam Đồng', CAST(102000.0000 AS Decimal(19, 4)), 10, N'Khu tập thể Nam Đồng là khu gia binh lớn nhất thủ đô Hà Nội, được hình thành cách đây hơn 50 năm. Là nơi ở của hơn 500 gia đình cán bộ quân đội trung, cao cấp, hơn 70 vị tướng đã từng sinh sống và trưởng thành từ Khu tập thể Nam Đồng, nhiều gia đình có cả hai thế hệ “tướng cha” và “tướng con”. Đây là một khu gia binh điển hình, một đại gia đình quân nhân thu nhỏ thời chiến và hậu chiến.', N'S03.jpg', N'Bình ca')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (4, 1, N'Mắt Biếc ', CAST(91300.0000 AS Decimal(19, 4)), 5, N'là một tác phẩm được nhiều người bình chọn là hay nhất của nhà văn Nguyễn Nhật Ánh. Tác phẩm này cũng đã được dịch giả Kato Sakae dịch sang tiếng Nhật để giới thiệu với độc giả Nhật Bản.
“Tôi gửi tình yêu cho mùa hè, nhưng mùa hè không giữ nổi. Mùa hè chỉ biết ra hoa, phượng đỏ sân trường và tiếng ve nỉ non trong lá. Mùa hè ngây ngô, giống như tôi vậy. Nó chẳng làm được những điều tôi ký thác. Nó để Hà Lan đốt tôi, đốt rụi. Trái tim tôi cháy thành tro, rơi vãi trên đường về.”
… Bởi sự trong sáng của một tình cảm, bởi cái kết thúc buồn, rất buồn khi xuyên suốt câu chuyện vẫn là những điều vui, buồn lẫn lộn… ', N'S04.jpg', N'Nguyễn Nhật Ánh')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (5, 1, N'Vừa Nhắm Mắt Vừa Mở Cửa Sổ ', CAST(47000.0000 AS Decimal(19, 4)), 0, N'Tập sách hay, dễ thương, và còn nhiều mỹ từ khác nữa xứng đáng được dành cho nó. Hãy tìm đọc nội dung thay vì đọc trước phần giới thiệu sách này viết gì…. Như thế, bạn sẽ càng thích thú hơn với “Vừa nhắm mắt, vừa mở cửa sổ”.', N'S05.jpg', N'Nguyễn Ngọc Thuần')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (6, 1, N'Tôi Thấy Hoa Vàng Trên Cỏ Xanh', CAST(100000.0000 AS Decimal(19, 4)), 2, N'Những câu chuyện nhỏ xảy ra ở một ngôi làng nhỏ: chuyện người, chuyện cóc, chuyện ma, chuyện công chúa và hoàng tử , rồi chuyện đói ăn, cháy nhà, lụt lội,... Bối cảnh là trường học, nhà trong xóm, bãi tha ma. Dẫn chuyện là cậu bé 15 tuổi tên Thiều. Thiều có chú ruột là chú Đàn, có bạn thân là cô bé Mận. Nhưng nhân vật đáng yêu nhất lại là Tường, em trai Thiều, một cậu bé học không giỏi. Thiều, Tường và những đứa trẻ sống trong cùng một làng, học cùng một trường, có biết bao chuyện chung. Chúng nô đùa, cãi cọ rồi yêu thương nhau, cùng lớn lên theo năm tháng, trải qua bao sự kiện biến cố của cuộc đời.
Tác giả vẫn giữ cách kể chuyện bằng chính giọng trong sáng hồn nhiên của trẻ con. 81 chương ngắn là 81 câu chuyện hấp dẫn với nhiều chi tiết thú vị, cảm động, có những tình tiết bất ngờ, từ đó lộ rõ tính cách người. Cuốn sách, vì thế, có sức ám ảnh.', N'S06.jpg', N'Nguyễn Nhật Ánh')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (7, 1, N'Bước Chậm Lại Giữa Thế Gian Vội Vã ', CAST(68000.0000 AS Decimal(19, 4)), 1, N'Chen vai thíach cánh để có một chỗ bám trên xe buýt giờ đi làm, nhích từng xentimét bánh xe trên đường lúc tan sở, quay cuồng với thi cử và tiến độ công việc, lu bù vướng mắc trong những mối quan hệ cả thân lẫn sơ… bạn có luôn cảm thấy thế gian xung quanh mình đang xoay chuyển quá vội vàng?
Nếu có thể, hãy tạm dừng một bước.
Để tự hỏi, là do thế gian này vội vàng hay do chính tâm trí bạn đang quá bận rộn? Để cầm cuốn sách nhỏ dung dị mà lắng đọng này lên, chậm rãi lật giở từng trang, thong thả khám phá những điều mà chỉ khi bước chậm lại mới có thể thấu rõ: về các mối quan hệ, về chính bản thân mình, về những trăn trở trước cuộc đời và nhân thế, về bao điều lý trí rất hiểu nhưng trái tim chưa cách nào nghe theo…', N'S07.jpg', N'Hae Min')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (8, 1, N'Anne Tóc Đỏ Dưới Chái Nhà Xanh', CAST(76000.0000 AS Decimal(19, 4)), 10, N'Khi Matthew và Marilla Cuthbert ngỏ lời với một trại trẻ  mồ côi để xin một cậu bé về đỡ  đần họ tại nông trang Green Gables, cả hai sửng sốt khi thấy bước ra khỏi tàu là một cô bé lắm mồm, tên là Anne.
Anne, tóc đỏ, hay gây gổ và lãng mạn vô phương cứu chữa - cô đã xáo trộn cả Green Gable lẫn ngôi làng quanh đó. Nhưng sự hóm hỉnh và thiện tính trong cô lại khiến cô được yêu mến không ngớt, không chỉ với cộng đồng tưởng tượng của Đảo Hoàng tử Edward, mà còn với bao thế hệ độc giả khắp bên kia bờ Đại Tây Dương, suốt một thế kỷ nay, kể từ ngày cuốn Anne Tóc đỏ đầu tiên ra đời năm 1908.', N'S08.jpg', N'Lucy Maud Montgomery')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (9, 1, N'Tôi Nói Gì Khi Nói Về Chạy Bộ', CAST(52000.0000 AS Decimal(19, 4)), 1, N'Liệu có gì chung giữa viết văn và chạy bộ? Có, Haruki Murakami trả lời, giản dị, tự tin, bằng hành động viết và bằng cuộc sống của chính mình. Nhà văn Nhật Bản nổi tiếng, tác giả Rừng Na Uy, Biên niên ký chim vặn dây cót, Kafka bên bờ biển… bên cạnh khả năng viết xuất chúng còn là một người chạy bộ cừ khôi. Trong cuốn sách nhỏ mà thú vị này, bằng giọng văn lôi cuốn, thoải mái nhưng đầy sức mạnh, Murakami kể về quá trình tham gia môn chạy bộ cùng những suy tưởng của ông về ý nghĩa của chạy bộ, và rộng hơn nữa, của vận động cơ thể - sự tuân theo một kỷ luật khắt khe về phương diện thể xác - đối với hoạt động chuyên môn của ông trong tư cách nhà văn. Những nghiền ngẫm của Murakami về sự tương đồng giữa chạy - hành vi thể chất - và viết văn - hành vi tinh thần - thực sự quý báu với những người đọc quan tâm đến văn chương và bản chất của văn chương, đặc biệt người viết trẻ.', N'S09.jpg', N'Haruki Murakami')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (10, 1, N'Cho Tôi Xin Một Vé Đi Tuổi Thơ', CAST(64000.0000 AS Decimal(19, 4)), 1, N'Trong Cho tôi xin một vé đi tuổi thơ, nhà văn Nguyễn Nhật Ánh mời người đọc lên chuyến tàu quay ngược trở lại thăm tuổi thơ và tình bạn dễ thương của 4 bạn nhỏ. Những trò chơi dễ thương thời bé, tính cách thật thà, thẳng thắn một cách thông minh và dại dột, những ước mơ tự do trong lòng… khiến cuốn sách có thể làm các bậc phụ huynh lo lắng rồi thở phào. Không chỉ thích hợp với người đọc trẻ, cuốn sách còn có thể hấp dẫn và thực sự có ích cho người lớn trong quan hệ với con mình.', N'S10.jpg', N'Nguyễn Nhật Ánh')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (11, 2, N'Naruto', CAST(101000.0000 AS Decimal(19, 4)), 10, N'là manga Nhật Bản bằng văn bản và minh họa bởi tác giả Kishimoto Masashi. Nhân vật chính là Uzumaki Naruto, một thiếu niên ồn ào, hiếu động, một ninja luôn muốn tìm cách khẳng định mình để được mọi người công nhận, rất muốn trở thành Hokage (Người đứng đầu làng lá Konoha) - người lãnh đạo ninja cả làng, được tất cả mọi người kính trọng.', N'M01.jpg', N'Kishimoto Masashi')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (12, 2, N'Truyện tranh Doremon', CAST(137750.0000 AS Decimal(19, 4)), 10, N'Bộ truyện được xem là huyền thoại của Nhật Bản. Nói về cuộc sống của một chú nhóc tên Nobita, tính tình hậu đậu và chú mèo máy Doremon đến từ thế kỉ 22 cùng những người bạn Xuka, Chaien, Xeko; để từ đó gây ra bao tiếng cười và rút ra những bài học ý nghĩa cho người xem !!', N'M02.png', N'Fujiko F. Fujio')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (13, 2, N'One Piece - Đảo Hải Tặc', CAST(201600.0000 AS Decimal(19, 4)), 12, N'One Piece là câu truyện kể về Luffy và các thuyền viên của mình. Khi còn nhỏ, Luffy ước mơ trở thành Vua Hải Tặc. Cuộc sống của cậu bé thay đổi khi cậu vô tình có được sức mạnh có thể co dãn như cao su, nhưng đổi lại, cậu không bao giờ có thể bơi được nữa. Giờ đây, Luffy cùng những người bạn hải tặc của mình ra khơi tìm kiếm kho báu One Piece, kho báu vĩ đại nhất trên thế giới.', N'M03.jpg', N' Eiichiro Oda')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (14, 2, N'Thám Tử Lừng Danh Conan', CAST(350000.0000 AS Decimal(19, 4)), 15, N'Mở đầu câu truyện, cậu học sinh trung học 17 tuổi Shinichi Kudo bị biến thành cậu bé Conan Edogawa. Shinichi trong phần đầu của Thám tử lừng danh Conan được miêu tả là một thám tử học đường xuất sắc. Trong một lần đi chơi công viên với Ran Mori, cậu tình cờ chứng kiến vụ một án giết người, Cậu đã giúp cảnh sát làm sáng tỏ vụ án. Trên đường về nhà, cậu vô tình phát hiện một vụ làm ăn mờ ám của những người đàn ông mặc đồ đen. Chúng phát hiện ra cậu và đã cho cậu uống một thứ thuốc độc chưa qua thử nghiệm là Apotoxin-4869 (APTX 4869) với mục đích thủ tiêu cậu. Tuy nhiên chất độc đã không giết chết Kudo mà chỉ khiến cậu bị teo nhỏ.', N'M04.jpg', N'Gosho Aoyama')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (15, 2, N'Siêu Quậy Teppi', CAST(200000.0000 AS Decimal(19, 4)), 12, N'Cốt truyện xoay quanh Teppi, một cậu bé mà tay chân hoạt động không bao giờ ngừng nghỉ. Teppi theo cha lên núi tìm kho báu và sống suốt thời thơ ấu trong thiên nhiên hoang dã. Vì thế mà khi trở về thành phố, cậu hơi lập dị và gây rắc rối thường xuyên từ khi nhập học do quen với cuộc sống tự do. Tuy nhiên, do sống trong nơi hoang dã từ nhỏ nên Teppi có khả năng hơn hẳn người bình thường trong thể thao, cậu chú ý đến môn kendo và bắt đầu luyện tập thường xuyên để trở thành kiếm thủ giỏi nhất.', N'M05.jpg', N'Tetsuya Chiba')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (16, 2, N'Toriko', CAST(180000.0000 AS Decimal(19, 4)), 10, N'là loạt manga được thực hiện bởi Shimabukuro Mitsutoshi. Tác phẩm đã đăng trên tạp chí Weekly Shōnen Jump từ ngày 19 tháng 5 năm 2008. Cốt truyện xoay quanh Toriko một thợ săn ẩm thực với sức mạnh phi thường cùng kiến thức rất rộng về thiên nhiên đang tìm kiếm những món ăn ngon nhất thế giới để thêm vào danh sách các món ăn trong bữa ăn để đời của mình.', N'M06.jpg', N'Shimabukuro Mitsutoshi')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (17, 2, N'7 Viên Ngọc Rồng', CAST(74000.0000 AS Decimal(19, 4)), 5, N'Một cậu bé có đuôi khỉ tên là Goku được tìm thấy bởi một ông lão sống một mình trong rừng, ông lão xem đứa bé như là cháu của mình. Một ngày nọ Goku tình cờ gặp một cô gái tên là Bulma trên đường đi bắt cá về, Goku và Bulma đã cùng nhau truy tìm bảy viên ngọc rồng. Bảy viên ngọc rồng chứa đựng một bí mật có thể triệu hồi một con rồng và ban điều ước cho ai sở hữu chúng. Trên cuộc hành trình dài đi tìm bảy viên ngọc rồng, họ gặp những người bạn và những đấu sĩ huyền thoại cũng như nhiều ác quỷ. Họ trải qua những khó khăn và học hỏi các chiêu thức võ thuật đặc biệt để tham gia thi đấu trong đại hội võ thuật thế giới được tổ chức hằng năm. Ngoài các sự kiện đại hội võ thuật, Guku và các bạn còn phải đối phó với các thế lực độc ác như Đại vương Pilaf, Quân đoàn khăn đỏ của Độc nhãn tướng quân, Đại ma vương Pocollo và những đứa con của hắn. Rồi họ đi đến Namek, gặp rồng thần của Namek; chạm trán rô bô sát thủ, Fide và sau đó là Xên bọ hung, Ma bư…', N'M07.jpg', N'Akira Toriyama')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (18, 2, N'Dragon Quest - Dấu Ấn Rồng Thiêng', CAST(500000.0000 AS Decimal(19, 4)), 7, N' Nhân vật chính trong truyện là Dai no Daiboken - Dũng sĩ rồng Dai, từ một cậu bé hồn nhiên đã nhanh chóng trở thành Dũng sĩ diệt trừ ma quỷ, niềm hy vọng lớn nhất của thế giới loài người. Sát cánh cùng cậu là những học trò của Evan: Pop, Marm, Leona, Huynken.', N'M08.jpg', N'Sanjo Riku')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (19, 2, N'Yu-Gi-Oh!', CAST(135000.0000 AS Decimal(19, 4)), 9, N'Yu-Gi-Oh, Vua Trò Chơi! là một manga tiếng Nhật được sáng tác bởi Kazuki Takahashi. Nó đã được chuyển thể thành nhiều anime, video game và trò chơi trading card game. Phần lớn bộ truyện tập trung vào trò chơi hư cấu gọi là Duel Monster (tên ban đầu gọi là Phép thuật và phù thuỷ), trong đó các nhân vật sử dụng các lá bài để "đấu" (duel) bằng các "quái thú" giả lập. Yu-Gi-Oh! Trading Card Game là một trò chơi ngoài đời thực dựa trên Duel Monster.', N'M09.jpg', N'Kazuki Takahashi')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (20, 2, N'Nanatsu No Taizai - Thất Hình Đại Tội', CAST(625000.0000 AS Decimal(19, 4)), 20, N'“Thất đại ác nhân”, một nhóm chiến binh có tham vọng lật đổ vương quốc Britannia, được cho là đã bị tiêu diệt bởi các “hiệp sĩ thánh chiến” mặc dù vẫn còn 1 số người cho rằng họ vẫn còn sống. 10 năm sau, Các hiệp sĩ thánh chiến đã làm 1 cuộc đảo chính và khống chế đức vua, họ trở thành người cai trị độc tài mới của vương quốc. Elizabeth, con gái duy nhất của nhà vua, đã lên đường tìm “thất đại ác nhân” để nhờ họ tái chiếm lại vương quốc. Công chúa có thành công trong việc tìm kiếm “thất đại ác nhân”, các “hiệp sĩ thánh chiến” sẽ làm gì để ngăn chăn cô? xem các chap truyện sau sẽ rõ.', N'M10.jpg', N'Suzuki Nakaba')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (21, 3, N'Trải Nghiệm Khách Hàng Xuất Sắc', CAST(200000.0000 AS Decimal(19, 4)), 1, N'Ngoài kinh nghiệm thực tế và kiến thức về quản trị nói chung và quản trị trải nghiệm khách hàng nói riêng, trong quá trình hoàn thành cuốn sách này, tác giả Nguyễn Dương đã thực hiện những đợt đi nghiên cứu nhiều công ty, bao gồm những chuyến đi, những cuộc nói chuyện, phỏng vấn, trao đổi, tìm hiểu và cập nhật cách mà nhiều công ty đã thực hiện để có thể cung cấp những trải nghiệm khách hàng tuyệt vời và tăng trưởng vượt bậc.', N'K01.jpg', N'Nguyễn Dương')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (22, 3, N'Người Bán Hàng Vĩ Đại Nhất Thế Giới', CAST(118400.0000 AS Decimal(19, 4)), 1, N'"Người Bán Hàng Vĩ Đại Nhất Thế Giới" kể về câu chuyện của Hafid, một cậu bé chăn lạc đà nghèo, ở Jerusalem thời cổ đại.  Xuất thân là một cậu nhóc trông lạc đà cho đoàn buôn của Pathros, nhưng với quyết tâm đổi đời, muốn cải thiện vị trí của mình trong cuộc sống, Hafid luôn nuôi ước mơ được trở thành người bán hàng. Cậu tin chỉ như thế mình mới có cơ hội trở nên giàu có và thành công.', N'K02.jpg', N'Og Mandino')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (23, 3, N'Đừng Bán Khi Chưa Được Giá', CAST(93000.0000 AS Decimal(19, 4)), 2, N'Bạn có muốn xây dựng một doanh nghiệp phát triển vượt trội mà vẫn được tự do tự tại sống theo ý mình? Bạn có muốn tạo dựng một công ty mà bạn có thể sang nhượng lại được để có thể nghỉ ngơi thoải mái hoặc theo đuổi các ý tưởng lớn tiếp theo?
Trong Đừng bán khi chưa được giá, hai ý tưởng này không hề mâu thuẫn với nhau. Nếu bạn cho rằng chỉ cần làm việc cật lực là có thể bán được công ty, thì bạn đã lầm. Ngược lại, để bán được công ty với giá cao nhất, bạn phải xây dựng doanh nghiệp của mình phát triển thịnh vượng mà không cần tới bạn, để chủ sở hữu tiếp theo có thể tiếp tục phát triển doanh nghiệp và thu lợi nhuận từ thành quả bạn đã tạo dựng khi bạn rời đi.', N'K03.jpg', N'John Warrillow')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (24, 3, N'Nghệ Thuật Bán Hàng Bậc Cao', CAST(135000.0000 AS Decimal(19, 4)), 1, N'Mục tiêu của một thương vụ là đảm bảo khách hàng nhận được giá trị tương xứng, nhưng nếu bạn mang đến cho khách hàng những giá trị còn cao hơn giá trị mà lẽ ra họ sẽ nhận được thì không những bạn đã có một thương vụ thành công mà bạn còn có thêm một khách hàng sẵn lòng giúp bạn có thêm nhiều khách hàng khác nữa.', N'K04.jpg', N'Zig Ziglar')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (25, 3, N'Hiệu Ứng Chim Mồi ', CAST(80000.0000 AS Decimal(19, 4)), 2, N'Trong Tiếu ngạo giang hồ, có đoạn Lệnh Hồ Xung gặp Phong Thanh Dương và được chỉ điểm Độc Cô Cửu Kiếm. Trong quá trình này, khi nhắc đến các hậu bối đương thời trong Ngũ nhạc kiếm phái, Phong Thanh Dương nhận xét:
Chúng cho rằng cứ học những chiêu kiếm tinh thục của sư phụ truyền cho là trở thành cao thủ. Hừ! Học thuộc lòng 300 bài thơ Ðường thì kẻ không biết làm thơ cũng ngâm được thật. Nhưng chỉ thuộc thơ người ta, thì mình có làm thơ cũng chẳng ra hồn. Nếu tự mình không có óc sáng tác liệu có thành đại thi gia được không?', N'K05.jpg', N'Hạo Nhiên, Quốc Khánh')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (26, 3, N'Marketing Du Kích', CAST(75000.0000 AS Decimal(19, 4)), 1, N'Marketing du kích là một phương thức marketing khác biệt được sử dụng đề thực hiện các hoạt động xúc tiến thương mại với ngân sách hạn chế. Marketing du kích có mục đích chủ yếu là gây chú ý cho khách hàng, làm họ bất ngờ, từ đó giành về lợi ích trong doanh thu và thương hiệu.
Mục tiêu lớn nhất của marketing chính là tạo ra lợi nhuận. Tiếp thị du kích là một cách thức giúp các chủ doanh nghiệp có thể chỉ phải tiêu tốn ít tiền hơn, song nhận lại nhiều hơn, đạt được lợi nhuận đáng kể. ', N'K06.jpg', N'Jay Levinson, Jeannie Levinson')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (27, 3, N'Cẩm Nang Start Up - Không Đường Và Hạ Gục Rồng', CAST(42000.0000 AS Decimal(19, 4)), 1, N'“Cẩm nang Startup” cung cấp đến bạn những bí quyết rất dễ bị bỏ qua trong bán hàng, kinh doanh và marketing. Đây là những bài học thực sự cần thiết để bắt đầu việc kinh doanh một doanh nghiệp nhỏ cũng như tìm ra giải pháp tốt nhất cho mọi khó khăn khi khởi nghiệp', N'K07.jpg', N'Ken Horn')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (28, 3, N'Những Đòn Tâm Lý Trong Bán Hàng', CAST(93000.0000 AS Decimal(19, 4)), 1, N'Những Đòn Tâm Lý Trong Bán Hàng (Tái Bản 2018)

Những đòn tâm lý trong bán hàng chính là nghệ thuật bán hàng mà Brian Tracy, một doanh nhân thành đạt, đúc rút từ chính câu chuyện khởi nghiệp của cuộc đời ông.
Trong bán hàng, bạn chỉ cần giỏi hơn và khác biệt chút đỉnh trong mỗi công đoạn để tích lũy và dần tạo nên một khác biệt lớn về thu nhập.
Nếu bạn là người bán hàng mà lại sợ bị từ chối, bạn đã chọn sai cách kiếm sống.', N'K08.jpg', N'Brian Tracy')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (29, 3, N'Bán Hàng Cho Người Giàu', CAST(238400.0000 AS Decimal(19, 4)), 2, N'Bán hàng cho người giàu - không giới hạn ,không khoan nhượng (cẩm nang trở nên cực kỳ giàu có- cẩm nang bách thắng )
Bán hàng cho những người có thể cho trả
SỰ THẬT ĐÁNG SỢ : Tầng lớp trung lưu- và những khoản chi tiêu của họ- đang biến mất với tốc độ cực kỳ nhanh chóng trên diện rộng. Khách hàng giờ đây mua hàng ít hơn và chi tiêu cho một số ít, các lĩnh vực tiêu dùng.', N'K09.jpg', N'Dan S Kennedy')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (30, 3, N'Tiếp Thị 4.0 - Dịch Chuyển Từ Truyền Thống Sang Công Nghệ Số', CAST(82000.0000 AS Decimal(19, 4)), 1, N'Quyển cẩm nang vô cùng cần thiết cho những người làm tiếp thị trong thời đại số. Được viết bởi cha đẻ ngành tiếp thị hiện đại, cùng hai đồng tác giả là lãnh đạo của công ty MarkPlus, quyển sách sẽ giúp bạn lèo lái thế giới không ngừng kết nối và khách hàng không ngừng thay đổi để có được nhiều khách hàng hơn, xây dựng thương hiệu hiệu quả hơn, và cuối cùng kinh doanh thành công hơn. Ngày nay khách hàng không có nhiều thời gian và sự chú ý dành cho thương hiệu của bạn – và họ còn bị bao quanh bởi vô số các chọn lựa. Bạn phải tách khỏi đám đông, phải nổi trội, để gây sự chú ý và truyền đạt thông điệp mà khách hàng muốn nghe. “Tiếp thị 4.0” tận dụng tâm lý thay đổi của khách hàng để tiếp cận nhiều khách hàng hơn và khiến họ gắn bó với thương hiệu hơn bao giờ hết.', N'K10.jpg', N'Philip Kotler, Hermawan Kartajaya, Iwan Setiawan')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (31, 4, N'Đắc Nhân Tâm', CAST(59300.0000 AS Decimal(19, 4)), 1, N'Đắc nhân tâm của Dale Carnegie là quyển sách của mọi thời đại và một hiện tượng đáng kinh ngạc trong ngành xuất bản Hoa Kỳ. Trong suốt nhiều thập kỷ tiếp theo và cho đến tận bây giờ, tác phẩm này vẫn chiếm vị trí số một trong danh mục sách bán chạy nhất và trở thành một sự kiện có một không hai trong lịch sử ngành xuất bản thế giới và được đánh giá là một quyển sách có tầm ảnh hưởng nhất mọi thời đại.', N'T01.jpg', N'Dale Carnegie')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (32, 4, N'Thuật Xử Thế Của Người Xưa', CAST(36000.0000 AS Decimal(19, 4)), 1, N'Ta hãy xét kỹ một cách thành thật tấm lòng mình: trong các bạn mà mình thương, có phải là những người thông minh nhất, gần họ, bao giờ mình cũng thấy thấp kém hơn; hay những người thật thà nhất, gần họ, bao giờ mình cũng thấy cao trọng hơn?
Cái ghét nhất của người đàn bà đẹp là có người đẹp hơn mình. Cái ghét nhất của người thông minh là có người thông minh hơn mình. "Người ta thích học văn hay mà không thích gần người viết văn hay..." Cái đó mình có thể hiểu được.', N'T02.jpg', N'Thu Giang Nguyễn Duy Cần')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (33, 4, N'Cái Dũng Của Thánh Nhân', CAST(36400.0000 AS Decimal(19, 4)), 1, N'Chỗ mà người xưa gọi là hào kiệt ắt phải có khí tiết hơn người. Nhưng nhân tình có chỗ không nhịn được. Bởi vậy, kẻ thất phu gặp nhục, tuốt gươm đứng dậy, vươn mình xốc đánh. Cái đó chưa gọi là dũng. Kẻ đại dũng trong thiên hạ, trái lại, thình lình gặp những việc phi thường cũng không kinh, vô cố bị những điều ngang trái cũng không giận. Đó là nhờ chỗ hoài bão của họ rất lớn và chỗ lập chí của họ rất xa vậy.', N'T03.jpg', N'Thu Giang Nguyễn Duy Cần')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (34, 4, N'Tâm Hồn Cao Thượng', CAST(110000.0000 AS Decimal(19, 4)), 1, N'Ngày 18/10 hằng năm là ngày khai trường truyền thống ở Ý. Vào ngày này năm 1886, Tâm hồn cao thượng (nguyên tác Cuore, nghĩa là Trái tim) cuốn tiểu thuyết trẻ em của nhà văn người Ý Edmondo De Amicis chính thức ra mắt. Ngay lập tức, nó chinh phục trái tim bạn đọc, không chỉ ở Ý mà còn lan khắp các châu lục khác.', N'T04.jpg', N'Edmondo De Amcis')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (35, 4, N'Cẩm Nang Tư Duy Đạo Đức', CAST(24000.0000 AS Decimal(19, 4)), 1, N'Bộ sách CẨM NANG TƯ DUY này dành cho mọi bạn đọc, từ học sinh, sinh viên đến các giảng viên, các nhà nghiên cứu, doanh nhân, người đã đi làm cũng như quý phụ huynh... muốn nâng cao năng lực tư duy của mình. Học sinh, sinh viên có thể đọc cẩm nang như tài liệu tham khảo để học tốt các bộ môn; quý phụ huynh có thể sử dụng cẩm nang để vừa nâng cao năng lực tư duy của mình vừa giúp con em mình phát triển các kỹ năng tư duy cần thiết để học tốt; các giảng viên, nhà nghiên cứu có thể sử dụng cẩm nang để học tốt; các chủ đề của mình; người đã đi làm, doanh nhân... có thể áp dụng các kỹ năng, ý tưởng của cẩm nang vào công việc và cuộc sống.', N'T05.jpg', N'Richard Paul , Linda Elder')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (36, 4, N'Sức Mạnh Của Tĩnh Tâm', CAST(65200.0000 AS Decimal(19, 4)), 1, N'Trái tim của chúng ta giống như một chiếc cốc thủy tinh. Khi cốc đựng đầy nước ép trái cây, người ta sẽ nói: “Đây là một cốc nước ép hoa quả”. Khi nó đựng đầy sữa, người ta lại nói: “Đây là một cốc sữa”. Còn chỉ khi chiếc cốc trống trơn thì người ta mới nói: “Đây là một cái cốc”. Rất nhiều lúc, trái tim của chúng ta chất chứa quá nhiều thứ, đến nỗi không thể nhìn thấy được cái tôi chân thực. Vì vậy, chỉ có vứt bỏ mọi sự rối ren để tâm tĩnh lại thì mới có thể xoa dịu được tâm trạng lo lắng bất an trong lòng, lấy lại sự yên bình và niềm vui trong tim.', N'T06.jpg', N'Hải Hoa')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (37, 4, N'Những Câu Chuyện Về Lòng Nhân Ái ', CAST(15300.0000 AS Decimal(19, 4)), 1, N'Việc các em được đọc ngay từ khi còn bé những câu chuyện đề cao những giá trị tốt đẹp là rất quan trọng. Và vì thế, NHỮNG CÂU CHUYỆN VỀ NHỮNG GIÁ TRỊ CAO ĐẸP ra đời. Bộ sách này được trình bày theo kiểu dễ đọc, có thể đọc ở nhà, trên lớp học…. Mỗi câu chuyện đều nhấn mạnh một đức tính nào đó như tính trung thực, tình nhân ái, lòng dũng cảm, tinh thần trách nhiệm… Những giá trị chân thật không bao giờ cũ, không bao giờ thừa. Sách gồm 11 câu chuyện về lòng nhân ái.', N'T07.jpg', N'NULL')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (38, 4, N'Có Một Con Mọt Sách', CAST(36450.0000 AS Decimal(19, 4)), 1, N'Bắt đầu từ mùa hè 2015, bên cạnh những thể loại sách đã quen thuộc với bạn đọc từ trước tới nay, Công ty Văn hóa Sáng tạo Trí Việt - First News sẽ phát hành nhiều ấn phẩm thú vị và bổ ích dành cho thiếu nhi và xem đây là thị trường quan trọng của First News trong thời gian tới. Mở đầu cho dòng sách này là cuốn sách Có một con mọt sách (NXB Tổng hợp TP.HCM) của bác sĩ Đỗ Hồng Ngọc.', N'T08.jpg', N'BS Đỗ Hồng Ngọc')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (39, 4, N'Viết Lên Hy Vọng', CAST(76400.0000 AS Decimal(19, 4)), 1, N'Trong vô vàn những câu danh ngôn về người thầy, có một câu nói rất nổi tiếng của nhà sư phạm Xô Viết, viện sĩ Viện Hàn lâm Khoa học Giáo dục Liên XôVasilij Aleksandrovich Sukhomlinskij rằng: "Đối với người giáo viên, cần phải có kiến thức, có hiểu biết sư phạm về quy luật xã hội, có khả năng dùng lời nói để tác động đến tâm hồn học sinh, có kỹ năng đặc sắc nhìn nhận con người và cảm thấy những rung động tinh tế nhất của trái tim con người." Dù ở bất kỳ quốc gia nào, trong bất kỳ nền giáo dục nào, câu nói ấy vẫn còn nguyên giá trị.', N'T09.jpg', N'Erin Gruwell và Những Nhà văn Tự do')
GO
INSERT [dbo].[Product] ([Id], [CatId], [Name], [Price], [Quantity], [Description], [Image], [Author]) VALUES (40, 4, N'Vượt Lên Chính Mình', CAST(46800.0000 AS Decimal(19, 4)), 1, N'Mỗi người trong cuộc đời đều phải cố gắng vượt qua rất nhiều khó khăn, trắc trở, chẳng mấy ai có thể luôn bình yên bước trên tấm thảm rải đầy hoa hồng. Thế nhưng kẻ thù to lớn nhất mà con người phải đối mặt, không ở đâu xa, lại là chính bản thân mình. Vượt qua được chính mình là vượt qua được tất cả và nắm vững thành công.', N'T10.jpg', N'Jon Gordon , Đặng Phương')
GO
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 1, N'S01-1.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 2, N'S02-1.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 3, N'S03-1.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 4, N'S04-1.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 5, N'S05-1.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 6, N'S06-1.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 7, N'S07-1.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 8, N'S08-1.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 9, N'S09-1.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 10, N'S10-1.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 11, N'M01-1.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 12, N'M02-1.png')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 13, N'M03-1.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 14, N'M04-1.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 15, N'M05-1.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 16, N'M06-1.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 17, N'M07-1.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 18, N'M08-1.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 19, N'M09-1.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 20, N'M10-1.png')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 21, N'K01-1.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 22, N'K02-1.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 23, N'K03-1.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 24, N'K04-1.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 25, N'K05-1.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 26, N'K06-1.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 27, N'K07-1.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 28, N'K08-1.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 29, N'K09-1.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 30, N'K10-1.png')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 31, N'T01-1.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 32, N'T02-1.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 33, N'T03-1.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 34, N'T04-1.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 35, N'T05-1.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 36, N'T06-1.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 37, N'T07-1.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 38, N'T08-1.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 39, N'T09-1.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (1, 40, N'T10-1.png')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 1, N'S01-2.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 2, N'S02-2.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 3, N'S03-2.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 4, N'S04-2.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 5, N'S05-2.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 6, N'S06-2.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 7, N'S07-2.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 8, N'S08-2.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 9, N'S09-2.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 10, N'S10-2.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 11, N'M01-2.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 12, N'M02-2.png')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 13, N'M03-2.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 14, N'M04-2.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 15, N'M05-2.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 16, N'M06-2.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 17, N'M07-2.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 18, N'M08-2.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 19, N'M09-2.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 20, N'M10-2.png')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 21, N'K01-2.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 22, N'K02-2.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 23, N'K03-2.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 24, N'K04-2.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 25, N'K05-2.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 26, N'K06-2.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 27, N'K07-2.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 28, N'K08-2.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 29, N'K09-2.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 30, N'K10-2.png')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 31, N'T01-2.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 32, N'T02-2.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 33, N'T03-2.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 34, N'T04-2.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 35, N'T05-2.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 36, N'T06-2.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 37, N'T07-2.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 38, N'T08-2.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 39, N'T09-2.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (2, 40, N'T10-2.png')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 1, N'S01-3.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 2, N'S02-3.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 3, N'S03-3.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 4, N'S04-3.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 5, N'S05-3.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 6, N'S06-3.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 7, N'S07-3.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 8, N'S08-3.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 9, N'S09-3.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 10, N'S10-3.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 11, N'M01-3.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 12, N'M02-3.png')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 13, N'M03-3.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 14, N'M04-3.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 15, N'M05-3.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 16, N'M06-3.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 17, N'M07-3.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 18, N'M08-3.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 19, N'M09-3.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 20, N'M10-3.png')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 21, N'K01-3.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 22, N'K02-3.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 23, N'K03-3.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 24, N'K04-3.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 25, N'K05-3.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 26, N'K06-3.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 27, N'K07-3.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 28, N'K08-3.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 29, N'K09-3.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 30, N'K10-3.png')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 31, N'T01-3.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 32, N'T02-3.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 33, N'T03-3.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 34, N'T04-3.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 35, N'T05-3.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 36, N'T06-3.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 37, N'T07-3.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 38, N'T08-3.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 39, N'T09-3.jpg')
GO
INSERT [dbo].[Product_Images] ([id], [ProductId], [Name]) VALUES (3, 40, N'T10-3.png')
GO
SET IDENTITY_INSERT [dbo].[Purchase] ON 
GO
INSERT [dbo].[Purchase] ([Purchase_ID], [Total], [Created_At], [Status], [Customer_Tel]) VALUES (3, CAST(666300.0000 AS Decimal(19, 4)), CAST(N'2021-01-02T23:37:30.490' AS DateTime), 1, N'789546123')
GO
INSERT [dbo].[Purchase] ([Purchase_ID], [Total], [Created_At], [Status], [Customer_Tel]) VALUES (4, CAST(357300.0000 AS Decimal(19, 4)), CAST(N'2021-02-03T23:38:12.297' AS DateTime), 3, N'135478952')
GO
INSERT [dbo].[Purchase] ([Purchase_ID], [Total], [Created_At], [Status], [Customer_Tel]) VALUES (5, CAST(425300.0000 AS Decimal(19, 4)), CAST(N'2021-03-04T23:38:44.017' AS DateTime), 3, N'135478951')
GO
INSERT [dbo].[Purchase] ([Purchase_ID], [Total], [Created_At], [Status], [Customer_Tel]) VALUES (6, CAST(158000.0000 AS Decimal(19, 4)), CAST(N'2021-04-05T23:25:31.623' AS DateTime), 1, N'123456789')
GO
INSERT [dbo].[Purchase] ([Purchase_ID], [Total], [Created_At], [Status], [Customer_Tel]) VALUES (7, CAST(158000.0000 AS Decimal(19, 4)), CAST(N'2021-05-06T23:25:48.517' AS DateTime), 1, N'123456787')
GO
INSERT [dbo].[Purchase] ([Purchase_ID], [Total], [Created_At], [Status], [Customer_Tel]) VALUES (8, CAST(260000.0000 AS Decimal(19, 4)), CAST(N'2021-06-01T23:26:08.277' AS DateTime), 1, N'1234567875')
GO
INSERT [dbo].[Purchase] ([Purchase_ID], [Total], [Created_At], [Status], [Customer_Tel]) VALUES (9, CAST(260000.0000 AS Decimal(19, 4)), CAST(N'2021-07-05T23:26:34.563' AS DateTime), 1, N'1234567875')
GO
INSERT [dbo].[Purchase] ([Purchase_ID], [Total], [Created_At], [Status], [Customer_Tel]) VALUES (10, CAST(291000.0000 AS Decimal(19, 4)), CAST(N'2021-08-13T23:26:59.677' AS DateTime), 1, N'1234567875')
GO
INSERT [dbo].[Purchase] ([Purchase_ID], [Total], [Created_At], [Status], [Customer_Tel]) VALUES (11, CAST(268000.0000 AS Decimal(19, 4)), CAST(N'2021-09-13T23:35:23.830' AS DateTime), 3, N'123456123')
GO
INSERT [dbo].[Purchase] ([Purchase_ID], [Total], [Created_At], [Status], [Customer_Tel]) VALUES (12, CAST(147000.0000 AS Decimal(19, 4)), CAST(N'2021-10-13T23:35:43.213' AS DateTime), 3, N'123456123')
GO
INSERT [dbo].[Purchase] ([Purchase_ID], [Total], [Created_At], [Status], [Customer_Tel]) VALUES (15, CAST(1715000.0000 AS Decimal(19, 4)), CAST(N'2021-11-22T23:37:30.490' AS DateTime), 1, N'123456789')
GO
INSERT [dbo].[Purchase] ([Purchase_ID], [Total], [Created_At], [Status], [Customer_Tel]) VALUES (16, CAST(1715000.0000 AS Decimal(19, 4)), CAST(N'2021-12-22T17:44:48.747' AS DateTime), 3, N'123456789')
GO
INSERT [dbo].[Purchase] ([Purchase_ID], [Total], [Created_At], [Status], [Customer_Tel]) VALUES (17, CAST(1789000.0000 AS Decimal(19, 4)), CAST(N'2021-06-22T17:51:04.443' AS DateTime), 3, N'123456789')
GO
SET IDENTITY_INSERT [dbo].[Purchase] OFF
GO
SET IDENTITY_INSERT [dbo].[PurchaseDetail] ON 
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (1, 6, 1, CAST(119000.0000 AS Decimal(19, 4)), 1, 119000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (2, 6, 2, CAST(39000.0000 AS Decimal(19, 4)), 1, 39000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (3, 7, 1, CAST(119000.0000 AS Decimal(19, 4)), 1, 119000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (4, 7, 2, CAST(39000.0000 AS Decimal(19, 4)), 1, 39000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (5, 8, 1, CAST(119000.0000 AS Decimal(19, 4)), 1, 119000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (6, 8, 2, CAST(39000.0000 AS Decimal(19, 4)), 1, 39000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (7, 8, 3, CAST(102000.0000 AS Decimal(19, 4)), 1, 102000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (8, 9, 1, CAST(119000.0000 AS Decimal(19, 4)), 1, 119000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (9, 9, 2, CAST(39000.0000 AS Decimal(19, 4)), 1, 39000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (10, 9, 3, CAST(102000.0000 AS Decimal(19, 4)), 1, 102000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (11, 10, 7, CAST(68000.0000 AS Decimal(19, 4)), 1, 68000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (12, 10, 8, CAST(76000.0000 AS Decimal(19, 4)), 1, 76000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (13, 10, 5, CAST(47000.0000 AS Decimal(19, 4)), 1, 47000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (14, 10, 6, CAST(100000.0000 AS Decimal(19, 4)), 1, 100000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (15, 11, 1, CAST(119000.0000 AS Decimal(19, 4)), 1, 119000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (16, 11, 3, CAST(102000.0000 AS Decimal(19, 4)), 1, 102000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (17, 11, 5, CAST(47000.0000 AS Decimal(19, 4)), 1, 47000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (18, 12, 5, CAST(47000.0000 AS Decimal(19, 4)), 1, 47000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (19, 12, 6, CAST(100000.0000 AS Decimal(19, 4)), 1, 100000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (22, 3, 1, CAST(119000.0000 AS Decimal(19, 4)), 12, 1428000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (23, 3, 2, CAST(39000.0000 AS Decimal(19, 4)), 2, 78000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (24, 3, 3, CAST(102000.0000 AS Decimal(19, 4)), 1, 102000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (26, 3, 5, CAST(47000.0000 AS Decimal(19, 4)), 1, 47000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (27, 3, 6, CAST(100000.0000 AS Decimal(19, 4)), 2, 200000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (29, 4, 1, CAST(119000.0000 AS Decimal(19, 4)), 1, 119000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (30, 4, 4, CAST(91300.0000 AS Decimal(19, 4)), 1, 91300)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (31, 4, 6, CAST(100000.0000 AS Decimal(19, 4)), 1, 100000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (32, 4, 5, CAST(47000.0000 AS Decimal(19, 4)), 1, 47000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (33, 5, 1, CAST(119000.0000 AS Decimal(19, 4)), 1, 119000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (34, 5, 4, CAST(91300.0000 AS Decimal(19, 4)), 1, 91300)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (35, 5, 6, CAST(100000.0000 AS Decimal(19, 4)), 1, 100000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (36, 5, 5, CAST(47000.0000 AS Decimal(19, 4)), 1, 47000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (37, 5, 7, CAST(68000.0000 AS Decimal(19, 4)), 1, 68000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (38, 3, 11, CAST(101000.0000 AS Decimal(19, 4)), 1, 101000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (1002, 16, 2, CAST(39000.0000 AS Decimal(19, 4)), 1, 39000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (1003, 16, 6, CAST(100000.0000 AS Decimal(19, 4)), 1, 100000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (1004, 16, 8, CAST(76000.0000 AS Decimal(19, 4)), 1, 76000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (1005, 16, 18, CAST(500000.0000 AS Decimal(19, 4)), 3, 1500000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (1006, 17, 2, CAST(39000.0000 AS Decimal(19, 4)), 1, 39000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (1007, 17, 6, CAST(100000.0000 AS Decimal(19, 4)), 1, 100000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (1008, 17, 8, CAST(76000.0000 AS Decimal(19, 4)), 1, 76000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (1009, 17, 18, CAST(500000.0000 AS Decimal(19, 4)), 3, 1500000)
GO
INSERT [dbo].[PurchaseDetail] ([PurchaseDetail_ID], [Purchase_ID], [Product_ID], [Price], [Quantity], [Total]) VALUES (1010, 17, 17, CAST(74000.0000 AS Decimal(19, 4)), 1, 74000)
GO
SET IDENTITY_INSERT [dbo].[PurchaseDetail] OFF
GO
INSERT [dbo].[PurchaseStatusEnum] ([EnumKey], [Value], [Description]) VALUES (N'New', 1, N'Đơn hàng mới được tạo')
GO
INSERT [dbo].[PurchaseStatusEnum] ([EnumKey], [Value], [Description]) VALUES (N'Completed', 2, N'Khách hàng đã thanh toán')
GO
INSERT [dbo].[PurchaseStatusEnum] ([EnumKey], [Value], [Description]) VALUES (N'Cancelled', 3, N'Đơn hàng đã hủy')
GO
INSERT [dbo].[PurchaseStatusEnum] ([EnumKey], [Value], [Description]) VALUES (N'Shipping', 4, N'Đã thanh toán và đã giao')
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([CatId])
REFERENCES [dbo].[Category] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
ALTER TABLE [dbo].[Product_Images]  WITH CHECK ADD  CONSTRAINT [FK_Product_Images_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Product_Images] CHECK CONSTRAINT [FK_Product_Images_Product]
GO
ALTER TABLE [dbo].[Purchase]  WITH CHECK ADD  CONSTRAINT [FK_Purchase_Customer] FOREIGN KEY([Customer_Tel])
REFERENCES [dbo].[Customer] ([Tel])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Purchase] CHECK CONSTRAINT [FK_Purchase_Customer]
GO
ALTER TABLE [dbo].[Purchase]  WITH CHECK ADD  CONSTRAINT [FK_Purchase_PurchaseStatusEnum] FOREIGN KEY([Status])
REFERENCES [dbo].[PurchaseStatusEnum] ([Value])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Purchase] CHECK CONSTRAINT [FK_Purchase_PurchaseStatusEnum]
GO
ALTER TABLE [dbo].[PurchaseDetail]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseDetail_Product] FOREIGN KEY([Product_ID])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[PurchaseDetail] CHECK CONSTRAINT [FK_PurchaseDetail_Product]
GO
ALTER TABLE [dbo].[PurchaseDetail]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseDetail_Purchase] FOREIGN KEY([Purchase_ID])
REFERENCES [dbo].[Purchase] ([Purchase_ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PurchaseDetail] CHECK CONSTRAINT [FK_PurchaseDetail_Purchase]
GO
/****** Object:  StoredProcedure [dbo].[sp_BestSellerPurchase]    Script Date: 22/1/2021 9:21:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_BestSellerPurchase]
as
begin

select TOP(5) Product_ID,sum(Quantity) as TotalQuanlity from PurchaseDetail
group by Product_ID
order by TotalQuanlity desc
end
GO
/****** Object:  StoredProcedure [dbo].[sp_BestSellerPurchasePrice]    Script Date: 22/1/2021 9:21:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_BestSellerPurchasePrice]
as
begin
	select TOP(5) Product_ID,sum(Total) as Total from PurchaseDetail
	group by Product_ID
end
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertPurchase]    Script Date: 22/1/2021 9:21:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_InsertPurchase]
@Customer_Tel nvarchar(50),@Created_At datetime, @Total decimal, @Status int = null,
@id int output
as
begin
	insert into Purchase(Customer_Tel, Created_At, Total, Status) values(@Customer_Tel,@Created_At, @Total, @Status)
	set @id = (select Purchase_ID from Purchase where Customer_Tel = @Customer_Tel and Created_At = @Created_At)
	return @id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_NewPurchase]    Script Date: 22/1/2021 9:21:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[sp_NewPurchase]
as
begin
select TOP(5) * from Purchase
order by (Created_At) desc
end
GO
/****** Object:  StoredProcedure [dbo].[sp_SumAtMonth]    Script Date: 22/1/2021 9:21:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_SumAtMonth]
@Year_Purchase int
as
begin
declare @Month int = 1
declare @TotalMonth decimal
declare @MonthTable table(Thang int , TotalByMonth decimal)
	while(@Month <=12)
	begin
		set @TotalMonth = (Select sum(Total) as tootal from Purchase
		where MONTH(Created_At) = @Month
		and Year(Created_At) = @Year_Purchase)
		if(@TotalMonth is not NULL)
		begin
			Insert into @MonthTable values(@Month,@TotalMonth)
		end
		else
			Insert into @MonthTable values(@Month,0)
		
		set @Month += 1

	end
select * from @MonthTable
end
GO

create trigger [dbo].[trgPurchaseDetail2] on [dbo].[PurchaseDetail]
for delete,insert,update
as
begin
	declare @quantity int =-1
	declare @id int =-1
	select * from inserted
	select * from deleted

	select @quantity = p.Quantity+i.Quantity,@id = i.Product_ID from deleted i join Product p on i.Product_ID = p.Id
	select @quantity,@id
	if(@quantity >=0)
	begin
		update Product set Quantity = @quantity where Id = @id
	end
	
	set @quantity = null
	set @id =-1

	select @quantity = p.Quantity-i.Quantity,@id = i.Product_ID from inserted i join Product p on i.Product_ID = p.Id
	select @quantity,@id
	if(@quantity <0)
		rollback
	if(@quantity >=0)
	begin
		update Product set Quantity = @quantity where Id = @id
	end
end
go