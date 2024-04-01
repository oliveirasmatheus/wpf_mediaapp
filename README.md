# PMEB_Final_Group2
Window C# final group project for movie search and recommend

Version: .NET 7.0

Database Setup:
  1 - This one gets you the logical db and log names. Should be the same as in the second command, but I put this here in case it's needed.
  
      RESTORE FILELISTONLY
      FROM DISK = '<File path to database>\IMDB_Project.bak';

  2 - The real restore command, needed to restore the db file as a working db in your local instance
  
      RESTORE DATABASE IMDB FROM DISK = '<File path to database>\imdb_project.bak'
      WITH MOVE 'IMDB_Project' TO '<File path to YOUR localDB instance folder>\IMDB_Project.mdf',
      MOVE 'IMDB_Project_Log' TO '<File path to YOUR localDB instance folder>\IMDB_Project_Log.ldf',
      RECOVERY, REPLACE, STATS = 10;
