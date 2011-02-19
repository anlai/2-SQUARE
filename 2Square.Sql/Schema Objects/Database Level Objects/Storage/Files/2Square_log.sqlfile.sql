ALTER DATABASE [$(DatabaseName)]
    ADD LOG FILE (NAME = [2Square_log], FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\2Square_log.ldf', SIZE = 768 KB, MAXSIZE = 2097152 MB, FILEGROWTH = 10 %);

