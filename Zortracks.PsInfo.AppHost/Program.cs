var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Zortracks_PsInfo_Apis_Host>("zortracks-psinfo-apis-host");

builder.Build().Run();
