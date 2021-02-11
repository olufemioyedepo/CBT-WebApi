USE [CBT]
GO
SET IDENTITY_INSERT [dbo].[Candidates] ON 

INSERT [dbo].[Candidates] ([Id], [Name], [Email], [Mobile]) VALUES (1, N'Olawale Kunle                                                                                       ', N'kunle@test.com', N'09091911111')
INSERT [dbo].[Candidates] ([Id], [Name], [Email], [Mobile]) VALUES (2, N'Yinka Femilo                                                                                        ', N'ola8282@gmail.com', N'080718165181')
SET IDENTITY_INSERT [dbo].[Candidates] OFF
GO
SET IDENTITY_INSERT [dbo].[TestScores] ON 

INSERT [dbo].[TestScores] ([Id], [Score], [Date], [Name], [CandidateId]) VALUES (1, 23, CAST(N'2021-02-11T13:33:24.8781093' AS DateTime2), N'MTS 303 Test', 2)
INSERT [dbo].[TestScores] ([Id], [Score], [Date], [Name], [CandidateId]) VALUES (2, 38, CAST(N'2021-02-11T13:44:45.7223977' AS DateTime2), N'CSC 203 Test', 2)
INSERT [dbo].[TestScores] ([Id], [Score], [Date], [Name], [CandidateId]) VALUES (3, 81, CAST(N'2021-02-11T13:45:08.6378676' AS DateTime2), N'PMT 201 Test', 2)
SET IDENTITY_INSERT [dbo].[TestScores] OFF
GO
