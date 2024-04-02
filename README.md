# PMEB_Final_Group2
Window C# final group project for movie search and recommend

Version: .NET 7.0

Database Setup:
  1 - This one gets you the logical db and log names. Should be the same as in the second command, but I put this here in case it's needed.
  
      RESTORE FILELISTONLY
      FROM DISK = '<File path to database>\NEW_IMDB.bak';

  2 - The real restore command, needed to restore the db file as a working db in your local instance
  
      RESTORE DATABASE IMDB FROM DISK = '<File path to database>\NEW_IMDB.bak'
      WITH MOVE 'IMDB_Project' TO '<File path to YOUR localDB instance folder>\IMDB_Project.mdf',
      MOVE 'IMDB_Project_Log' TO '<File path to YOUR localDB instance folder>\IMDB_Project_Log.ldf',
      RECOVERY, REPLACE, STATS = 10;



NuGet:
      Microsoft.EntityFrameworkCore  7.0.17
      Microsoft.EntityFrameworkCore.Design  7.0.17
      Microsoft.EntityFrameworkCore.SqlServer  7.0.17
      Microsoft.EntityFrameworkCore.Tools  7.0.17


Scaffold Command：

scaffold-dbcontext "{Your Local DB Connect String}" Microsoft.EntityFrameworkCore.SqlServer -contextdir Data -outputdir Models/Generated -contextnamespace PMEB_Final_Group2.Data -namespace PMEB_Final_Group2.Models -force

AppConfig：connectionStrings：{Your Local DB Connect String}


  
