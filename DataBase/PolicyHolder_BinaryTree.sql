USE [sample]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PolicyHolder_BinaryTree](
	[id] [int] NOT NULL,
	[policyholder_code] [varchar](10) NOT NULL,
	[left_child_id] [int] NULL,
	[right_child_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PolicyHolder_BinaryTree]  WITH CHECK ADD FOREIGN KEY([left_child_id])
REFERENCES [dbo].[PolicyHolder_BinaryTree] ([id])
GO

ALTER TABLE [dbo].[PolicyHolder_BinaryTree]  WITH CHECK ADD FOREIGN KEY([policyholder_code])
REFERENCES [dbo].[PolicyHolder] ([code])
GO

ALTER TABLE [dbo].[PolicyHolder_BinaryTree]  WITH CHECK ADD FOREIGN KEY([right_child_id])
REFERENCES [dbo].[PolicyHolder_BinaryTree] ([id])
GO


